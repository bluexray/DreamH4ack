using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DH.Core.Logging;
using NLog;

namespace DH.Nlog
{
    public class NlogWrapper:ILog
    {

        private readonly NLog.Logger log;

        public NlogWrapper(string typeName)
        {
            log = NLog.LogManager.GetLogger(typeName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NlogWrapper"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public NlogWrapper(Type type)
        {
            log = NLog.LogManager.GetLogger(UseFullTypeNames ? type.FullName : type.Name);
        }

        public static bool UseFullTypeNames { get; set; }

        public bool IsDebugEnabled { get { return log.IsDebugEnabled; } }

        public bool IsInfoEnabled { get { return log.IsInfoEnabled; } }

        public bool IsWarnEnabled { get { return log.IsWarnEnabled; } }

        public bool IsErrorEnabled { get { return log.IsErrorEnabled; } }

        public bool IsFatalEnabled { get { return log.IsFatalEnabled; } }

        private static string AsString(object message)
        {
            return message != null ? message.ToString() : null;
        }

        /// <summary>
        /// Logs a Debug message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Debug(object message)
        {
            if (IsDebugEnabled)
                Log(LogLevel.Debug, AsString(message));
        }

        /// <summary>
        /// Logs a Debug message and exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public void Debug(object message, Exception exception)
        {
            if (IsDebugEnabled)
                Log(LogLevel.Debug, AsString(message), exception);
        }

        /// <summary>
        /// Logs a Debug format message.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        public void DebugFormat(string format, params object[] args)
        {
            if (IsDebugEnabled)
                Log(LogLevel.Debug, format, args);
        }

        /// <summary>
        /// Logs a Error message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Error(object message)
        {
            if (IsErrorEnabled)
                Log(LogLevel.Error, AsString(message));
        }

        /// <summary>
        /// Logs a Error message and exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public void Error(object message, Exception exception)
        {
            if (IsErrorEnabled)
                Log(LogLevel.Error, AsString(message), exception);
        }

        /// <summary>
        /// Logs a Error format message.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        public void ErrorFormat(string format, params object[] args)
        {
            if (IsErrorEnabled)
                Log(LogLevel.Error, format, args);
        }

        /// <summary>
        /// Logs a Fatal message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Fatal(object message)
        {
            if (IsFatalEnabled)
                Log(LogLevel.Fatal, AsString(message));
        }

        /// <summary>
        /// Logs a Fatal message and exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public void Fatal(object message, Exception exception)
        {
            if (IsFatalEnabled)
                Log(LogLevel.Fatal, AsString(message), exception);
        }

        /// <summary>
        /// Logs a Error format message.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        public void FatalFormat(string format, params object[] args)
        {
            if (IsFatalEnabled)
                Log(LogLevel.Fatal, format, args);
        }

        /// <summary>
        /// Logs an Info message and exception.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Info(object message)
        {
            if (IsInfoEnabled)
                Log(LogLevel.Info, AsString(message));
        }

        /// <summary>
        /// Logs an Info message and exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public void Info(object message, Exception exception)
        {
            if (IsInfoEnabled)
                Log(LogLevel.Info, AsString(message), exception);
        }

        /// <summary>
        /// Logs an Info format message.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        public void InfoFormat(string format, params object[] args)
        {
            if (IsInfoEnabled)
                Log(LogLevel.Info, format, args);
        }

        /// <summary>
        /// Logs a Warning message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Warn(object message)
        {
            if (IsWarnEnabled)
                Log(LogLevel.Warn, AsString(message));
        }

        /// <summary>
        /// Logs a Warning message and exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public void Warn(object message, Exception exception)
        {
            if (IsWarnEnabled)
                Log(LogLevel.Warn, AsString(message), exception);
        }

        /// <summary>
        /// Logs a Warning format message.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        public void WarnFormat(string format, params object[] args)
        {
            if (IsWarnEnabled)
                Log(LogLevel.Warn, format, args);
        }

        public void Log(NLog.LogLevel logLevel, string format, params object[] args)
        {
            log.Log(typeof(NlogWrapper), new LogEventInfo(logLevel, log.Name, null, format, args));
        }

        public void Log(NLog.LogLevel logLevel, string format, object[] args, Exception ex)
        {
            log.Log(typeof(NlogWrapper), new LogEventInfo(logLevel, log.Name, null, format, args, ex));
        }
    }

}
