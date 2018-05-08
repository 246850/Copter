using System;

namespace Copter.Infrastructure.Messaging
{
    /// <summary>
    /// 消息队列执行 接口
    /// </summary>
    public interface IMessagingExecutor
    {
        void Publish(Action func);
    }
}
