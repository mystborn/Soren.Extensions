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

        public static void AddConsoleTarget(LogAdapter logger, bool includeLogLevel = true)
        {
            logger.LogTrace += (value, ctx) =>
            {
                if (includeLogLevel)
                    Console.Write("[Trace] ");
                Console.WriteLine(value);
            };

            logger.LogDebug += (value, ctx) =>
            {
                if (includeLogLevel)
                    Console.Write("[Debug] ");
                Console.WriteLine(value);
            };

            logger.LogInfo += (value, ctx) =>
            {
                if (includeLogLevel)
                    Console.Write("[Info] ");
                Console.WriteLine(value);
            };

            logger.LogWarning += (value, ctx) =>
            {
                if (includeLogLevel)
                    Console.Write("[Warning] ");
                Console.WriteLine(value);
            };

            logger.LogError += (value, ctx) =>
            {
                if (includeLogLevel)
                    Console.Write("[Error] ");
                Console.WriteLine(value);
            };

            logger.LogFatal += (value, ctx) =>
            {
                if (includeLogLevel)
                    Console.Write("[Fatal] ");
                Console.WriteLine(value);
            };
        }

        public static void AddColorConsoleTarget(LogAdapter logger, bool includeLogLevel = true, bool wholeLineColored = false)
        {
            void WriteValue(string value)
            {
                if(wholeLineColored)
                {
                    Console.WriteLine(value);
                    Console.ResetColor();
                }
                else
                {
                    Console.ResetColor();
                    Console.WriteLine(value);
                }
            }

            logger.LogTrace += (value, ctx) =>
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                if(includeLogLevel)
                    Console.Write("[Trace] ");
                WriteValue(value);
            };

            logger.LogDebug += (value, ctx) =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                if (includeLogLevel)
                    Console.Write("[Debug] ");
                WriteValue(value);
            };

            logger.LogInfo += (value, ctx) =>
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                if (includeLogLevel)
                    Console.Write("[Info] ");
                WriteValue(value);
            };

            logger.LogWarning += (value, ctx) =>
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                if (includeLogLevel)
                    Console.Write("[Warning] ");
                WriteValue(value);
            };

            logger.LogError += (value, ctx) =>
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                if (includeLogLevel)
                    Console.Write("[Error] ");
                WriteValue(value);
            };

            logger.LogFatal += (value, ctx) =>
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.DarkRed;
                if (includeLogLevel)
                    Console.Write("[Fatal] ");
                WriteValue(value);
            };
        }

        public static void AddDebugTarget(LogAdapter logger, bool includeLogLevel = true)
        {
            logger.LogTrace += (value, ctx) =>
            {
                if (includeLogLevel)
                    System.Diagnostics.Debug.Write("[Trace] ");
                System.Diagnostics.Debug.WriteLine(value);
            };

            logger.LogDebug += (value, ctx) =>
            {
                if (includeLogLevel)
                    System.Diagnostics.Debug.Write("[Debug] ");
                System.Diagnostics.Debug.WriteLine(value);
            };

            logger.LogInfo += (value, ctx) =>
            {
                if (includeLogLevel)
                    System.Diagnostics.Debug.Write("[Info] ");
                System.Diagnostics.Debug.WriteLine(value);
            };

            logger.LogWarning += (value, ctx) =>
            {
                if (includeLogLevel)
                    System.Diagnostics.Debug.Write("[Warning] ");
                System.Diagnostics.Debug.WriteLine(value);
            };

            logger.LogError += (value, ctx) =>
            {
                if (includeLogLevel)
                    System.Diagnostics.Debug.Write("[Error] ");
                System.Diagnostics.Debug.WriteLine(value);
            };

            logger.LogFatal += (value, ctx) =>
            {
                if (includeLogLevel)
                    System.Diagnostics.Debug.Write("[Fatal] ");
                System.Diagnostics.Debug.WriteLine(value);
            };
        }

        public static LogAdapter ConsoleAdapter(bool includeLogLevel = true)
        {
            var logger = new LogAdapter();
            AddConsoleTarget(logger, includeLogLevel);
            return logger;
        }

        public static LogAdapter ColorConsoleAdapter(bool includeLogLevel = true, bool wholeLineColored = false)
        {
            var logger = new LogAdapter();
            AddColorConsoleTarget(logger, includeLogLevel, wholeLineColored);
            return logger;
        }

        public static LogAdapter DebugAdapter(bool includeLogLevel = true)
        {
            var logger = new LogAdapter();
            AddDebugTarget(logger, includeLogLevel);
            return logger;
        }
    }
}
