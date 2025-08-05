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
        public static string AppName = "BugReportWeb";
        public static string AppVersion = "0.9.0";

        public static void Main(string[] args)
        {
            var builder = CreateHostBuilder(args).Build();

            try
            {
                IDFLogger<Program> logger = new DFLogger<Program>();
                logger.Startup(Program.AppName, Program.AppVersion);

                IConfigurationHelper configurationHelper = DFServices.GetService<IConfigurationHelper>();
                IBugReportProvider bugReportProvider = DFServices.GetService<IBugReportProvider>();
                var config = configurationHelper.Settings as BugReportConfig;

                string msg = string.Format("Ping BugReportServer : {0}", config.BugReportServer);
                DFLogger.LogOutput(DFLogLevel.INFO, AppName, msg);

                var result = bugReportProvider.PingServer();
                DFLogger.LogOutput(DFLogLevel.INFO, AppName, "BugReportServer:" + result );
            }
            catch (Exception ex)
            {
                DFLogger.LogOutput(DFLogLevel.WARNING, "Startup", ex.ToString());
            }

            builder.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddTransient<IConfigurationHelper, ConfigurationHelper<BugReportConfig> >();

                new DFServices(services)
                    .SetupLogger()
                    .LogToConsole(DFLogLevel.INFO)
                ;

                services.AddTransient<IBugReportProvider, BugReportProvider>();
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            ;
    }
}
