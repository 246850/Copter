using DapperExtensions.Mapper;
using System.Text.RegularExpressions;

namespace Copter.Data.Dapper
{
    /// <summary>
    /// DapperExtension ClassMapper 对象 - 关系映射基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class CopterBaseMap<TEntity, TKey> : AutoClassMapper<TEntity>
        where TEntity : CopterBaseEntity<TKey>, new()
        where TKey : struct
    {
        public CopterBaseMap()
        {
        }

        public override void Table(string tableName)
        {
            if (Regex.IsMatch(tableName, @".+Entity$", RegexOptions.IgnoreCase))
            {
                tableName = tableName.Remove(tableName.Length - 6, 6);
            }
            TableName = tableName;
        }
    }
}
