
using System.Collections.Generic;
using DFCommonLib.HttpApi;

namespace BugReportWeb
{
    public class BugReportListModel : WebAPIData
    {
        public IList<BugReportModel> bugReports { get; set; }

        public BugReportListModel()
        {
        }

        public BugReportListModel(int errorCode, string message ) : base(errorCode,message)
        {
        }
    }

    public class BugReportModel
    {
        public int id;
        public string title;
        public string message;
    }
}