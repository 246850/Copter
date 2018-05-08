using System;
using System.Collections.Generic;
using System.Linq;
using Copter.Ioc.Publish;

namespace Copter.Ioc
{
    /// <summary>
    /// 引擎抽象基类
    /// </summary>
    public abstract class CopterEngine : IEngine, IPublisher
    {
        public ContainerManager ContainerManager { get; protected set; }

        public void Initialize(CopterConfig config)
        {
            RegisterDependencies(config);

            RunStartupTasks();
            //if (!config.IgnoreStartupTasks)
            //{
            //    RunStartupTasks();
            //}
        }

        public object Resolve(Type type)
        {
            return ContainerManager.Resolve(type);
        }

        public T Resolve<T>() where T : class
        {
            return Resolve<T>(null);
        }

        public T Resolve<T>(object serviceKey) where T : class
        {
            return ContainerManager.Resolve<T>(serviceKey, null);
        }

        public T[] ResolveAll<T>()
        {
            return ContainerManager.ResolveAll<T>();
        }

        public T[] ResolveAll<T>(object serviceKey)
        {
            return ContainerManager.ResolveAll<T>(serviceKey, null);
        }

        protected virtual void RunStartupTasks()
        {
            ITypeFinder typeFinder = ContainerManager.Resolve<ITypeFinder>();
            IEnumerable<Type> startUpTaskTypes = typeFinder.FindClassesOfType<IStartupTask>();

            List<IStartupTask> startUpTasks = new List<IStartupTask>();
            foreach (Type type in startUpTaskTypes)
            {
                IStartupTask item = (IStartupTask)Activator.CreateInstance(type);
                if (item == null) continue;
                startUpTasks.Add(item);
            }
            startUpTasks = startUpTasks.AsQueryable().OrderBy(x => x.Order).ToList();
            //startUpTasks.Sort(new StartupTaskComparer());

            foreach (IStartupTask task in startUpTasks)
                task.Execute();
        }

        protected abstract void RegisterDependencies(CopterConfig config);

        public void Publish<T>(T message) where T : class, new()
        {
            IPublisher publisher = Resolve<IPublisher>();
            if (publisher == null)
                throw new Exception("应用程序尚未注册发布者实例");
            if (message == null)
                throw new ArgumentNullException("message", "参数message为null");
            publisher.Publish(message);
        }

        public void Publish<T>(T message, object filter) where T : class, new()
        {
            IPublisher publisher = Resolve<IPublisher>();
            if (publisher == null)
                throw new Exception("应用程序尚未注册发布者实例");
            if (message == null)
                throw new ArgumentNullException("message", "参数message为null");
            if (filter == null)
                throw new ArgumentNullException("filter", "过滤器为null");
            publisher.Publish(message, filter);
        }
    }
}
