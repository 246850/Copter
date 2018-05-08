using System;
using System.Linq;

namespace Copter.Ioc.Publish
{
    public class DefaultPublisher : IPublisher
    {
        public void Publish<T>(T message) where T : class, new()
        {
            ISubscriber<T>[] subscribers = ResolveSubscriber<T>();
            Notify<T>(subscribers, message);
        }

        public void Publish<T>(T message, object filter) where T : class, new()
        {
            if (filter == null) throw new ArgumentNullException("filter", "过滤器为null");
            ISubscriber<T>[] subscribers = ResolveSubscriber<T>(filter);
            Notify<T>(subscribers, message);
        }

        private ISubscriber<T>[] ResolveSubscriber<T>() where T : class, new()
        {
            ISubscriber<T>[] subscribers = EngineContext.Current.ResolveAll<ISubscriber<T>>();
            return subscribers;
        }

        private ISubscriber<T>[] ResolveSubscriber<T>(object filter) where T : class, new()
        {
            ISubscriber<T>[] subscribers = ResolveSubscriber<T>();
            if (filter != null)
                return subscribers.Where(x => x.Filter.ToString().ToLower() == filter.ToString().ToLower()).ToArray();
            return subscribers;
        }

        void Notify<T>(ISubscriber<T>[] subscribers, T message) where T : class, new()
        {
            if (subscribers != null && subscribers.Any())
                foreach (ISubscriber<T> item in subscribers)
                    item.Notify(message);
        }
    }
}
