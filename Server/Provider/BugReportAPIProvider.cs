using System;
using System.IO;
using System.Collections.Generic;
using DFCommonLib.Logger;
using DFCommonLib.IO;

using BugReportServer.Model;
using BugReportServer.Repository;

namespace BugReportServer.Provider
{
    public interface IBugReportAPIProvider
    {
        BugReponseData ReportBug(BugReportData bugReportData);
        BugReponseFileData AttachFile(BugReportFileData fileData);
    }

    public class BugReportAPIProvider : IBugReportAPIProvider
    {
        IBugReportRepository _repository;
        private readonly IDFLogger<BugReportRepository> _logger;
        private readonly IFileHandler _fileHandler;

        public BugReportAPIProvider(IBugReportRepository repository, IDFLogger<BugReportRepository> logger, IFileHandler fileHandler )
        {
            _repository = repository;
            _logger = logger;
            _fileHandler = fileHandler;
            _fileHandler.SetFolder("/BugReports");
        }

        //
        // Store bugreport
        //
        public BugReponseData ReportBug(BugReportData bugReportData)
        {
            bugReportData = VerifyFields(bugReportData);
            uint serverId = _repository.SaveBugReport(bugReportData);
            return new BugReponseData( bugReportData.clientBugId, serverId );
        }

        //
        // Save file with bugreport
        //
        public BugReponseFileData AttachFile(BugReportFileData fileData)
        {
            if ( fileData.serverBugId > 0 && fileData.fileId > 0 )
            {
                // Make sure the bugreport actually exist
                if ( !_repository.IsBugReport(fileData.serverBugId))
                {
                    _logger.LogDebug(string.Format("Ignored file for bugreport {0} since bug does not exist", fileData.serverBugId));
                    return new BugReponseFileData( fileData.serverBugId, fileData.fileId);
                }

                // Prefix the filename with the bug id
                var strId = string.Format("{0}",fileData.serverBugId);
                var diskFilename = strId + _fileHandler.CreateDiskFilename(fileData.filename);

                var errorCode = _fileHandler.SaveFile( diskFilename, fileData.base64data);
                if ( errorCode == 0 )
                {
                    _repository.LinkFileToReport(fileData.serverBugId, diskFilename);
                }
                else
                {
                    _logger.LogDebug(string.Format("Ignored file for bugreport {0} since bug does not exist", fileData.serverBugId));
                }
                return new BugReponseFileData( fileData.serverBugId, fileData.fileId);
            }
            return null;
        }


        private BugReportData VerifyFields(BugReportData bugReportData)
        {
            bugReportData.title = VerifyString(bugReportData.title);
            bugReportData.message = VerifyString( bugReportData.message );
            bugReportData.email = VerifyString( bugReportData.email );
            bugReportData.clientName = VerifyString( bugReportData.clientName );
            bugReportData.clientVersion = VerifyString( bugReportData.clientVersion );
            return bugReportData;
        }

        private string VerifyString(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return input;
            }
            return "";
        }
    }
}
