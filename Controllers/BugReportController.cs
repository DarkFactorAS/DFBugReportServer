using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using BugReportServer.Model;
using BugReportServer.Repository;

namespace BugReportServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BugReportController : ControllerBase
    {
        ILogger<BugReportController> _logger;
        IBugReportRepository _repository;

        public BugReportController(ILogger<BugReportController> logger, IBugReportRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpPut]
        [Route("ReportBug")]
        public string ReportBug(BugReportData bugReportData)
        {
            return _repository.SaveBugReport(bugReportData);
        }
    }
}
