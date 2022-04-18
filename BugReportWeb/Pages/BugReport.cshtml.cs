using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BugReportWeb.Pages
{
    public class BugReportPageModel : PageModel
    {
        private readonly ILogger<BugReportPageModel> _logger;
        private readonly IBugReportProvider _bugReportProvider;

        public BugReportModel bugReport;
        public string bugReportImage;

        public BugReportPageModel(ILogger<BugReportPageModel> logger, IBugReportProvider bugReportProvider)
        {
            _logger = logger;
            _bugReportProvider = bugReportProvider;
        }

        public void OnGet(int bugReportId)
        {
            var data = _bugReportProvider.GetBugReport(bugReportId).Result;
            if ( data.errorCode == 0 )
            {
                bugReport = data.bugReport;
                if ( data.imageBase64Data != null )
                {
                    //var imageData = Convert.FromBase64String (base64data);
                    bugReportImage = string.Format("data:image/png;base64,{0}", data.imageBase64Data);  
                }
            }
        }

        public void DeleteBugReport(int bugReportId)
        {
            _bugReportProvider.DeleteBugReport(bugReportId);
        }
    }
}
