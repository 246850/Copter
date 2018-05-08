using System.Collections.Generic;

namespace Copter.Result.Json
{
    /// <summary>
    /// Code 类型的 列表结果
    /// </summary>
    /// <typeparam name="T">类型 T</typeparam>
    public class ListResultOfCode<T> : CodeResult, IHasListResult<T>
    {
        public virtual IList<T> List { get; set; }
    }
}
