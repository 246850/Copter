using System.Security.Principal;

namespace Copter.Web.Mvc.Principal
{
    /// <summary>
    /// 登录用户 标识
    /// </summary>
    /// <typeparam name="TKey">主键Id类型</typeparam>
    public class CopterIdentity<TBody, TKey>: IIdentity where TBody: AuthUserBase<TKey>
    {
        /// <summary>
        /// 默认授权类型使用 Json Web Token
        /// </summary>
        /// <param name="name">获取当前用户的名称。</param>
        /// <param name="data">用户数据</param>
        public CopterIdentity(string name, TBody data):this(name, data, "JSONWEBTOKEN")
        {
            
        }

        public CopterIdentity(string name, TBody data, string authenticationType)
        {
            Name = name;
            Body = data;
            AuthenticationType = authenticationType;
            IsAuthenticated = true;
        }
        public string Name { get; }
        public string AuthenticationType { get; }
        public bool IsAuthenticated { get; }

        /// <summary>
        /// 用户数据载体
        /// </summary>
        public TBody Body{ get; }
    }
}
