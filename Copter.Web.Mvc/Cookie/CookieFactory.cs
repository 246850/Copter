namespace Copter.Web.Mvc.Cookie
{
    public class CookieFactory:ICookieFactory
    {
        public ICookieClient Create()
        {
            return new DefaultCookieClient();
        }
    }
}
