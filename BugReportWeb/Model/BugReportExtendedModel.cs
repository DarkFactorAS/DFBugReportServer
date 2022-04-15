using System;
using System.Collections.Generic;
using DFCommonLib.HttpApi;

namespace BugReportWeb
{
    public class BugReportExtendedModel : WebAPIData
    {
        public BugReportModel bugReport{ get; set; }
        public string imageBase64Data { get; set; }

        public BugReportExtendedModel() : base(0,"")
        {
        }

        public BugReportExtendedModel(int errorCode, string message) : base(errorCode,message)
        {
        }

        
    }
}
