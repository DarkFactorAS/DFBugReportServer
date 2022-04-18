
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DFCommonLib.Logger;
using DFCommonLib.HttpApi;

namespace BugReportWeb
{
    public interface IBugReportProvider : IDFRestClient
    {
        Task<BugReportListModel> GetAllBugReports();
        Task<BugReportExtendedModel> GetBugReport(int bugReportId);
        void DeleteBugReport(int bugReportId);
    }

    public class BugReportProvider : DFRestClient, IBugReportProvider
    {
        private const int GET_LIST = 1;
        private const int GET_BUGREPORT = 2;
        private string _endpoint;

        public BugReportProvider(IDFLogger<DFRestClient> logger) : base(logger)
        {
            SetEndpoint("http://localhost:5200");
        }

        override protected string GetModule()
        {
            return "BugWeb";
        }

        public void SetEndpoint(string endpoint)
        {
            _endpoint = endpoint;
        }

        override protected string GetHostname()
        {
            return _endpoint;
        }

        public async Task<BugReportListModel> GetAllBugReports()
        {
            var data = await GetJsonData<BugReportListModel>(GET_LIST,"GetAllBugReports");
            return (BugReportListModel) data;
        }

        public async Task<BugReportExtendedModel> GetBugReport(int bugReportId)
        {
            var data = await GetJsonData<BugReportExtendedModel>(GET_BUGREPORT,"GetBugReport?bugReportId=" + bugReportId);
            return (BugReportExtendedModel) data;
        }

        public void DeleteBugReport(int bugReportId)
        {
        }
    }
}