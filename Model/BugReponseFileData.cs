using System;
using System.Collections.Generic;

namespace BugReportServer.Model
{
    public class BugReponseFileData
    {
        public uint serverBugId{ get; set;}
        public uint fileId{ get; set;}
        public BugReponseFileData(uint serverBugId, uint fileId)
        {
            this.serverBugId = serverBugId;
            this.fileId = fileId;
        }
    }
}
