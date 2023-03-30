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

        private readonly IDFLogger<BugReportRepository> _logger;

        public BugReportRepository(IDbConnectionFactory connection, IDFLogger<BugReportRepository> logger )
        {
            _connection = connection;
            _logger = logger;
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