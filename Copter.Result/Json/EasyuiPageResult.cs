using Newtonsoft.Json;
using System.Collections.Generic;

namespace Copter.Result.Json
{
    /// <summary>
    /// Easy UI 数据列表 结果
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    public class EasyuiPageResult<T> : PagerResultOfBoolean<T>
    {
        [JsonProperty("rows")]
        public override IList<T> List { get; set; }

        [JsonProperty("total")]
        public long Total
        {
            get
            {
                if (Pager == null) return 0;
                return Pager.Total;
            }
        }

    }
}
