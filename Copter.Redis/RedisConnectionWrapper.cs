using StackExchange.Redis;
using System.Net;

namespace Copter.Redis
{
    /// <summary>
    /// Redis Connection 包装接口
    /// </summary>
    public interface IRedisConnectionWrapper
    {
        /// <summary>
        /// 获取 db
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        IDatabase GetDatabase(int? db = null);
        /// <summary>
        /// 获取Server
        /// </summary>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        IServer Server(EndPoint endPoint);
        /// <summary>
        /// 获取终端 集合
        /// </summary>
        /// <returns></returns>
        EndPoint[] GetEndpoints();
        /// <summary>
        /// 释放 db
        /// </summary>
        /// <param name="db"></param>
        void FlushDb(int? db = null);
    }

    /// <summary>
    /// Redis Connection 包装接口
    /// </summary>
    public class RedisConnectionWrapper : IRedisConnectionWrapper
    {
        private readonly ConfigurationOptions _configuration;
        private volatile static IConnectionMultiplexer _connection;
        private static readonly object Lock = new object();

        public RedisConnectionWrapper(string connectionString) : this(ConfigurationOptions.Parse(connectionString))
        {
        }
        public RedisConnectionWrapper(ConfigurationOptions configuration)
        {
            _configuration = configuration;
        }

        protected virtual IConnectionMultiplexer Connection
        {
            get
            {
                if (_connection != null && _connection.IsConnected) return _connection;

                lock (Lock)
                {
                    if (_connection != null && _connection.IsConnected) return _connection;

                    if (_connection != null)
                    {
                        //Connection disconnected. Disposing connection...
                        _connection.Dispose();
                    }

                    //Creating new instance of Redis Connection
                    _connection = ConnectionMultiplexer.Connect(_configuration);
                }

                return _connection;
            }
        }
        #region 实现
        public IDatabase GetDatabase(int? db = null)
        {
            return Connection.GetDatabase(db ?? -1); //_settings.DefaultDb);
        }

        public void FlushDb(int? db = default(int?))
        {
            EndPoint[] endPoints = GetEndpoints();

            foreach (var endPoint in endPoints)
            {
                Server(endPoint).FlushDatabase(db ?? -1); //_settings.DefaultDb);
            }
        }

        public EndPoint[] GetEndpoints()
        {
            return Connection.GetEndPoints();
        }

        public IServer Server(EndPoint endPoint)
        {
            return Connection.GetServer(endPoint);
        }
        #endregion
    }
}
