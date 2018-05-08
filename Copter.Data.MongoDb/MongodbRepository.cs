using MongoDB.Driver;
using System;
using System.Linq;

namespace Copter.Data.MongoDb
{
    public class MongodbRepository<TEntity> : IRepository<TEntity> where TEntity : CopterBaseEntity<Guid>, new()
    {
        public MongodbRepository(MongodbConnectionWrapper connection)
        {
            if (connection == null) throw new ArgumentNullException("connecdtion");
            if (string.IsNullOrWhiteSpace(connection.Host) || string.IsNullOrWhiteSpace(connection.DbName)) throw new Exception("Host或DbName格式不正确");

            IMongoClient client = new MongoClient(connection.Host);
            Db = client.GetDatabase(connection.DbName);
        }
        protected IMongoDatabase Db { get; }
        protected IMongoCollection<TEntity> Collection
        {
            get
            {
                string name = typeof(TEntity).Name;
                return Db.GetCollection<TEntity>(name);
            }
        }

        #region 实现
        public IQueryable<TEntity> Table
        {
            get
            {
                return Collection.AsQueryable();
            }
        }

        public TEntity Get(Guid id)
        {
            return Table.FirstOrDefault(x=> x.Id == id);
        }

        public void Add(TEntity entity)
        {
            Collection.InsertOne(entity);
        }
        #endregion
    }
}
