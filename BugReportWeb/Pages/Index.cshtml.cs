using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BugReportWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IBugReportProvider _bugReportProvider;

        public BugReportListModel model;

        public IndexModel(ILogger<IndexModel> logger, IBugReportProvider bugReportProvider)
        {
            _logger = logger;
            _bugReportProvider = bugReportProvider;
        }

        public void OnGet()
        {
            var data = _bugReportProvider.GetAllBugReports().Result;
            model = data;
        }
    }
}
