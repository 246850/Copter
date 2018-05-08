using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;

namespace Copter.Ioc
{
    /// <summary>
    /// AutoFac 容器包装基类
    /// </summary>
    public abstract class ContainerManager
    {
        public IContainer Container { get; private set; }

        protected ContainerManager(IContainer container)
        {
            Container = container;
        }

        public virtual object Resolve(Type type, ILifetimeScope scope = null)
        {
            if (scope == null)
                scope = Scope();
            return scope.Resolve(type);
        }

        public virtual T Resolve<T>() where T : class
        {
            return Resolve<T>(null, null);
        }

        public virtual T Resolve<T>(object serviceKey, ILifetimeScope scope) where T : class
        {
            if (scope == null)
                scope = Scope();
            T instance = serviceKey == null || string.IsNullOrWhiteSpace(serviceKey.ToString()) ? scope.Resolve<T>() : scope.ResolveKeyed<T>(serviceKey);

            return instance;
        }

        public virtual T[] ResolveAll<T>()
        {
            return ResolveAll<T>(null, null);
        }

        public virtual T[] ResolveAll<T>(object serviceKey, ILifetimeScope scope)
        {
            if (scope == null)
                scope = Scope();
            if (serviceKey == null || string.IsNullOrWhiteSpace(serviceKey.ToString()))
                return scope.Resolve<IEnumerable<T>>().ToArray();
            return scope.ResolveKeyed<IEnumerable<T>>(serviceKey).ToArray();
        }

        public abstract ILifetimeScope Scope();
    }
}
