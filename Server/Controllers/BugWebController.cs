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
        IBugReportProvider _provider;

        public BugWebController(IBugReportProvider provider)
        {
            _provider = provider;
        }

        [HttpGet]
        [Route("GetList")]
        public BugReportListModel GetList()
        {
            BugReportListModel model = new BugReportListModel(200,"");
            model.bugReports = new List<BugReportData>();

            model.bugReports.Add( new BugReportData{
                clientBugId = 1,
                title = "Hardcoded test",
                message = "Little test from"
            });

            return model;
        }

        [HttpPut]
        [Route("DeleteBug")]
        public WebAPIData DeleteBug(int bugID)
        {
            //return _provider.AttachFile(fileData);
            return null;
        }
    }
}
