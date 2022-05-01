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
        public int errorCode;
        public string errorMessage;

        public BugReportPageModel(ILogger<BugReportPageModel> logger, IBugReportProvider bugReportProvider)
        {
            _logger = logger;
            _bugReportProvider = bugReportProvider;
        }

        public void OnGet(int bugReportId)
        {
            errorCode = 0;
            errorMessage = null;

            var data = _bugReportProvider.GetBugReport(bugReportId).Result;
            if ( data != null && data.errorCode == 0 )
            {
                bugReport = data.bugReport;
                errorMessage = data.message;
                if ( data.imageBase64Data != null )
                {
                    //var imageData = Convert.FromBase64String (base64data);
                    bugReportImage = string.Format("data:image/png;base64,{0}", data.imageBase64Data);  
                }
            } 
        }

        [BindProperty]
        public int bugReportId { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var result = await _bugReportProvider.DeleteBugReport(bugReportId);
            if ( result.IsCodeOK() )
            {
                return RedirectToPage("./Index");
            }

            // Todo show error
             return RedirectToPage("./Index");
            // return RedirectToPage("./BugReport?bugReportId=" + bugReportId);
        }
    }
}
