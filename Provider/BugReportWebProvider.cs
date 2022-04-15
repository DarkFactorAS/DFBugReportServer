using System;
using System.IO;
using System.Collections.Generic;
using DFCommonLib.Logger;
using DFCommonLib.HttpApi;

using BugReportServer.Model;
using BugReportServer.Repository;

namespace BugReportServer.Provider
{
    public interface IBugReportWebProvider
    {
        BugReportListModel GetAllBugReports();
        WebAPIData DeleteBugReport(int bugreportId);
    }

    public class BugReportWebProvider : IBugReportWebProvider
    {
        IBugReportWebRepository _repository;

        public BugReportWebProvider(IBugReportWebRepository repository)
        {
            _repository = repository;
        }

        public BugReportListModel GetAllBugReports()
        {
            // TODO: Must me logged in to do this
            return _repository.GetAllBugReports();
        }

        public WebAPIData DeleteBugReport(int bugreportId)
        {
            // TODO: Must me logged in to do this
            return _repository.DeleteBugReport(bugreportId);
        }
    }
}
