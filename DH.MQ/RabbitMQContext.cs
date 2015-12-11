using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace DH.MQ
{
    public class RabbitMQContext
    {

        public IConnection SendConnection { get; set; }

        public IConnection ListenConnection { get; set; }

        public IModel SendChannel { get; set; }

        public IModel ListenChannel { get; set; }
    }
}
