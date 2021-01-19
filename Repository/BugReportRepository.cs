using System;
using System.Collections.Generic;
using DFCommonLib.DataAccess;
using DFCommonLib.Logger;

using BugReportServer.Model;

namespace BugReportServer.Repository
{
    public interface IBugReportRepository
    {
        string SaveBugReport(BugReportData bugReportData);
    }

    public class BugReportRepository : IBugReportRepository
    {
        private IDbConnectionFactory _connection;

        private readonly IDFLogger<BugReportRepository> _logger;

        public BugReportRepository(
            IDbConnectionFactory connection,
            IDFLogger<BugReportRepository> logger
            )
        {
            _connection = connection;
            _logger = logger;
        }

        public string SaveBugReport(BugReportData bugReportData)
        {
            var token = CreateToken();

            var sql = @"INSERT INTO bugreports (token, title,description,email,created,updated)
                VALUES ( @token, @title, @description, @email, now(), now() )";
            using (var cmd = _connection.CreateCommand(sql))
            {
                cmd.AddParameter("@token", token);
                cmd.AddParameter("@title", bugReportData.title);
                cmd.AddParameter("@description", bugReportData.description);
                cmd.AddParameter("@email", bugReportData.email);
                cmd.ExecuteNonQuery();
            }
            return token;
        }

        private string CreateToken()
        {
            string token = Guid.NewGuid().ToString();
            return token;
        }
    }
}