using System;

namespace Copter.Data
{
    /// <summary>
    /// 领域实体泛型主键Id 抽象基类
    /// </summary>
    /// <typeparam name="TKey">主键Id TKey</typeparam>
    public abstract class CopterBaseEntity<TKey> where TKey : struct
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public virtual TKey Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public virtual DateTime LastModifyTime { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        public virtual string CreateUser { get; set; }
        /// <summary>
        /// 最后修改者
        /// </summary>
        public virtual string LastModifyUser { get; set; }
    }
}
