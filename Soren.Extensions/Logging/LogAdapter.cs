using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soren.Extensions.Logging
{
    public class LogAdapter
    {
        public event Action<string, object?>? LogTrace;
        public event Action<string, object?>? LogDebug;
        public event Action<string, object?>? LogInfo;
        public event Action<string, object?>? LogWarning;
        public event Action<string, object?>? LogError;
        public event Action<string, object?>? LogFatal;

        public void Trace(string message) => Trace(message, null);

        public void Trace(string mesage, object? ctx)
        {
            LogTrace?.Invoke(mesage, ctx);
        }

        public void Debug(string message) => Debug(message, null);

        public void Debug(string mesage, object? ctx)
        {
            LogDebug?.Invoke(mesage, ctx);
        }

        public void Info(string message) => Info(message, null);

        public void Info(string message, object? ctx)
        {
            LogInfo?.Invoke(message, ctx);
        }

        public void Warning(string message) => Warning(message, null);

        public void Warning(string message, object? ctx)
        {
            LogWarning?.Invoke(message, ctx);
        }

        public void Error(string message) => Error(message, null);

        public void Error(string message, object? ctx)
        {
            LogError?.Invoke(message, ctx);
        }

        public void Fatal(string message) => Fatal(message, null);

        public void Fatal(string message, object? ctx)
        {
            LogFatal?.Invoke(message, ctx);
        }

        public static LogAdapter ConsoleAdapter()
        {
            var logger = new LogAdapter();
            logger.LogTrace += (value, ctx) => Console.WriteLine($"[Trace] {value}");
            logger.LogDebug += (value, ctx) => Console.WriteLine($"[Debug] {value}");
            logger.LogInfo += (value, ctx) => Console.WriteLine($"[Info] {value}");
            logger.LogWarning += (value, ctx) => Console.WriteLine($"[Warning] {value}");
            logger.LogError += (value, ctx) => Console.WriteLine($"[Error] {value}");
            logger.LogTrace += (value, ctx) => Console.WriteLine($"[Trace] {value}");
            return logger;
        }
    }
}
