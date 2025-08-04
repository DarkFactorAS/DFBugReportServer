using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using DFCommonLib.Config;

namespace BugReportWeb
{
    public class BugReportConfig : AppSettings
    {
        public string BugReportServer { get; set; }
    }
}