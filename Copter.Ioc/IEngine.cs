using System;

namespace Copter.Ioc
{
    public interface IEngine
    {
        ContainerManager ContainerManager { get; }

        void Initialize(CopterConfig config);

        T Resolve<T>() where T : class;

        T Resolve<T>(object serviceKey) where T : class;

        object Resolve(Type type);

        T[] ResolveAll<T>();

        T[] ResolveAll<T>(object serviceKey);

        #region publish/subscriber

        void Publish<T>(T message) where T : class, new();
        void Publish<T>(T message, object filter) where T : class, new();

        #endregion
    }
}
