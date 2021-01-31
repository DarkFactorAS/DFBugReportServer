using System;
using System.Collections.Generic;

using BugReportServer.Model;
using BugReportServer.Repository;

namespace BugReportServer.Provider
{
    public interface IBugReportProvider
    {
        BugReponseData ReportBug(BugReportData bugReportData);
        bool AttachFile(BugReportFileData fileData);
    }

    public class BugReportProvider : IBugReportProvider
    {
        IBugReportRepository _repository;

        public BugReportProvider(IBugReportRepository repository)
        {
            _repository = repository;
        }

        public BugReponseData ReportBug(BugReportData bugReportData)
        {
            bugReportData = VerifyFields(bugReportData);
            uint serverId = _repository.SaveBugReport(bugReportData);
            return new BugReponseData( bugReportData.clientBugId, serverId );
        }

        public bool AttachFile(BugReportFileData fileData)
        {
            var ext = GetExtension(fileData.filename);
            var token = CreateToken();
            var diskFilename = token + "." + ext;

            _repository.LinkFileToReport(fileData.serverBugId, diskFilename);

            // Todo : Save to disk

            return false;
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
