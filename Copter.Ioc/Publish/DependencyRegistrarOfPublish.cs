using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;

namespace Copter.Ioc.Publish
{
    public class DependencyRegistrarOfPublish : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, CopterConfig config)
        {
            builder.RegisterType<DefaultPublisher>().As<IPublisher>();

            List<Type> subscribers = typeFinder.FindClassesOfType(typeof(ISubscriber<>)).ToList();
            foreach (Type subscriber in subscribers)
                builder.RegisterType(subscriber)
                        .As(subscriber.FindInterfaces((type, criteria) =>
                        {
                            bool isMatch = type.IsGenericType && ((Type)criteria).IsAssignableFrom(type.GetGenericTypeDefinition());
                            return isMatch;
                        }, typeof(ISubscriber<>))).InstancePerLifetimeScope();
        }

        public int Order { get { return -1; } }
    }
}
