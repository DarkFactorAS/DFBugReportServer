
using System;
using System.Collections.Generic;

using DFCommonLib.DataAccess;
using DFCommonLib.Logger;

namespace BugReportWeb
{
    public interface IBugReportProvider
    {
        IList<BugReportModel> GetAllBugReports();
    }

    public class BugReportProvider : IBugReportProvider
    {
        IBugReportRepository _repository;

        public BugReportProvider( IBugReportRepository repostory )
        {
            _repository = repostory;
        }

        public IList<BugReportModel> GetAllBugReports()
        {
            return _repository.GetAllBugReports();
        }
    }
}