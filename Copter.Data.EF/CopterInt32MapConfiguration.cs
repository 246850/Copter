
namespace Copter.Data.EF
{
    /// <summary>
    /// 实体-关系表映射配置 - 继承自 EntityTypeConfiguration | 主键类型Int32
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class CopterInt32MapConfiguration<TEntity> : CopterMapConfiguration<TEntity, int> where TEntity : CopterBaseEntity<int>, new()
    {
    }
}
