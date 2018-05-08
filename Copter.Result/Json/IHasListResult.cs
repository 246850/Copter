using System.Collections.Generic;

namespace Copter.Result.Json
{
    /// <summary>
    /// 带 列表 结果集 接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IHasListResult<T>
    {
        /// <summary>
        /// 列表数据 载体
        /// </summary>
        IList<T> List { get; set; }
}
}
