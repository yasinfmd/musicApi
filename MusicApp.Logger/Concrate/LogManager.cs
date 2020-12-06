using MusicApp.Logger.Abstract;
using Serilog;
using System;

namespace MusicApp.Logger.Concrate
{
    public class LogManager : ILogService
    {

        public LogManager()
        {
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("logs/log.txt", fileSizeLimitBytes: null, buffered: true, rollingInterval: RollingInterval.Day,
                 restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
                 outputTemplate: "{Timestamp:dd-MM-yyyy HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                 retainedFileCountLimit: null
                 ).CreateLogger();
        }
        public void LogDebug(string message)
        {
            Log.Debug(message);
            Log.CloseAndFlush();
        }

        public void LogError(Exception exception, string message)
        {
            Log.Error(exception, message);
            Log.CloseAndFlush();
        }

        public void LogInfo(string message)
        {
            Log.Information(message);
            Log.CloseAndFlush();
        }

        public void LogWarning(string message)
        {
            Log.Warning(message);
            Log.CloseAndFlush();
        }
    }
}
