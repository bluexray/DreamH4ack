using System;

namespace DH.Core.Logging
{
    internal class NullLogFactory : ILogFactory
    {
        private readonly bool debugEnabled;

        public NullLogFactory(bool debugEnabled = false)
        {
            this.debugEnabled = debugEnabled;
        }

        public ILog GetLogger(Type type)
        {
            return new NullDebugLogger(type) { IsDebugEnabled = debugEnabled };
        }

        public ILog GetLogger(string typeName)
        {
            return new NullDebugLogger(typeName) { IsDebugEnabled = debugEnabled };
        }
    }
}