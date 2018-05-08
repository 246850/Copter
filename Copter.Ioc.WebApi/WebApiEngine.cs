using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Autofac.Integration.WebApi;

namespace Copter.Ioc.WebApi
{
    /// <summary>
    /// WebApi 引擎
    /// </summary>
    public  class WebApiEngine : CopterEngine
    {
        protected override void RegisterDependencies(CopterConfig config)
        {
            ContainerBuilder builder = new ContainerBuilder();
            IContainer container = builder.Build();
            ContainerManager = new WebApiContainerManager(container);

            //we create new instance of ContainerBuilder
            //because Build() or Update() method can only be called once on a ContainerBuilder.
            builder = new ContainerBuilder();

            ITypeFinder typeFinder = new WebTypeFinder();
            if (config != null)
            {
                builder.RegisterInstance(config).As<CopterConfig>().SingleInstance();
            }
            builder.RegisterInstance(this).As<IEngine>().SingleInstance();
            builder.RegisterInstance(typeFinder).As<ITypeFinder>().SingleInstance();
            
            IEnumerable<Type> dependencyTypes = typeFinder.FindClassesOfType<IDependencyRegistrar>();
            List<IDependencyRegistrar> dependencies = new List<IDependencyRegistrar>();
            foreach (Type type in dependencyTypes)
            {
                IDependencyRegistrar item = (IDependencyRegistrar)Activator.CreateInstance(type);
                dependencies.Add(item);
            }

            dependencies = dependencies.AsQueryable().OrderBy(x => x.Order).ToList();
            foreach (IDependencyRegistrar dependency in dependencies)
                dependency.Register(builder, typeFinder, config);

            builder.Update(container);

            // 注册到 webapi 容器
            HttpConfiguration configuration = GlobalConfiguration.Configuration;
            IDependencyResolver resolver = new AutofacWebApiDependencyResolver(ContainerManager.Container);
            configuration.DependencyResolver = resolver;
        }
    }
}
