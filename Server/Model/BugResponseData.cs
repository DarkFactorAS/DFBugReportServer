using System;
using System.Collections.Generic;

namespace BugReportServer.Model
{
    public class BugReponseData
    {
        public uint clientBugId{ get; set;}
        public uint serverBugId{ get; set;}
        public BugReponseData(uint clientBugId, uint serverBugId)
        {
            this.clientBugId = clientBugId;
            this.serverBugId = serverBugId;
        }
    }
}
