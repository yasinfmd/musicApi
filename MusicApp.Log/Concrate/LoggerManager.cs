using Microsoft.Extensions.Configuration;
using MusicApp.Log.Abstract;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
namespace MusicApp.Log.Concrate
{
    public class LoggerManager : ILoggerService
    {


        public void LogDebug(string message)
        {
            //var log = logger.WriteTo.File("log.txt", fileSizeLimitBytes: null,
            //     rollingInterval: RollingInterval.Day, retainedFileCountLimit: null,
            //     buffered: true,
            //     outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
            //     )
            //(@"C:\testklasor\LogFile.txt"
            //      .CreateLogger();
            //log.Information("Hellow");
            var log = new LoggerConfiguration().MinimumLevel.Information().WriteTo.File("logs/log.txt", fileSizeLimitBytes: null, buffered: true, rollingInterval: RollingInterval.Day,
                 restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
                 outputTemplate: "{Timestamp:dd-MM-yyyy HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                 retainedFileCountLimit: null
                 ).CreateLogger();
       
        }

        public void LogError(string message)
        {
        }

        public void LogInfo(string message)
        {

        }

        public void LogWarning(string message)
        {
        }
    }
}
