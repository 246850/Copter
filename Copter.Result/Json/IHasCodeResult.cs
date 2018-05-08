namespace Copter.Result.Json
{
    /// <summary>
    /// 带 code 结果 接口
    /// </summary>
    public interface IHasCodeResult
    {
        /// <summary>
        /// 编号
        /// </summary>
        long Code { get; set; }
    }
}
