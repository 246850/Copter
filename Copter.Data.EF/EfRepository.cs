using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Diagnostics;
using System.Transactions;
using System.Linq;

namespace Copter.Data.EF
{
    public abstract class EfRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : CopterBaseEntity<TKey>, new() where TKey : struct
    {
        protected DbContext Context { get; }
        protected IDbSet<TEntity> Entities
        {
            get
            {
                return Context.Set<TEntity>();
            }
        }

        public EfRepository(DbContext context)
        {
#if DEBUG
            context.Database.Log = query => Debug.WriteLine(query);
#endif
            Context = context;
        }

        #region 接口实现
        public IQueryable<TEntity> Table { get { return Entities; } }

        public IQueryable<TEntity> TableAsNoTracking { get { return Context.Set<TEntity>().AsNoTracking(); } }

        public abstract TEntity Single(TKey id);

        public abstract TEntity Get(TKey id);

        public int Add(TEntity entity)
        {
            DateTime now = DateTime.Now;
            entity.CreateTime = now;
            entity.LastModifyTime = now;
            Entities.Add(entity);
            return Context.SaveChanges();
        }

        public int Add(IList<TEntity> entities)
        {
            DateTime now = DateTime.Now;
            foreach (TEntity entity in entities)
            {
                entity.CreateTime = now;
                entity.LastModifyTime = now;
                Entities.Add(entity);
            }
            return Context.SaveChanges();
        }

        public int Update(TEntity entity)
        {
            entity.LastModifyTime = DateTime.Now;
            Context.Entry(entity).State = EntityState.Modified;
            return Context.SaveChanges();
        }

        public int Delete(TKey id)
        {
            TEntity deleteEntity = Get(id);
            if (deleteEntity != null)
            {
                return Delete(deleteEntity);
            }
            return -1;
        }

        public int Delete(TEntity entity)
        {
            Entities.Remove(entity);
            return Context.SaveChanges();
        }

        public int ExecuteSqlCommand(string sql, params object[] paras)
        {
            int result = Context.Database.ExecuteSqlCommand(sql, paras);
            return result;
        }

        public bool ExecuteTransaction(Action func, Action<Exception> logger)
        {
            if (func == null) throw new ArgumentNullException("func", "批处理委托为null");

            DbTransaction transaction = null;
            try
            {
                if (Context.Database.Connection.State != ConnectionState.Open)
                {
                    Context.Database.Connection.Open();
                }
                transaction = Context.Database.Connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                Context.Database.UseTransaction(transaction);
                func.Invoke();

                transaction.Commit();

                return true;
            }
            catch (Exception ex)
            {
                if (transaction != null) transaction.Rollback();
                if (logger != null) logger.BeginInvoke(ex, null, null);
                return false;
            }
            finally
            {
                if (transaction != null) transaction.Dispose();
            }
        }

        public bool ExecuteTransactionScope(Action func, Action<Exception> logger)
        {
            if (func == null) throw new ArgumentNullException("func", "批处理委托为null");
            TransactionScope transaction = new TransactionScope();
            try
            {
                func.Invoke();
                transaction.Complete();
                return true;
            }
            catch (Exception ex)
            {
                if (logger != null) logger.BeginInvoke(ex, null, null);
                return false;
            }
            finally
            {
                transaction.Dispose();
            }
        }

        public void ExecuteDbContext(Action<DbContext> func, Action<Exception> logger)
        {
            if (func == null) throw new ArgumentNullException("func", "参数委托为null");
            try
            {
                func.Invoke(Context);
            }
            catch (Exception ex)
            {
                if (logger != null) logger.BeginInvoke(ex, null, null);
            }
        }
        #endregion
    }

    /// <summary>
    /// 主键类型Int32 - ef仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class EfInt32Repository<TEntity> : EfRepository<TEntity, int>, IInt32Repository<TEntity> where TEntity : CopterBaseEntity<int>, new()
    {
        public EfInt32Repository(DbContext context) : base(context)
        {
        }

        public override TEntity Get(int id)
        {
            return Entities.FirstOrDefault(x => x.Id == id);
        }

        public override TEntity Single(int id)
        {
            return Entities.SingleOrDefault(x => x.Id == id);
        }
    }
}
