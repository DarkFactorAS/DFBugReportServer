using System;
using System.Collections.Generic;

namespace BugReportServer.Model
{
    public class BugReportFileData
    {
        public uint serverBugId{ get; set;}
        public uint fileId{ get;set;}
        public string filename { get; set; }
        public string base64data { get; set; }

        public BugReportFileData()
        {
        }
    }
}
