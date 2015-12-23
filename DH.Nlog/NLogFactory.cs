using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DH.Core.Logging;

namespace DH.Nlog
{
    public class NlogFactory:ILogFactory
    {

        public NlogFactory() { }

        public ILog GetLogger(Type type)
        {
            return new NlogWrapper(type);
        }

        public ILog GetLogger(string typeName)
        {
            return new NlogWrapper(typeName);
        }
    }

}
