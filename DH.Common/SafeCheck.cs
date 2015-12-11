using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DH.Common
{
    public static class SafeCheck
    {
        /// <summary>
        ///     当为空时，报参数不能为空的异常信息
        /// </summary>
        /// <param name="isTrue">为true时，报异常</param>
        /// <param name="parameterName">参数名称</param>
        public static void IsTure(bool isTrue, string parameterName)
        {
            if (isTrue) { throw new Exception(parameterName); }
        }

        /// <summary>
        ///     当为空时，报参数不能为空的异常信息
        /// </summary>
        /// <typeparam name="T">任何值</typeparam>
        /// <param name="value">任何值</param>
        /// <param name="parameterName">参数名称</param>
        public static T NotNull<T>(T value, string parameterName) where T : class
        {
            IsTure(value == null, parameterName);
            return value;
        }

        /// <summary>
        ///     当为空时，报参数不能为空的异常信息
        /// </summary>
        /// <typeparam name="T">任何值</typeparam>
        /// <param name="value">任何值</param>
        /// <param name="parameterName">参数名称</param>
        public static T? NotNull<T>(T? value, string parameterName) where T : struct
        {
            IsTure(value == null, parameterName);
            return value;
        }

        /// <summary>
        ///     当为空时，报参数不能为空的异常信息
        /// </summary>
        /// <param name="value">任何值</param>
        /// <param name="parameterName">参数名称</param>
        public static string NotEmpty(string value, string parameterName)
        {
            IsTure(string.IsNullOrWhiteSpace(value), parameterName);
            return value;
        }
    }
}
