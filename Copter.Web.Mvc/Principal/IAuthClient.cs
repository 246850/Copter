using Copter.Infrastructure.Models;

namespace Copter.Web.Mvc.Principal
{
    /// <summary>
    /// 登录授权 接口
    /// </summary>
    /// <typeparam name="TBody">授权信息载体</typeparam>
    /// <typeparam name="TKey">主键Id 类型</typeparam>
    public interface IAuthClient<TBody, TKey> where TBody: AuthUserBase<TKey>, new()
    {
        /// <summary>
        /// AppSecret 获取者
        /// </summary>
        ISecretBuilder SecretBuilder { get; }

        /// <summary>
        /// 登入 系统
        /// </summary>
        /// <param name="body">用户数据</param>
        void SignIn(TBody body);

        /// <summary>
        /// 获取授权信息体
        /// </summary>
        /// <returns></returns>
        TBody GetBody();

        /// <summary>
        /// 退出系统
        /// </summary>
        void SignOut();
    }
}
