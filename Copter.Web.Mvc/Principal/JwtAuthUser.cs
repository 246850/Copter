namespace Copter.Web.Mvc.Principal
{
    /// <summary>
    /// Jwt授权登录 用户信息 载体类
    /// </summary>
    public class JwtAuthUser<TKey> : AuthUserBase<TKey>
    {
        public JwtAuthUser()
        {

        }
        public JwtAuthUser(TKey id, string account, string name):base(id, account, name)
        {

        }
    }
}
