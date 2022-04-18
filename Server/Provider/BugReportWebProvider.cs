using System;
using System.IO;
using System.Collections.Generic;
using DFCommonLib.Logger;
using DFCommonLib.HttpApi;
using DFCommonLib.IO;

using BugReportServer.Model;
using BugReportServer.Repository;

namespace BugReportServer.Provider
{
    public interface IBugReportWebProvider
    {
        BugReportListModel GetAllBugReports();
        BugReportExtendedData GetBugReport(int bugreportId);
        WebAPIData DeleteBugReport(int bugreportId);
    }

    public class BugReportWebProvider : IBugReportWebProvider
    {
        IBugReportWebRepository _repository;
        IFileHandler _fileHandler;

        public BugReportWebProvider(IBugReportWebRepository repository, IFileHandler fileHandler)
        {
            _repository = repository;
            _fileHandler = fileHandler;
            _fileHandler.SetFolder("/BugReports");
        }

        public BugReportListModel GetAllBugReports()
        {
            // TODO: Must me logged in to do this
            return _repository.GetAllBugReports();
        }

        public BugReportExtendedData GetBugReport(int bugreportId)
        {
            BugReportExtendedData data = new BugReportExtendedData();
            data.bugReport = _repository.GetBugReport(bugreportId);
            string filename = _repository.GetBugReportFilename(bugreportId);

            if ( filename != null )
            {
                data.imageBase64Data = _fileHandler.ReadBase64File(filename);
            }

            return data;
        }

        public WebAPIData DeleteBugReport(int bugreportId)
        {
            // TODO: Must me logged in to do this
            return _repository.DeleteBugReport(bugreportId);
        }
    }
}
