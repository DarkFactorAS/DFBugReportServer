using System;
using System.IO;
using System.Collections.Generic;
using DFCommonLib.Logger;

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
        private const uint MAX_FILE_SIZE = 10000000; // 10.000.000 bytes
        private readonly IDFLogger<BugReportRepository> _logger;

        public BugReportAPIProvider(IBugReportRepository repository, IDFLogger<BugReportRepository> logger)
        {
            _repository = repository;
            _logger = logger;
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
                // Do not allow large files
                int len = fileData.base64data.Length;
                if ( len > MAX_FILE_SIZE || len <= 0 )
                {
                    _logger.LogDebug(string.Format("File for bugreport {0} is too large {1} > {2}", fileData.serverBugId, len, MAX_FILE_SIZE));
                    return new BugReponseFileData( fileData.serverBugId, fileData.fileId);
                }

                // Make sure the bugreport actually exist
                if ( !_repository.IsBugReport(fileData.serverBugId))
                {
                    _logger.LogDebug(string.Format("Ignored file for bugreport {0} since bug does not exist", fileData.serverBugId));
                    return new BugReponseFileData( fileData.serverBugId, fileData.fileId);
                }

                var strId = string.Format("{0}",fileData.serverBugId);
                var ext = GetExtension(fileData.filename);
                var token = CreateToken();
                var diskFilename = strId + "-" + token + "." + ext;

                _repository.LinkFileToReport(fileData.serverBugId, diskFilename);

                // Convert data
                byte[] decodedBytes = Convert.FromBase64String (fileData.base64data);

                // Todo : Get path from config
                var currentFolder = Directory.GetCurrentDirectory();
                //var fullPath = $"{currentFolder}/BugReports";
                var fullPath = $"/BugReports";
                Directory.CreateDirectory(fullPath);
                var fullFilePath = fullPath + "/" + diskFilename;

                // Hm report error here ?
                if ( !File.Exists(fullFilePath) )
                {
                    File.WriteAllBytes(fullFilePath, decodedBytes );
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

        private string CreateToken()
        {
            string token = Guid.NewGuid().ToString();
            return token;
        }

        private string GetExtension(string filename)
        {
            return "png";
        }
    }
}
