using System;
using System.Collections.Generic;

namespace BugReportServer.Model
{
    public class BugReportData
    {
        public string title { get; set; }
        public string description { get; set; }
        public string email { get; set; }

        public BugReportData()
        {
        }

        public BugReportData(string title, string description, string email)
        {
            this.title = title;
            this.description = description;
            this.email = email;
        }
    }
}
