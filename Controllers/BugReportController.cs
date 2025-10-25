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
    public class BugReportController : DFRestServerController
    {
        IBugReportAPIProvider _provider;

        public BugReportController(IBugReportAPIProvider provider)
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

        public override string Version()
        {
            return Program.AppVersion;
        }
    }
}
