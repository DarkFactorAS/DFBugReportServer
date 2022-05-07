using System;
using System.Collections.Generic;

namespace BugReportServer.Model
{
    public class BugReponseData
    {
        public uint clientBugId{ get; set;}
        public uint serverBugId{ get; set;}
        public string message{ get; set;}
        public BugReponseData(uint clientBugId, uint serverBugId, string message)
        {
            this.clientBugId = clientBugId;
            this.serverBugId = serverBugId;
            this.message = message;
        }
    }
}
