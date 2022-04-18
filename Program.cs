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
using DFCommonLib.IO;
using BugReportServer.Repository;
using BugReportServer.Provider;

namespace BugReportServer
{
    public class Program
    {
        public static string AppName = "BugReportServer";

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddTransient<IConfigurationHelper, ConfigurationHelper>();

                new DFServices(services)
                    .SetupLogger()
                    .SetupMySql()
                    .LogToConsole(DFLogLevel.INFO)
                    .LogToMySQL(DFLogLevel.IMPORTANT)
                    //.LogToEvent(DFLogLevel.ERROR, AppName);
                ;

                services.AddTransient<IBugReportAPIProvider, BugReportAPIProvider>();
                services.AddTransient<IBugReportWebProvider, BugReportWebProvider>();
                services.AddTransient<IBugReportRepository, BugReportRepository>();
                services.AddTransient<IBugReportWebRepository, BugReportWebRepository>();
                services.AddTransient<IFileHandler, FileHandler>();
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            }
        );
    }
}
