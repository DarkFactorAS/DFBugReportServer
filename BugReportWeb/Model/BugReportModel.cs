
using System.Collections.Generic;
using DFCommonLib.HttpApi;
using System;
using System.Collections.Generic;

namespace BugReportWeb
{
    public class BugReportModel
    {

        public uint clientBugId{ get; set;}
        public string title { get; set; }
        public string message { get; set; }
        public string email { get; set; }
        public string clientName { get; set; }
        public string clientVersion { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
    }
}