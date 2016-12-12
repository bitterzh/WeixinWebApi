using System;
using System.Web;
using System.Web.Security;

namespace Totyu.WeixinWebApi.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthorizationManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="remeberMe"></param>
        /// <param name="version"></param>
        /// <param name="identity"></param>
        /// <param name="displayName"></param>
        public static void SetTicket(bool remeberMe, int version, string identity, string displayName)
        {
            FormsAuthentication.SetAuthCookie(identity, remeberMe);
            //displayName为空会导致cookies写入失败
            if (string.IsNullOrEmpty(displayName))
            {
                displayName = "匿名";
            }
            var authTicket = new FormsAuthenticationTicket(
                version,
                identity,
                DateTime.Now,
                DateTime.Now.AddDays(remeberMe ? 30 : 1),
                remeberMe,
                displayName);
            string encrytedTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrytedTicket);
            HttpContext.Current.Response.Cookies.Add(authCookie);
        }
    }
}