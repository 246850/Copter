namespace Copter.Web.Mvc.Cookie
{
    /// <summary>
    /// CookieClient 创建工厂
    /// </summary>
    public interface ICookieFactory
    {
        /// <summary>
        /// 创建CookieClient 实例
        /// </summary>
        /// <returns>ICookieClient 实例</returns>
        ICookieClient Create();
    }
}
