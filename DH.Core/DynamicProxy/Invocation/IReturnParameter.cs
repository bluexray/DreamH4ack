using System;

namespace DH.Core.DynamicProxy
{
    /// <summary>
    ///     返回参数
    /// </summary>
    public interface IReturnParameter
    {
        /// <summary>
        ///     返回值
        /// </summary>
        Object Value { get; set; }

        /// <summary>
        ///     返回值类型
        /// </summary>
        Type ReturnType { get; }
    }
}
