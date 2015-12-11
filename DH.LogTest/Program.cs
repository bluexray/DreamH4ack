using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DH.Log4Net;
using DH.Core.Logging;

namespace DH.LogTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            p.logtest();

        }

        public void logtest()
        {
            string filename = "log4net.config";


            //默认为在web。config中配置,当传入true的时候
            LogManager.LogFactory = new Log4NetFactory(filename);



            string message = "Error Message";
            Exception ex = new Exception();
            string messageFormat = "Message Format: message: {0}, exception: {1}";

            //ILog log = new Log4NetWrapper(GetType());

        
            ILog log = LogManager.GetLogger(GetType());


            log.Debug(message);
            log.Debug(message, ex);
            log.DebugFormat(messageFormat, message, ex.Message);

            log.Error(message);
            log.Error(message, ex);
            log.ErrorFormat(messageFormat, message, ex.Message);

            log.Fatal(message);
            log.Fatal(message, ex);
            log.FatalFormat(messageFormat, message, ex.Message);

            log.Info(message);
            log.Info(message, ex);
            log.InfoFormat(messageFormat, message, ex.Message);

            log.Warn(message);
            log.Warn(message, ex);
            log.WarnFormat(messageFormat, message, ex.Message);
        }
    }
}
