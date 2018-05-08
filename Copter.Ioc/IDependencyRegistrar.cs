using Autofac;

namespace Copter.Ioc
{
    /// <summary>
    /// 依赖注入接口
    /// </summary>
    public interface IDependencyRegistrar
    {
        void Register(ContainerBuilder builder, ITypeFinder typeFinder, CopterConfig config);
        int Order { get; }
    }
}
