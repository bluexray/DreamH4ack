﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DH.Core.Logging;
using DH.Nlog;
using NLog;
using NLog.Config;

namespace DH.LogTest
{
    class Program
    {
        static void Main(string[] args)
        {


            ConfigurationItemFactory.Default.Targets
                            .RegisterDefinition("ElasticSearch", typeof(DH.Nlog.ElasticSearch.ElasticSearchTarget));



            Program p = new Program();
            p.logtest();

            Console.ReadKey();

        }

        public void logtest()
        {
            string filename = "log4net.config";


            //默认为在web。config中配置,当传入true的时候
            // LogManager.LogFactory = new Log4NetFactory(filename);

            DH.Core.Logging.LogManager.LogFactory = new NlogFactory();


            string message = "Error Message";
            Exception ex = new Exception();
            string messageFormat = "Message Format: message: {0}, exception: {1}";

            //ILog log = new Log4NetWrapper(GetType());


            //ILog log = LogManager.GetLogger(GetType());

            //ILog log = LogManager.GetLogger("loginfo");




            var log =DH.Core.Logging.LogManager.GetLogger(GetType());

            

            
            //log.Debug(new
            //{
            //    application_name ="logtest",
            //    Type = "Request",
            //    Method = "test",
            //    Level = "Debug",
            //    Message=message
            //});


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
