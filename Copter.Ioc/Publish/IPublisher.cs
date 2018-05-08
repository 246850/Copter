namespace Copter.Ioc.Publish
{
    /// <summary>
    /// 发布消息接口
    /// </summary>
    public interface IPublisher
    {
        void Publish<T>(T message) where T : class, new();

        void Publish<T>(T message, object filter) where T : class, new();
    }
}
