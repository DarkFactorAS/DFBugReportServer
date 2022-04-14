using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using BugReportServer.Model;
using BugReportServer.Provider;

namespace BugReportServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BugReportController : ControllerBase
    {
        IBugReportProvider _provider;

        public BugReportController(IBugReportProvider provider)
        {
            _provider = provider;
        }

        [HttpPut]
        [Route("ReportBug")]
        public BugReponseData ReportBug(BugReportData bugReportData)
        {
            return _provider.ReportBug(bugReportData);
        }

        [HttpPut]
        [Route("AttachFile")]
        public BugReponseFileData AttachFile(BugReportFileData fileData)
        {
            return _provider.AttachFile(fileData);
        }
    }
}
