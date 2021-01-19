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
using BugReportServer.Repository;

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
                new DFServices(services)
                    .SetupLogger()
                    .SetupMySql()
                    .LogToConsole(DFLogLevel.INFO)
                    .LogToMySQL(DFLogLevel.WARNING)
                    .LogToEvent(DFLogLevel.ERROR, AppName);
                ;

                services.AddTransient<IBugReportRepository, BugReportRepository>();
                services.AddTransient<IConfigurationHelper, ConfigurationHelper>();

            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            }
        );
    }
}
