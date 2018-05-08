using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;

namespace Copter.Infrastructure.Messaging
{
    internal sealed class MQAsyncExecutor : IMessagingExecutor
    {
        #region 字段/属性/构造函数
        /// <summary>
        /// 锁对象
        /// </summary>
        private static readonly object LockObj = new object();
        public MQAsyncExecutor()
        {
            _threadResetEvent = new ManualResetEvent(false);
            _queueList = new ConcurrentQueue<Action>();

            Thread mqThread = new Thread(MQAsyncThreadBody);
            mqThread.Start();
        }
        #endregion

        #region 消息队列

        private readonly ManualResetEvent _threadResetEvent;
        private readonly ConcurrentQueue<Action> _queueList;
        /// <summary>
        /// 消息队列 线程体
        /// </summary>
        private void MQAsyncThreadBody()
        {
            while (true)
            {
                //  阻止当前线程，等待接收信号
                _threadResetEvent.WaitOne();
                Thread.Sleep(1000);
                try
                {
                    Action func;
                    while (_queueList.TryDequeue(out func))
                        func.BeginInvoke(null, null);
                }
                catch
                {
                    // ignored
                }
                finally
                {
                    //  重置事件状态，阻止当前线程
                    lock (LockObj)
                        if (!_queueList.Any()) _threadResetEvent.Reset();
                }
            }
        }

        #endregion

        #region 方法
        public void Publish(Action func)
        {
            lock (LockObj)
            {
                if (func != null) _queueList.Enqueue(func);
                if (_queueList.Any()) _threadResetEvent.Set();
            }
        }
        #endregion
    }
}
