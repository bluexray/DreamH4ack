using System;
using System.Threading.Tasks;
using DH.Authorization.Server;
using Microsoft.Owin;
using Owin;


[assembly: OwinStartup(typeof(Startup))]

namespace DH.Authorization.Server
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Test code:
            //app.Run(context =>
            //{
            //    context.Response.ContentType = "text/plain";
            //    return context.Response.WriteAsync("Hello, world.");
            //});

            ConfigureAuth(app);
            // 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
