using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using BugReportServer.Model;
using BugReportServer.Provider;

using System.Collections.Generic;
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
        [Route("GetList")]
        public BugReportListModel GetList()
        {
            return _provider.GetAllBugReports();
        }

        [HttpPut]
        [Route("DeleteBugReport")]
        public WebAPIData DeleteBugReport(int bugreportId)
        {
            return _provider.DeleteBugReport(bugreportId);
        }
    }
}
