namespace Copter.Ioc
{
    /// <summary>
    /// 启动 任务 接口
    /// </summary>
    public interface IStartupTask
    {
        /// <summary>
        /// 执行方法
        /// </summary>
        void Execute();
        /// <summary>
        /// 顺序
        /// </summary>
        int Order { get; }
    }
}
