using System.Data.Entity.ModelConfiguration;
namespace Copter.Data.EF
{
    /// <summary>
    /// 实体-关系表映射配置基类 - 继承自 EntityTypeConfiguration
    /// </summary>
    public abstract class CopterMapConfiguration<TEntity, TKey>: EntityTypeConfiguration<TEntity>, IEntityTypeMap where TEntity : CopterBaseEntity<TKey>, new() where TKey : struct
    {
        protected CopterMapConfiguration()
        {
            HasKey(t => t.Id);
            Property(t => t.Id).HasColumnName("Id");

            InitOptionalMap();
        }

        /// <summary>
        /// 初始化可选字段 映射配置
        /// </summary>
        public virtual void InitOptionalMap()
        {
            Property(t => t.CreateTime).HasColumnName("CreateTime");
            Property(t => t.LastModifyTime).HasColumnName("LastModifyTime");
            Property(t => t.CreateUser).HasColumnName("CreateUser");
            Property(t => t.LastModifyUser).HasColumnName("LastModifyUser");
        }
    }
}
