using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Copter.Data.EF
{
    /// <summary>
    /// Ef领域实体 泛型仓储 接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    public interface IRepository<TEntity, in TKey> where TEntity: CopterBaseEntity<TKey>, new() where TKey : struct
    {
        /// <summary>
        /// 对应表 查询表达式 - 追踪
        /// </summary>
        IQueryable<TEntity> Table { get; }

        /// <summary>
        /// 对应表 查询表达式 - 不追踪
        /// </summary>
        IQueryable<TEntity> TableAsNoTracking { get; }

        /// <summary>
        /// 根据主键Id 查找数据，存在多个 则抛出异常
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Single(TKey id);

        /// <summary>
        /// 根据主键Id 查找数据
        /// </summary>
        /// <param name="id">唯一标识Id 主键</param>
        /// <returns>实体对象</returns>
        TEntity Get(TKey id);

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>新增数据主键Id</returns>
        int Add(TEntity entity);

        /// <summary>
        /// 添加数据 - 批量
        /// </summary>
        /// <param name="entities">实体集合</param>
        /// <returns>影响行数</returns>
        int Add(IList<TEntity> entities);

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="entity">实体对象 - 追踪模式下</param>
        /// <returns>影响行数</returns>
        int Update(TEntity entity);

        /// <summary>
        /// 根据主键Id 删除数据
        /// </summary>
        /// <param name="id">唯一标识Id 主键</param>
        /// <returns>影响行数</returns>
        int Delete(TKey id);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">实体对象 - 追踪模式下</param>
        /// <returns>影响行数</returns>
        int Delete(TEntity entity);

        /// <summary>
        /// 执行sql语句 - 可参数化
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="paras">参数</param>
        /// <returns>影响行数</returns>
        int ExecuteSqlCommand(string sql, params object[] paras);

        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="func">批处理 委托</param>
        /// <param name="logger">日常日志记录 委托，不记日志传null</param>
        /// <returns>是否成功</returns>
        bool ExecuteTransaction(Action func, Action<Exception> logger);

        /// <summary>
        /// 分布式事务
        /// </summary>
        /// <param name="func">批处理 委托</param>
        /// <param name="logger">日常日志记录 委托，不记日志传null</param>
        /// <returns>是否成功</returns>
        bool ExecuteTransactionScope(Action func, Action<Exception> logger);

        /// <summary>
        /// 通过 DbContext 执行
        /// </summary>
        /// <param name="func">参数为DbContext的委托</param>
        /// <param name="logger">日常日志记录 委托，不记日志传null</param>
        void ExecuteDbContext(Action<DbContext> func, Action<Exception> logger);
    }

    /// <summary>
    /// 领域实体主键类型Int32 - 泛型仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IInt32Repository<TEntity> : IRepository<TEntity, int> where TEntity : CopterBaseEntity<int>, new()
    {
        
    }
}
