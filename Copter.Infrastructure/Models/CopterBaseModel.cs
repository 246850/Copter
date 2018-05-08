using System;

namespace Copter.Infrastructure.Models
{
    /// <summary>
    /// 业务模型 - 泛型抽象基类
    /// </summary>
    /// <typeparam name="TKey">主键标识类型</typeparam>
    public abstract class CopterBaseModel<TKey> where TKey:struct
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public TKey Id { get; set; }
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
