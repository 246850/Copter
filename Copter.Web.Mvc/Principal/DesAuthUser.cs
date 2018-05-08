namespace Copter.Web.Mvc.Principal
{
    /// <summary>
    /// Des授权登录 用户信息 载体类
    /// </summary>
    public class DesAuthUser<TKey> : AuthUserBase<TKey>
    {
        public DesAuthUser()
        {

        }
        public DesAuthUser(TKey id, string account, string name):base(id, account, name)
        {

        }
    }
}
