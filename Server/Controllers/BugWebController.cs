using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using BugReportServer.Model;
using BugReportServer.Provider;

using DFCommonLib.HttpApi;

namespace BugReportServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BugWebController : ControllerBase
    {
        IBugReportWebProvider _provider;

        public BugWebController(IBugReportWebProvider provider)
        {
            _provider = provider;
        }

        [HttpGet]
        [Route("PingServer")]
        public string PingServer()
        {
            return _provider.PingServer();
        }

        [HttpGet]
        [Route("GetAllBugReports")]
        public BugReportListModel GetAllBugReports()
        {
            return _provider.GetAllBugReports();
        }

        [HttpGet]
        [Route("GetBugReport")]
        public BugReportExtendedData GetBugReport(int bugreportId)
        {
            return _provider.GetBugReport(bugreportId);
        }

        [HttpPut]
        [Route("DeleteBugReport")]
        public WebAPIData DeleteBugReport(int bugReportId)
        {
            return _provider.DeleteBugReport(bugReportId);
        }
    }
}
