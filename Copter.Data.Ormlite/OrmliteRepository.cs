using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using Copter.Result.List;
using ServiceStack.OrmLite;

namespace Copter.Data.Ormlite
{
    public class OrmliteRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : CopterBaseEntity<TKey>, new() where TKey : struct
    {
        public OrmliteRepository(IDbConnection connection)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            Connection = connection;
            if (Connection.State != ConnectionState.Open && Connection.State != ConnectionState.Broken)
            {
                Connection.Open();
            }
        }
        
        #region - 实现
        public IDbConnection Connection { get; }
        public SqlExpression<TEntity> Table
        {
            get
            {
                return Connection.From<TEntity>();
            }
        }

        public long Count(SqlExpression<TEntity> expression)
        {
            return Connection.Count(expression);
        }

        public int Delete(SqlExpression<TEntity> expression)
        {
            return Connection.Delete(expression);
        }

        public int Delete(IList<TKey> idList)
        {
            return Connection.DeleteByIds<TEntity>(idList);
        }

        public int Delete(TKey id)
        {
            return Connection.DeleteById<TEntity>(id);
        }

        public T ExecuteDbConnection<T>(Func<IDbConnection, T> func)
        {
            if (func != null) return func.Invoke(Connection);
            return default(T);
        }

        public void ExecuteDbConnection(Action<IDbConnection> func)
        {
            if (func != null) func.Invoke(Connection);
        }
        
        public bool ExecuteTransaction(Action func)
        {
            IDbTransaction transaction = Connection.OpenTransaction(IsolationLevel.ReadCommitted);
            try
            {
                if (func != null)
                {
                    func.Invoke();

                    transaction.Commit();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                if (transaction != null)
                {
                    transaction.Dispose();
                }
            }
        }

        public bool ExecuteTransaction(Action func, Action<Exception> logger)
        {
            IDbTransaction transaction = Connection.OpenTransaction(IsolationLevel.ReadCommitted);
            try
            {
                if (func != null)
                {
                    func.Invoke();

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

        public bool Exists(SqlExpression<TEntity> expression)
        {
            return Connection.Exists(expression);
        }
        
        public long Add(TEntity entity, bool selectIdentity = true)
        {
            DateTime now = DateTime.Now;
            entity.CreateTime = now;
            entity.LastModifyTime = now;

            return Connection.Insert(entity, selectIdentity: selectIdentity);
        }
        
        public int Add(IList<TEntity> entities)
        {
            Connection.InsertAll(entities);
            return 1;
        }

        public TEntity LoadEntity(SqlExpression<TEntity> expression)
        {
            return Connection.Single(expression);
        }

        public IList<TEntity> LoadEntityList(SqlExpression<TEntity> expression)
        {
            List<TEntity> entities = Connection.Select(expression);
            return entities;
        }

        public IPagedList<TEntity> LoadEntityList(SqlExpression<TEntity> expression, int page, int pageSize)
        {
            IPagedList<TEntity> entities = expression.ToPagedList<TEntity,TKey>(Connection, page, pageSize);
            return entities;
        }

        public bool Save(TEntity entity, bool references = false)
        {
            entity.LastModifyTime = DateTime.Now;
            return Connection.Save(entity, references);
        }

        public TEntity Get(TKey id)
        {
            return Connection.SingleById<TEntity>(id);
        }

        public int Update(TEntity entity)
        {
            entity.LastModifyTime = DateTime.Now;
            int result = Connection.Update(entity);
            return result;
        }

        public int Update(object entity, Expression<Func<TEntity, bool>> expression)
        {
            int result = Connection.Update(entity, expression);
            return result;
        }

        #endregion
    }

    /// <summary>
    /// 主键类型Int32 - Ormlite仓储 实现
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class OrmliteInt32Repository<TEntity> : OrmliteRepository<TEntity, int>, IInt32Repository<TEntity> where TEntity : CopterBaseEntity<int>, new()
    {
        public OrmliteInt32Repository(IDbConnection connection) : base(connection)
        {
        }
    }
}
