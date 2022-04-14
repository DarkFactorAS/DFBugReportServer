
using System;
using System.Collections.Generic;

using DFCommonLib.DataAccess;
using DFCommonLib.Logger;

namespace BugReportWeb
{
    public interface IBugReportRepository
    {
        IList<BugReportModel> GetAllBugReports();
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


        public IList<BugReportModel> GetAllBugReports()
        {
            IList<BugReportModel> model = new List<BugReportModel>();

            model.Add( new BugReportModel{
                id = 1,
                title = "Test"
            });

            return model;
        }
    }
}