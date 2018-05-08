using System;

namespace Copter.Infrastructure.Utils
{
    /// <summary>
    /// DataTable列名 和 实体类属性名 映射 Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnMapAttribute : Attribute
    {
        public ColumnMapAttribute(string columnName)
        {
            if (string.IsNullOrWhiteSpace(columnName)) throw new ArgumentNullException(columnName);
            ColumnName = columnName;
        }
        /// <summary>
        /// 映射列名
        /// </summary>
        public string ColumnName { get; }
    }
}
