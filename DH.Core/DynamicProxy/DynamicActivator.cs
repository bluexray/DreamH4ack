using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DH.Core.DynamicProxy
{
    public sealed class DynamicActivator
    {

        /// <summary>
        ///     使用指定类型的默认构造函数来创建该类型的代理实例。
        /// </summary>
        public static object CreateInstance(Type type)
        {
            return CreateInstance(type, null);
        }

        private static object CreateInstance(Type type, object p)
        {
            var proxyType = DynamicTypeCacheManager.GetCache(type);
            return InstanceCacheManger.Cache(proxyType, args);
        }
    }
}
