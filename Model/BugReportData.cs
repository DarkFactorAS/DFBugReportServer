using System;
using System.Collections.Generic;

namespace BugReportServer.Model
{
    public class BugReportData
    {
        public uint clientBugId{ get; set;}
        public string title { get; set; }
        public string message { get; set; }
        public string email { get; set; }
       public string clientName { get; set; }
       public string clientVersion { get; set; }

        public BugReportData()
        {
        }
    }
}
