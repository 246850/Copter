namespace Copter.Data.MongoDb
{
    /// <summary>
    /// Mongdb数据库连接包装类
    /// </summary>
    public class MongodbConnectionWrapper
    {
        public MongodbConnectionWrapper()
        {

        }
        public MongodbConnectionWrapper(string host, string dbName)
        {
            Host = host;
            DbName = dbName;
        }
        /// <summary>
        /// 连接地址
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 数据库名/文档名
        /// </summary>
        public string DbName { get; set; }
    }
}
