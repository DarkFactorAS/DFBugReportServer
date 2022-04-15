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

        public WebAPIData DeleteBugReport(int bugreportId)
        {
            return null;
        }
    }
}