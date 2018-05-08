namespace Copter.Ioc.Publish
{
    /// <summary>
    /// 订阅接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISubscriber<in T> where T : class, new()
    {
        void Notify(T message);

        object Filter { get; }
    }
}
