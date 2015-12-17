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
        /// <summary>
        /// Initializes a new instance of the <see cref="NlogFactory"/> class.
        /// </summary>
        public NlogFactory() { }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public ILog GetLogger(Type type)
        {
            return new NlogWrapper(type);
        }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <param name="typeName">Name of the type.</param>
        /// <returns></returns>
        public ILog GetLogger(string typeName)
        {
            return new NlogWrapper(typeName);
        }
    }

}
