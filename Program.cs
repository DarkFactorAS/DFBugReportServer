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
using DFCommonLib.DataAccess;

using BugReportServer.Repository;
using BugReportServer.Provider;

namespace BugReportServer
{
    public class Program
    {
        public static string AppName = "BugReportServer";
        public static string AppVersion = "1.2.0";

        public static void Main(string[] args)
        {
            var builder = CreateHostBuilder(args).Build();
            try
            {
                IDFLogger<Program> logger = new DFLogger<Program>();
                logger.Startup(Program.AppName, Program.AppVersion);

                IConfigurationHelper configurationHelper = DFServices.GetService<IConfigurationHelper>();
                var config = configurationHelper.Settings;
                var msg = string.Format("Connecting to DB : {0}:{1}", config.DatabaseConnection.Server, config.DatabaseConnection.Port);
                DFLogger.LogOutput(DFLogLevel.INFO, AppName, msg);

                // Run database script
                IStartupDatabasePatcher startupRepository = DFServices.GetService<IStartupDatabasePatcher>();
                startupRepository.WaitForConnection();
                if (startupRepository.RunPatcher())
                {
                    DFLogger.LogOutput(DFLogLevel.INFO, "Startup", "Database patcher ran successfully");
                }
                else
                {
                    DFLogger.LogOutput(DFLogLevel.ERROR, "Startup", "Database patcher failed");
                    Environment.Exit(1);
                    return;
                }

                builder.Run();
            }
            catch (Exception ex)
            {
                DFLogger.LogOutput(DFLogLevel.WARNING, "Startup", ex.ToString());
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                DFServices.Create(services);

                services.AddTransient<IConfigurationHelper, ConfigurationHelper<AppSettings>>();

                new DFServices(services)
                    .SetupLogger()
                    .SetupMySql()
                    .LogToConsole(DFLogLevel.INFO)
                    .LogToMySQL(DFLogLevel.IMPORTANT)
                    //.LogToEvent(DFLogLevel.ERROR, AppName);
                ;

                services.AddTransient<IStartupDatabasePatcher, BugReportDatabasePatcher>();
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
