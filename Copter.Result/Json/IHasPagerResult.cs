namespace Copter.Result.Json
{
    /// <summary>
    /// 带分页数据 Pager 结果 接口
    /// </summary>
    public interface IHasPagerResult
    {
        /// <summary>
        /// 分页数据
        /// </summary>
        PagerItem Pager { get; set; }
    }
}
