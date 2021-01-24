
using BugReportServer.Model;
using BugReportServer.Repository;

namespace BugReportServer.Provider
{
    public interface IBugReportProvider
    {
        BugReponseData ReportBug(BugReportData bugReportData);
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

        private BugReportData VerifyFields(BugReportData bugReportData)
        {
            bugReportData.title = VerifyString(bugReportData.title);
            bugReportData.description = VerifyString( bugReportData.description );
            bugReportData.email = VerifyString( bugReportData.email );
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
