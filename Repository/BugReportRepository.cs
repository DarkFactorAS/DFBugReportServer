using System;
using DFCommonLib.DataAccess;
using DFCommonLib.Logger;

using BugReportServer.Model;

namespace BugReportServer.Repository
{
    public interface IBugReportRepository
    {
        uint SaveBugReport(BugReportData bugReportData);
        bool IsBugReport(uint bugReportId);
        void LinkFileToReport(uint serverBugId, string filename);
    }

    public class BugReportRepository : IBugReportRepository
    {
        private IDbConnectionFactory _connection;
        private IDBPatcher _dbPatcher;

        private static string PATCHER = "BugReport";

        private readonly IDFLogger<BugReportRepository> _logger;

        public BugReportRepository(
            IDbConnectionFactory connection,
            IDFLogger<BugReportRepository> logger,
            IDBPatcher dbPatcher
            )
        {
            _connection = connection;
            _logger = logger;
            _dbPatcher = dbPatcher;

            Init();
        }

        public bool Init()
        {
            _dbPatcher.Init();
            _dbPatcher.Patch(PATCHER,1, "CREATE TABLE `logtable` ( `id` int(11) NOT NULL AUTO_INCREMENT, `created` datetime NOT NULL, `loglevel` int(11) NOT NULL, `groupname` varchar(100) NOT NULL DEFAULT '', `message` varchar(1024) NOT NULL DEFAULT '', PRIMARY KEY (`id`))");
            _dbPatcher.Patch(PATCHER,2, "CREATE TABLE `bugreports` ( `id` int(11) NOT NULL AUTO_INCREMENT, `title` varchar(100) NOT NULL DEFAULT '', `message` varchar(300) NOT NULL DEFAULT '', `email` varchar(100) NOT NULL DEFAULT '', `clientName` varchar(50) NOT NULL DEFAULT '',  `clientVersion` varchar(25) NOT NULL DEFAULT '',  `created` datetime NOT NULL,  `updated` datetime NOT NULL,  PRIMARY KEY (`id`))");
            _dbPatcher.Patch(PATCHER,3, "CREATE TABLE `bugreportfiles` ( `id` int(11) NOT NULL AUTO_INCREMENT, `bugId` int(11) NOT NULL DEFAULT 0, `filename` varchar(300) NOT NULL DEFAULT '', PRIMARY KEY (`id`))");
            return _dbPatcher.Successful();
        }

        public uint SaveBugReport(BugReportData bugReportData)
        {
            var sql = @"INSERT INTO bugreports (title, message,email,clientName, clientVersion,created,updated)
                VALUES ( @title, @message, @email, @clientName, @clientVersion, now(), now() )";
            using (var cmd = _connection.CreateCommand(sql))
            {
                cmd.AddParameter("@title", bugReportData.title);
                cmd.AddParameter("@message", bugReportData.message);
                cmd.AddParameter("@email", bugReportData.email);
                cmd.AddParameter("@clientName", bugReportData.clientName);
                cmd.AddParameter("@clientVersion", bugReportData.clientVersion);
                cmd.ExecuteNonQuery();
            }

            return GetId();
        }

        public bool IsBugReport(uint bugReportId)
        {
            var sql = @"SELECT id from bugreports where id = @bugId";
            using (var cmd = _connection.CreateCommand(sql))
            {
                cmd.AddParameter("@bugId", bugReportId);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void LinkFileToReport(uint serverBugId, string filename)
        {
            var sql = @"INSERT INTO bugreportfiles (bugId, filename) VALUES ( @bugId, @filename )";
            using (var cmd = _connection.CreateCommand(sql))
            {
                cmd.AddParameter("@bugId", serverBugId);
                cmd.AddParameter("@filename", filename);
                cmd.ExecuteNonQuery();
            }
        }


        private uint GetId()
        {
            var sql = @"SELECT LAST_INSERT_ID() as id";
            using (var cmd = _connection.CreateCommand(sql))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        uint id = Convert.ToUInt32(reader["id"]);
                        return id;
                    }
                }
            }
            return 0;
        }
    }
}