using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using DapperExtensions;
using System.Linq;
using Copter.Result.List;

namespace Copter.Data.Dapper
{
    /// <summary>
    /// Dapper仓储实现类
    /// </summary>
    /// <typeparam name="TEntity">领域实体类型</typeparam>
    /// <typeparam name="TKey">主键Id</typeparam>
    public class DapperRepository<TEntity, TKey>: IRepository<TEntity, TKey>
        where TEntity : CopterBaseEntity<TKey>, new()
        where TKey : struct
    {
        public DapperRepository(IDbConnection connection)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            Connection = connection;
            if(Connection.State != ConnectionState.Open && Connection.State != ConnectionState.Broken)
            {
                Connection.Open();
            }
        }

        public IDbConnection Connection { get; }

        public void Add(IList<TEntity> entities, IDbTransaction transaction = null)
        {
            foreach(TEntity entity in entities)
            {
                entity.CreateTime = DateTime.Now;
                entity.LastModifyTime = DateTime.Now;
            }
            Connection.Insert(entities, transaction);
        }

        public TKey Add(TEntity entity, IDbTransaction transaction = null)
        {
            entity.CreateTime = DateTime.Now;
            entity.LastModifyTime = DateTime.Now;
            Connection.Insert(entity, transaction);
            return entity.Id;
        }
        
        public int Update(TEntity entity, IDbTransaction transaction = null)
        {
            entity.LastModifyTime = DateTime.Now;
            return Connection.Update(entity, transaction) ? 1: -1;
        }

        public int Count(object predicate)
        {
            int count = Connection.Count<TEntity>(predicate);
            return count;
        }

        public int Delete(TEntity entity, IDbTransaction transaction = null)
        {
            return Connection.Delete<TEntity>(entity, transaction)? 1: -1;
        }

        public int Delete(object predicate, IDbTransaction transaction = null)
        {
            return Connection.Delete<TEntity>(predicate, transaction) ? 1 : -1;
        }

        public int Delete(TKey id, IDbTransaction transaction = null)
        {
            IFieldPredicate predicate = Predicates.Field<TEntity>(entity=> entity.Id, Operator.Eq, id, true);
            return Delete(predicate, transaction);
        }

        public TEntity Get(TKey id)
        {
            TEntity entity = Connection.Get<TEntity>(id);
            return entity;
        }

        public TEntity LoadEntity(object predicate)
        {
            return LoadEntityList(predicate).FirstOrDefault();
        }

        public IList<TEntity> LoadEntityList(object predicate = null, IList<ISort> sorts = null)
        {
            // 分页必须存在 排序字段 这里默认 按Id降序
            if (sorts == null) sorts = new List<ISort> { new Sort { PropertyName = "Id", Ascending = false } };
            return Connection.GetList<TEntity>(predicate, sorts).ToList(); ;
        }

        public IPagedList<TEntity> LoadEntityList(object predicate, IList<ISort> sorts, int page, int pageSize)
        {
            // 分页必须存在 排序字段 这里默认 按Id降序
            if (sorts == null)
            {
                sorts = new List<ISort> { Predicates.Sort<TEntity>(x => x.Id, false) };
            }

            int total = Count(predicate);   // 统计数量
            IEnumerable<TEntity> list = Connection.GetPage<TEntity>(predicate, sorts, page, pageSize);
            return list.ToPagedList(page, pageSize, total);
        }

        public IPagedList<TEntity> LoadEntityList(string selectSql, string countSql, object param, int page, int pageSize)
        {
            long total = Connection.ExecuteScalar<long>(countSql, param);
            IEnumerable<TEntity> list = Connection.Query<TEntity>(selectSql, param);

            return list.ToPagedList(page, pageSize, total);
        }

        public bool ExecuteTransaction(Action<IDbTransaction> func, Action<Exception> logger)
        {
            IDbTransaction transaction = Connection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                if (func != null)
                {
                    func.Invoke(transaction);

                    transaction.Commit();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                if (logger != null) logger.BeginInvoke(ex, null, null);
                return false;
            }
            finally
            {
                if (transaction != null)
                {
                    transaction.Dispose();
                }
            }
        }

        public void ExecuteDbConnection(Action<IDbConnection> func)
        {
            if (func != null) func.Invoke(Connection);
        }

        public TEntity ExecuteDbConnection(Func<IDbConnection, TEntity> func)
        {
            if (func != null) return func.Invoke(Connection);
            return null;
        }
    }

    /// <summary>
    /// 主键类型Int32 - Dapper仓储 实现
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class DapperInt32Repository<TEntity> : DapperRepository<TEntity, int>, IInt32Repository<TEntity> where TEntity : CopterBaseEntity<int>, new()
    {
        public DapperInt32Repository(IDbConnection connection) : base(connection)
        {
        }
    }
}
