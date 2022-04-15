using System;
using System.Collections.Generic;
using DFCommonLib.HttpApi;

namespace BugReportServer.Model
{
    public class BugReportExtendedData : WebAPIData
    {
        public BugReportData bugReport{ get; set; }
        public string imageBase64Data { get; set; }

        public BugReportExtendedData() : base(0,"")
        {
        }

        public BugReportExtendedData(int errorCode, string message ) : base(errorCode,message)
        {
        }
    }
}
