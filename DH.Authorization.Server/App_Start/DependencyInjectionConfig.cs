﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using DH.Authorization.Server.Providers;

namespace DH.Authorization.Server.App_Start
{
    public class DependencyInjectionConfig
    {
        /// <summary>
        ///注册依赖注入
        /// </summary>
        public static void Register()
        {
            var builder = new ContainerBuilder();
            //注册api容器的实现
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            //注册mvc容器的实现
            //builder.RegisterControllers(Assembly.GetExecutingAssembly());
            //var assemblys = BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToList();
            //builder.RegisterAssemblyTypes(assemblys.ToArray()).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces();
            //builder.RegisterAssemblyTypes(assemblys.ToArray()).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces();
            //注册 Password Grant 授权服务
            builder.RegisterType<PasswordAuthorizationProvider>().AsSelf().SingleInstance();
            builder.RegisterType<RefreshAuthenticationTokenProvider>().AsSelf().SingleInstance();
            //注册  Credential Grant Password 
           // builder.RegisterType<ClientAuthorizationServerProvider>().AsSelf().SingleInstance();
            //builder.RegisterType<AccessTokenAuthorizationServerProvider>().AsSelf().SingleInstance();
            //在Autofac中注册Redis的连接，并设置为Singleton (官方建議保留Connection，重複使用)
            //builder.Register(r =>{ return ConnectionMultiplexer.Connect(DBSetting.Redis);}).AsSelf().SingleInstance();
            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}