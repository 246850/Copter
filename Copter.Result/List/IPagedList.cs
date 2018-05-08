using System.Collections.Generic;

namespace Copter.Result.List
{
    /// <summary>
    /// 分页列表 接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPagedList<T> : IList<T> where T : class ,new()
    {
        /// <summary>
        /// 分页信息对象
        /// </summary>
        PagerItem Pager { get; }

    }
}
