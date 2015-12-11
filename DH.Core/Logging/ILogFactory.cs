using System;

namespace DH.Core.Logging
{
    public interface ILogFactory
    {
        ILog GetLogger(string typeName);

        ILog GetLogger(Type type);
    }
}
