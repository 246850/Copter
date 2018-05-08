using System;
using Autofac;
using Autofac.Core.Lifetime;

namespace Copter.Ioc.WebApi
{
    /// <summary>
    /// WebApi Autofac 容器
    /// </summary>
    public class WebApiContainerManager : ContainerManager
    {
        public override ILifetimeScope Scope()
        {
            try
            {
                return Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
            }
            catch (Exception)
            {
                return Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
            }
        }

        public WebApiContainerManager(IContainer container) : base(container) { }
    }
}
