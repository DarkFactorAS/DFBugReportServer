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
        private readonly IDFLogger<BugReportWebProvider> _logger;

        public BugReportWebProvider(IBugReportWebRepository repository, IDFLogger<BugReportWebProvider> logger, IFileHandler fileHandler)
        {
            _repository = repository;
            _logger = logger;
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
                try
                {
                    data.imageBase64Data = _fileHandler.ReadBase64File(filename);
                }
                catch( Exception ex )
                {
                    data.message = ex.ToString();
                }
            }

            return data;
        }

        public WebAPIData DeleteBugReport(int bugreportId)
        {
            if ( bugreportId == 0 )
            {
                return new WebAPIData(500,"BugReportID is 0");
            }

            // Delete screenshot if we have one
            string filename = _repository.GetBugReportFilename(bugreportId);
            if (!string.IsNullOrEmpty(filename))
            {
                var fileResult = _fileHandler.DeleteFile(filename);
                if ( fileResult == FileHandler.ErrorCode.OK )
                {
                    _logger.LogImportant(string.Format("Deleted bugreport file : {0} for bugreport {1}", filename, bugreportId));
                    _repository.DeleteBugReportFiles(bugreportId);
                }
                else
                {
                    _logger.LogWarning(string.Format("Failed to delete file : {0} for bugreport {1} (error:{2})", filename, bugreportId, fileResult));
//                    return new WebAPIData(404,"Bugreportfile not found/deleted");
                }
            }

            // Delete the bugreport itself
            _repository.DeleteBugReport(bugreportId);

            return new WebAPIData(0,"OK");
        }
    }
}
