
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DFCommonLib.Config;
using DFCommonLib.Logger;
using DFCommonLib.HttpApi;

using BugReportWeb;

namespace BugReportWeb
{
    public interface IBugReportProvider : IDFRestClient
    {
        void SetEndpoint(string endpoint);
        Task<BugReportListModel> GetAllBugReports();
        Task<BugReportExtendedModel> GetBugReport(int bugReportId);
        Task<WebAPIData> DeleteBugReport(int bugReportId);
    }

    public class BugReportProvider : DFRestClient, IBugReportProvider
    {
        private const int GET_LIST = 1;
        private const int GET_BUGREPORT = 2;
        private const int DELETE_BUGREPORT = 3;
        private string _endpoint;

        public BugReportProvider(IDFLogger<DFRestClient> logger, IConfigurationHelper configuration) : base(logger)
        {
            var customer = configuration.GetFirstCustomer() as BugReportWebCustomer;
            if ( customer != null )
            {
                SetEndpoint(customer.BugReportServer);
            }
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

        public async Task<WebAPIData> DeleteBugReport(int bugReportId)
        {
            var result = await PutJsonData(DELETE_BUGREPORT, "DeleteBugReport?bugReportId=" + bugReportId, "");
            return result;
        }
    }
}