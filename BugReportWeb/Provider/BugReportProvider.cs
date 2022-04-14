
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
    }

    public class BugReportProvider : DFRestClient, IBugReportProvider
    {
        private const int GET_LIST = 1;
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
            var data = await GetJsonData<BugReportListModel>(GET_LIST,"GetList");
            return (BugReportListModel) data;
        }
    }
}