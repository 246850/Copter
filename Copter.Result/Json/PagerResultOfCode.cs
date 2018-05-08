namespace Copter.Result.Json
{
    /// <summary>
    /// Code 类型的 分页列表结果
    /// </summary>
    /// <typeparam name="T">类型 T</typeparam>
    public class PagerResultOfCode<T>: ListResultOfCode<T>, IHasPagerResult
    {
        public PagerResultOfCode()
        {
            Pager = new PagerItem();
        }
        /// <summary>
        /// 分页信息 对象
        /// </summary>
        public virtual PagerItem Pager { get; set; }
    }
}
