using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DH.MQ
{
    public class XMLConfig : ConfigSetting
    {
        public string VHost { get; set; }
        public bool IsAck { get; set; }
        public bool IsDurability { get; set; }
        public string User { get; set; }

        public string PWD { get; set; }

        public string QueueName { get; set; }

        public string Exchange { get; set; }

        public string Type { get; set; }

        public string RoutingKey { get; set; }

    }
}
