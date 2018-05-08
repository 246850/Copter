using Copter.Result.List;

namespace Copter.Data
{
    /// <summary>
    /// 分页查询 操作
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    public interface IHasPagedService<TEntity, TKey> 
        where TEntity:CopterBaseEntity<TKey>, new() 
        where TKey:struct
    {
        IPagedList<TEntity> GetPagedList(TEntity query, int page, int pageSize);
    }

    public interface IInt32PagedService<TEntity> : IHasPagedService<TEntity, int>
        where TEntity : CopterBaseEntity<int>, new()
    {
        
    }
    
}
