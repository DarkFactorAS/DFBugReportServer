using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using DFCommonLib.Config;

namespace BugReportWeb
{
    public class BugReportWebConfigurationHelper : ConfigurationFactory, IConfigurationHelper
    {
        public BugReportWebSettings _configSettings;

        public ConfigurationSettings ConfigurationSettings
        {
            get
            {
                return _configSettings;
            }
        }

        public Customer GetFirstCustomer()
        {
            if (_configSettings != null )
            {
                var customerSetting = _configSettings.CustomerSettings;
                if ( customerSetting != null )
                {
                    var customers = customerSetting.Customers;
                    if ( customers != null )
                    {
                        return customers.FirstOrDefault();
                    }
                }
            }
            return null;
        }

        public BugReportWebConfigurationHelper(IHostEnvironment env) : base(env)
        {
            if (_configSettings == null)
            {
                _configSettings = GetConfigurationFromBuilder(ConfigurationBuilder);
            }
        }

        protected BugReportWebSettings GetConfigurationFromBuilder(IConfiguration builder)
        {
            var configSettings = new BugReportWebSettings();
            builder.Bind(configSettings);
            return configSettings;
        }
    }
}