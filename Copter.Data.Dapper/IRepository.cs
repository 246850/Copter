using Copter.Result.List;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Data;

namespace Copter.Data.Dapper
{
    /// <summary>
    /// Dapper领域实体 泛型仓储 接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    public interface IRepository<TEntity, TKey> 
        where TEntity:CopterBaseEntity<TKey>, new()
        where TKey:struct
    {
        /// <summary>
        /// 数据库连接对象
        /// </summary>
        IDbConnection Connection { get; }

        /// <summary>
        /// 根据主键Id获取单个 对象 - 基于DapperExtensions
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <returns>TEntity 对象</returns>
        TEntity Get(TKey id);

        /// <summary>
        /// 根据谓词{IPredicate} 统计数量 - 基于DapperExtensions
        /// </summary>
        /// <param name="predicate">IPredicate表达式</param>
        /// <returns></returns>
        int Count(object predicate);

        /// <summary>
        /// 根据Id删除 单个对象 - 基于DapperExtensions
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <param name="transaction">事务对象</param>
        /// <returns></returns>
        int Delete(TKey id, IDbTransaction transaction = null);

        /// <summary>
        /// 删除单个对象 - 基于DapperExtensions
        /// </summary>
        /// <param name="entity">TEntity实体对象</param>
        /// <param name="transaction">事务对象</param>
        /// <returns></returns>
        int Delete(TEntity entity, IDbTransaction transaction = null);
        /// <summary>
        /// 根据谓词{IPredicate} 删除单个对象 - 基于DapperExtensions
        /// </summary>
        /// <param name="predicate">IPredicate表达式</param>
        /// <param name="transaction">事务对象</param>
        /// <returns></returns>
        int Delete(object predicate, IDbTransaction transaction = null);

        /// <summary>
        /// 更新单个对象 - 基于DapperExtensions
        /// </summary>
        /// <param name="entity">TEntity实体对象</param>
        /// <param name="transaction">事务对象</param>
        /// <returns></returns>
        int Update(TEntity entity, IDbTransaction transaction = null);

        /// <summary>
        /// 添加单个对象
        /// </summary>
        /// <param name="entity">TEntity实体对象</param>
        /// <param name="transaction">事务对象</param>
        /// <returns></returns>
        TKey Add(TEntity entity, IDbTransaction transaction = null);

        /// <summary>
        /// 批量添加单个对象 - 基于DapperExtensions
        /// </summary>
        /// <param name="entities">TEntity实体对象集合</param>
        /// <param name="transaction">事务对象</param>
        /// <returns></returns>
        void Add(IList<TEntity> entities, IDbTransaction transaction = null);

        /// <summary>
        /// 根据谓词{IPredicate} 查询单个对象 - 基于DapperExtensions
        /// </summary>
        /// <param name="predicate">IPredicate表达式</param>
        /// <returns></returns>
        TEntity LoadEntity(object predicate);

        /// <summary>
        /// 根据谓词{IPredicate} 查询列表 - 基于DapperExtensions
        /// </summary>
        /// <param name="predicate">IPredicate表达式</param>
        /// <param name="sorts">ISort集合</param>
        /// <returns></returns>
        IList<TEntity> LoadEntityList(object predicate = null, IList<ISort> sorts = null);

        /// <summary>
        /// 根据谓词{IPredicate} 查询分页列表 - 基于DapperExtensions
        /// </summary>
        /// <param name="predicate">IPredicate表达式</param>
        /// <param name="page">当前页</param>
        /// <param name="pageSize">页记录大小</param>
        /// <param name="sorts">ISort集合</param>
        /// <returns></returns>
        IPagedList<TEntity> LoadEntityList(object predicate, IList<ISort> sorts, int page, int pageSize);

        /// <summary>
        /// 查询分页列表 基于原生Dapper Sql语句
        /// </summary>
        /// <param name="selectSql">list查询语句，参数化</param>
        /// <param name="countSql">数据查询语句， 参数化</param>
        /// <param name="param">参数对象</param>
        /// <param name="page">当前页</param>
        /// <param name="pageSize">页记录大小</param>
        /// <returns></returns>
        IPagedList<TEntity> LoadEntityList(string selectSql, string countSql, object param, int page, int pageSize);

        /// <summary>
        /// 事务性操作
        /// </summary>
        /// <param name="func">事务执行的委托，通常包括 批量对数据库操作行为</param>
        /// <param name="logger">参数为Exception类型的日记记录委托</param>
        /// <returns></returns>
        bool ExecuteTransaction(Action<IDbTransaction> func, Action<Exception> logger);

        /// <summary>
        /// 通过IDbConnection执行操作
        /// </summary>
        /// <param name="func">参数为IDbConnection类型的委托</param>
        void ExecuteDbConnection(Action<IDbConnection> func);

        /// <summary>
        /// 通过IDbConnection执行操作，有返回值
        /// </summary>
        /// <param name="func">参数为IDbConnection类型返回值为TEntity类型的委托</param>
        TEntity ExecuteDbConnection(Func<IDbConnection, TEntity> func);
    }

    /// <summary>
    /// 领域实体主键类型Int32 - Dapper泛型仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IInt32Repository<TEntity> : IRepository<TEntity, int> where TEntity : CopterBaseEntity<int>, new()
    {

    }
}
