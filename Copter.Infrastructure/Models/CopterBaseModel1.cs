using System.Collections.Generic;
using Newtonsoft.Json;

namespace Copter.Infrastructure.Models
{
    /// <summary>
    /// 业务模型 - 抽象基类 - 带SelectListItem
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    /// <typeparam name="TSelectListItem">数据载体类型</typeparam>
    public abstract class CopterBaseModel<TKey, TSelectListItem> : CopterBaseModel<TKey> where TKey : struct where TSelectListItem : class
    {
        protected CopterBaseModel()
        {
            CustomProperties = new Dictionary<string, TSelectListItem>();
        }

        [JsonIgnore]
        public IDictionary<string, TSelectListItem> CustomProperties { get; set; }
    }
}
