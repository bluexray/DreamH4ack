using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autofac;
using DH.Core.Dependency;
using System.Reflection;

namespace DH.Core.Initialize
{
    public class BootStrapper : IBootStrapper
    {
        public void Init()
        {

            IContainer container;
            ContainerBuilder builder = new ContainerBuilder();
            Type baseType = typeof(IDependency);
            Assembly[] assemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies()
                                    .Select(Assembly.Load).ToArray();
            assemblies = assemblies.Union(new[] { Assembly.GetExecutingAssembly() }).ToArray();

            builder.RegisterAssemblyTypes(assemblies)
                     .Where(type => baseType.IsAssignableFrom(type) && !type.IsAbstract);

            container = builder.Build();
        }

    }
}
