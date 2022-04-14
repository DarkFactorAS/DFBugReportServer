using System;
using System.Collections.Generic;
using System.Text;

using DFCommonLib.Config;

namespace BugReportWeb
{
    public class BugReportWebSettings : ConfigurationSettings
    {
        public new BugReportWebConfiguration CustomerSettings { get; set; }
    }
}
