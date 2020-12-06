using System;
using System.Collections.Generic;
using System.Text;

namespace MusicApp.Log.Abstract
{
    public interface ILoggerService
    {

        void LogInfo(string message);

        void LogWarning(string message);
        void LogError(string message);

        void LogDebug(string message);

    }
}
