using System.Collections.Generic;

namespace Copter.Result.Json
{
    /// <summary>
    /// Boolean值 类型的 列表结果
    /// </summary>
    /// <typeparam name="T">类型 T</typeparam>
    public class ListResultOfBoolean<T>: BooleanResult, IHasListResult<T>
    {
        public virtual IList<T> List { get; set; }
    }
}
