using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DH.MQ;
using DH.Core.Dependency;
using Autofac;
using System.Reflection;
using DH.Core;

namespace DH.Console.Test
{

    public enum ProviderType
    {
        RabbitMQ,
        MSMQ
    }
    class Program
    {
        public static IContainer container;
        static void Main(string[] args)
        {
            //RMQProvider.CreateInstace();
            //RMQProvider.Connected();

            //ReviceMessage();

            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<RMQProvider>().As<IProvider>();

            var c = new XMLConfig { IsAck = false };



            //builder.RegisterType<RMQProvider>();

            //var baseType = typeof(IProvider);


            Assembly[] assemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies()
                        .Select(Assembly.Load).ToArray();
            assemblies = assemblies.Union(new[] { Assembly.GetExecutingAssembly() }).ToArray();


            //builder.RegisterAssemblyTypes(assemblies)
            //    .Where(type => baseType.IsAssignableFrom(type) && !type.IsAbstract);
            //.AsSelf() //自身服务，用于没有接口的类
            //.AsImplementedInterfaces() //接口服务
            //.PropertiesAutowired(); //属性注入;


            //builder.RegisterAssemblyTypes(assemblies).As<IProvider>();

            // builder.RegisterAssemblyTypes(assemblies).AsImplementedInterfaces();


            container = builder.Build();

            //var x = container.Resolve<RMQProvider>();

            var q = container.Resolve<IProvider>();

            RMQProvider x = new RMQProvider();

            //x.CreateBus().Publish(new TextMessage
            //    { Text= "rabbitmq  test on dh framework!" }
            //) ;
            x.SendMessage(new TextMessage { Text = "rabbitmq procuster test" }, "lu9.rabbitMQ", "lu9");


            //q.Connected();

            

            //q.SendMessage(new TextMessage
            //                   { Text= "rabbitmq  test on dh framework!" } 
            //            );
            //System.Console.ReadKey();
           


            //builder.RegisterType<RMQProvider>().Keyed<IProvider>(ProviderType.RabbitMQ);


            //var s = (RMQProvider)container.ResolveKeyed<IProvider>(ProviderType.RabbitMQ);
            
           

        }
    }


}
