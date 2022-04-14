using System.Collections.Generic;
using DFCommonLib.HttpApi;

namespace BugReportServer.Model
{
     public class BugReportListModel : WebAPIData
    {
        public IList<BugReportData> bugReports { get; set; }

        public BugReportListModel(int errorCode, string message ) : base(errorCode,message)
        {
        }
    }
}