namespace DH.Core.DynamicProxy
{
    /// <summary>
    ///     方法返回拦截器
    /// </summary>
    public interface IReturnInterceptor : IInterceptor
    {
        /// <summary>
        ///     方法返回时拦截
        /// </summary>
        /// <param name="invocation"></param>
        void OnReturnExecuted(IReturnInvocation invocation);
    }
}
