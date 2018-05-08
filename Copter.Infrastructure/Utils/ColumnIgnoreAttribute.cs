using System;

namespace Copter.Infrastructure.Utils
{
    /// <summary>
    /// DataTable列名 和 实体类属性名 忽略映射
    /// </summary>
    public class ColumnIgnoreAttribute : Attribute
    {
        public ColumnIgnoreAttribute() : this(true)
        {

        }

        public ColumnIgnoreAttribute(bool ignore)
        {
            Ignore = ignore;
        }
        /// <summary>
        /// 是否忽略
        /// </summary>
        public bool Ignore { get; }
    }
}
