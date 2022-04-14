using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

using DFCommonLib.Config;

namespace BugReportWeb
{
    public class BugReportWebConfiguration
    {
        public IEnumerable<BugReportWebCustomer> Customers { get; set; }

        public BugReportWebCustomer GetCustomer(string customerName)
        {
            return Customers.Where(x => x.Id == customerName).FirstOrDefault();
        }
    }

    public class BugReportWebCustomer : Customer
    {
        public string BugReportServer { get; set; }
    }
}
