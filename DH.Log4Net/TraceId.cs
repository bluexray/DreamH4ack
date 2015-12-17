using System;
using System.IO;
using log4net.Core;
using log4net.Layout.Pattern;

namespace DH.Log4Net
{
    internal sealed class TraceId : PatternLayoutConverter
    {
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            var c = loggingEvent.MessageObject as MessageLog;
            if (c!=null)
            {
                writer.Write(c.TraceID);
            }
        }
    }
}