using System;
using System.Collections.Generic;
using System.Text;

namespace MusicApp.Logger.Abstract
{
    public interface ILogService
    {
        void LogInfo(string message);

        void LogWarning(string message);
        void LogError(Exception exception, string message);

        void LogDebug(string message);
    }
}
