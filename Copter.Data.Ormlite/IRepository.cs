using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using Copter.Result.List;
using ServiceStack.OrmLite;

namespace Copter.Data.Ormlite
{
    public interface IRepository<TEntity, TKey>
        where TEntity : CopterBaseEntity<TKey>, new()
        where TKey:struct 
    {
        /// <summary>
        /// 数据库连接对象
        /// </summary>
        IDbConnection Connection { get; }

        /// <summary>
        /// 对应表 - 查询表达式
        /// </summary>
        SqlExpression<TEntity> Table { get; }

        /// <summary>
        /// 是否存在记录
        /// </summary>
        /// <param name="expression">查询表达式</param>
        bool Exists(SqlExpression<TEntity> expression);

        /// <summary>
        /// 统计记录数
        /// </summary>
        /// <param name="expression">查询表达式</param>
        long Count(SqlExpression<TEntity> expression);

        /// <summary>
        /// 根据id查询单条记录，相当于select * from table where id = 1 - 存在多条抛出异常
        /// </summary>
        /// <param name="id">通常为：主键/唯一键Id</param>
        TEntity Get(TKey id);

        /// <summary>
        /// 根据条件查询单条记录
        /// </summary>
        /// <param name="expression">查询表达式</param>
        TEntity LoadEntity(SqlExpression<TEntity> expression);

        /// <summary>
        /// 根据条件查询列表，相当于select * from table where field1 = value1 and field2 = value2
        /// </summary>
        /// <param name="expression">查询表达式</param>
        IList<TEntity> LoadEntityList(SqlExpression<TEntity> expression);

        /// <summary>
        /// 根据条件查询列表 - 分页，相当于select * from table where field1 = value1 and field2 = value2 limit 1, 10
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="page">当前页码</param>
        /// <param name="pageSize">页容量大小</param>
        IPagedList<TEntity> LoadEntityList(SqlExpression<TEntity> expression, int page, int pageSize);

        /// <summary>
        /// 插入记录，相当于insert into table(field1, field2) values(value1, value2); SELECT @@IDENTITY
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="selectIdentity">true：查询当前记录的Id，false：不查询</param>
        long Add(TEntity entity, bool selectIdentity = true);

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns></returns>
        int Add(IList<TEntity> entities);

        bool Save(TEntity entity, bool references = false);

        /// <summary>
        /// 更新记录，前提必须有主键Id，相当于 update table set field = value where id = 1
        /// </summary>
        /// <param name="entity">实体对象</param>
        int Update(TEntity entity);

        /// <summary>
        /// 根据条件 更新记录，适用于无主键Id类似的表。
        /// </summary>
        /// <param name="entity">动态实体对象</param>
        /// <param name="expression">表达式</param>
        int Update(object entity, Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// 根据Id删除记录，相当于delete from table where id = 1
        /// </summary>
        /// <param name="id">通常为：主键/唯一键Id</param>
        int Delete(TKey id);

        /// <summary>
        /// 根据Id删除记录 - 批量，相当于delete from table where id in(1, 2, 3)
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        int Delete(IList<TKey> idList);

        /// <summary>
        /// 根据表达式条件 删除记录 - 例delete from table where name = '....';
        /// </summary>
        /// <param name="expression"></param>
        int Delete(SqlExpression<TEntity> expression);

        /// <summary>
        /// 事务操作[抛异常] - 返回结果为true代表成功
        /// </summary>
        /// <param name="func">事务执行的委托，通常包括 批量对数据库操作行为</param>
        /// <returns></returns>
        bool ExecuteTransaction(Action func);

        /// <summary>
        /// 事务操作[不抛异常] - 返回结果为true代表成功
        /// </summary>
        /// <param name="func">事务执行的委托，通常包括 批量对数据库操作行为</param>
        /// <param name="logger">参数为Exception类型的日记记录委托</param>
        bool ExecuteTransaction(Action func, Action<Exception> logger);

        /// <summary>
        /// 通过IDbConnection执行操作
        /// </summary>
        /// <param name="func">参数为IDbConnection类型的委托</param>
        void ExecuteDbConnection(Action<IDbConnection> func);

        /// <summary>
        /// 通过IDbConnection执行操作，有返回值
        /// </summary>
        /// <param name="func">参数为IDbConnection类型返回值为TEntity类型的委托</param>
        T ExecuteDbConnection<T>(Func<IDbConnection, T> func);
    }

    /// <summary>
    /// 领域实体主键类型Int32 - Ormlite泛型仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IInt32Repository<TEntity> : IRepository<TEntity, int> where TEntity : CopterBaseEntity<int>, new()
    {

    }
}
