using MusicApp.Logger.Abstract;
using Serilog;
using System;

namespace MusicApp.Logger.Concrate
{
    public class LogManager : ILogService
    {

        //public LogManager()
        //{

        //}
        public void LogDebug(string message)
        {
            using (var log = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("logs/log.txt", fileSizeLimitBytes: null, buffered: true, rollingInterval: RollingInterval.Day,
           restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
           outputTemplate: "{Timestamp:dd-MM-yyyy HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
           retainedFileCountLimit: null
           ).CreateLogger())
            {
                log.Debug(message);
                Log.CloseAndFlush();
            }

        }

        public void LogError(Exception exception, string message)
        {
            using (var log = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("logs/log.txt", fileSizeLimitBytes: null, buffered: true, rollingInterval: RollingInterval.Day,
            restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
            outputTemplate: "{Timestamp:dd-MM-yyyy HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
            retainedFileCountLimit: null
            ).CreateLogger())
            {
                log.Error(exception,message);
                Log.CloseAndFlush();
            }
        }

        public void LogInfo(string message)
        {
            using (var log = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("logs/log.txt", fileSizeLimitBytes: null, buffered: true, rollingInterval: RollingInterval.Day,
          restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
          outputTemplate: "{Timestamp:dd-MM-yyyy HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
          retainedFileCountLimit: null
          ).CreateLogger())
            {
                log.Information(message);
                Log.CloseAndFlush();
            }

        }

        public void LogWarning(string message)
        {
            using (var log = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("logs/log.txt", fileSizeLimitBytes: null, buffered: true, rollingInterval: RollingInterval.Day,
              restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
              outputTemplate: "{Timestamp:dd-MM-yyyy HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
              retainedFileCountLimit: null
              ).CreateLogger())
            {
                log.Warning(message);
                Log.CloseAndFlush();
            }
        }
    }
}
