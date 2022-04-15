using System;
using System.Collections.Generic;
using DFCommonLib.DataAccess;
using DFCommonLib.Logger;

using BugReportServer.Model;
using DFCommonLib.HttpApi;

namespace BugReportServer.Repository
{
    public interface IBugReportWebRepository
    {
        BugReportListModel GetAllBugReports();
        BugReportData GetBugReport(int bugReportId);
        WebAPIData DeleteBugReport(int bugreportId);
    }

    public class BugReportWebRepository : IBugReportWebRepository
    {
        private IDbConnectionFactory _connection;
        private readonly IDFLogger<BugReportWebRepository> _logger;

        public BugReportWebRepository(IDbConnectionFactory connection, IDFLogger<BugReportWebRepository> logger)
        {
            _connection = connection;
            _logger = logger;
        }

        public BugReportListModel GetAllBugReports()
        {
            BugReportListModel model = new BugReportListModel(0,"");
            model.bugReports = new List<BugReportData>();

            try
            {
                var sql = @"SELECT * from bugreports";
                using (var cmd = _connection.CreateCommand(sql))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BugReportData bugReport = new BugReportData();
                            bugReport.clientBugId = Convert.ToUInt32(reader["id"]);
                            bugReport.title = reader["title"].ToString(); 
                            bugReport.message = reader["message"].ToString(); 
                            bugReport.created = Convert.ToDateTime(reader["created"].ToString());
                            bugReport.updated = Convert.ToDateTime(reader["updated"].ToString());

                            model.bugReports.Add(bugReport);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                // For now just show the callstack
                model.errorCode = 666;
                model.message = ex.ToString();
            }
            return model;
        }

        public BugReportData GetBugReport(int bugReportId)
        {
            try
            {
                var sql = @"SELECT * from bugreports where id = @bugId";
                using (var cmd = _connection.CreateCommand(sql))
                {
                    cmd.AddParameter("@bugId", bugReportId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            BugReportData bugReport = new BugReportData();
                            bugReport.clientBugId = Convert.ToUInt32(reader["id"]);
                            bugReport.title = reader["title"].ToString(); 
                            bugReport.message = reader["message"].ToString(); 
                            bugReport.created = Convert.ToDateTime(reader["created"].ToString());
                            bugReport.updated = Convert.ToDateTime(reader["updated"].ToString());
                            return bugReport;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                // Log error
            }
            return null;
        }

        public WebAPIData DeleteBugReport(int bugreportId)
        {
            return null;
        }
    }
}