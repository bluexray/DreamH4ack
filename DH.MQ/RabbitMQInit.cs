using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RabbitMQ.Client;

namespace DH.MQ
{
    public class RabbitMQInit
    {
        public static string QUEUENAME = "";
        public static XMLConfig config =new XMLConfig();
        public static string EXCHANGE = "";
        public static string TYPE = "";
        public static string RoutingKey = "";

        public static IConnection CreateConnection()
        {
            const ushort heartbeat = 60;//心跳

            if (config==null)
            {
                config = (XMLConfig)new XMlSettingProvider().GetCurrentSetting();
            }


            //config = (XMLConfig)new XMlSettingProvider().GetCurrentSetting();

            QUEUENAME = config.QueueName;
            EXCHANGE = config.Exchange;
            TYPE = config.Type;
            RoutingKey = config.RoutingKey;

             var fact = new ConnectionFactory()
            {
                HostName = config.VHost,
                UserName = config.User,
                Password = config.PWD,
                RequestedHeartbeat =heartbeat,
                AutomaticRecoveryEnabled = true //自动重连
            };

            return fact.CreateConnection();
        }

        public static IModel CreateChannel(IConnection connection)
        {
            //bool durable = false;

            var channel = connection.CreateModel();



            //exchange的4种模式：direct fanout headers topic
            channel.ExchangeDeclare(exchange: EXCHANGE,
                                   type: TYPE);

            return channel;
        }
    }
}
