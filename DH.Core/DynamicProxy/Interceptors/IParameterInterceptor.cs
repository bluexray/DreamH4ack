namespace DH.Core.DynamicProxy
{
    /// <summary>
    ///     方法参数拦截器
    /// </summary>
    public interface IParameterInterceptor : IInterceptor
    {
        /// <summary>
        ///     方法传入参数时拦截
        /// </summary>
        void OnParameterExecuting(IParameterInvocation invocation);
    }
}
