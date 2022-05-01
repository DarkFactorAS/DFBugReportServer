using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using DFCommonLib.Config;
using DFCommonLib.Utils;
using DFCommonLib.Logger;

namespace BugReportWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = CreateHostBuilder(args).Build();

/*
            try
            {
                IConfigurationHelper configuration = DFServices.GetService<IConfigurationHelper>();
                IBugReportProvider bugReportProvider = DFServices.GetService<IBugReportProvider>();
                var customer = configuration.GetFirstCustomer() as BugReportWebCustomer;
                if ( customer != null )
                {
                    bugReportProvider.SetEndpoint(customer.BugReportServer);
                }
            }
            catch(Exception ex)
            {
                DFLogger.LogOutput(DFLogLevel.WARNING, "Startup", ex.ToString() );
            }
            */

            builder.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddTransient<IConfigurationHelper, ConfigurationHelper<BugReportWebCustomer> >();
                    services.AddTransient<IBugReportProvider, BugReportProvider>();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            ;
    }
}
