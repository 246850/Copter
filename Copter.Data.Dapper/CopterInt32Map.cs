
namespace Copter.Data.Dapper
{
    /// <summary>
    /// 主键类型Int32 . 对象-关系映射基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class CopterInt32Map<TEntity>: CopterBaseMap<TEntity,int>
        where TEntity : CopterBaseEntity<int>, new()
    {
    }
}
