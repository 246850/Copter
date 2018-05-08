using System;
using System.Collections.Generic;
using System.Linq;

namespace Copter.Data.MongoDb
{
    /// <summary>
    /// Mongodb 领域实体 泛型仓储 接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IRepository<TEntity> where TEntity: CopterBaseEntity<Guid>, new()
    {
        /// <summary>
        /// 对应查询表
        /// </summary>
        IQueryable<TEntity> Table { get; }

        /// <summary>
        /// 根据Id获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Get(Guid id);

        /// <summary>
        /// 插入一条
        /// </summary>
        /// <param name="entity"></param>
        void Add(TEntity entity);
    }
}
