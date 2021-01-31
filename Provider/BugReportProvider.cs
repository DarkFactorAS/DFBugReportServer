using System;
using System.IO;
using System.Collections.Generic;

using BugReportServer.Model;
using BugReportServer.Repository;

namespace BugReportServer.Provider
{
    public interface IBugReportProvider
    {
        BugReponseData ReportBug(BugReportData bugReportData);
        BugReponseFileData AttachFile(BugReportFileData fileData);
    }

    public class BugReportProvider : IBugReportProvider
    {
        IBugReportRepository _repository;

        public BugReportProvider(IBugReportRepository repository)
        {
            _repository = repository;
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
                var strId = string.Format("{0}",fileData.serverBugId);
                var ext = GetExtension(fileData.filename);
                var token = CreateToken();
                var diskFilename = strId + "-" + token + "." + ext;

                _repository.LinkFileToReport(fileData.serverBugId, diskFilename);

                // Convert data
                byte[] decodedBytes = Convert.FromBase64String (fileData.base64data);

                // Todo : Get path from config
                var currentFolder = Directory.GetCurrentDirectory();
                var fullPath = $"{currentFolder}/BugReports";
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
