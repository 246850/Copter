namespace Copter.Result.Json
{
    /// <summary>
    /// Boolean值 类型的 分页列表结果
    /// </summary>
    /// <typeparam name="T">类型 T</typeparam>
    public class PagerResultOfBoolean<T>: ListResultOfBoolean<T>,IHasPagerResult
    {
        /// <summary>
        /// 分页信息 对象
        /// </summary>
        public virtual PagerItem Pager { get; set; }
    }
}
