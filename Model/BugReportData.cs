using System;
using System.Collections.Generic;

namespace BugReportServer.Model
{
    public class BugReportData
    {
        public uint clientBugId{ get; set;}
        public string title { get; set; }
        public string description { get; set; }
        public string email { get; set; }

        public BugReportData()
        {
        }
    }
}
