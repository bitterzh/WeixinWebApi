using System;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using Totyu.WeixinSDK.Helper;
using Totyu.WeixinSDK.Helper.Http;
using Totyu.WeixinSDK.Log;

namespace Totyu.WeixinWebApi.Attributes
{
    /// <summary>
    /// 微信OAuth
    /// </summary>
    public class WeixinOAuthAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 授权登陆        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var target_uri = RequestUtility.UrlEncode(WeixinConfig.Domain);//filterContext.RequestContext.HttpContext.Request.Url.PathAndQuery;
            var identity = filterContext.HttpContext.User.Identity;
            if (!identity.IsAuthenticated)
            {
                var userAgent = filterContext.RequestContext.HttpContext.Request.UserAgent;
                //这里需要完整url地址，对应Controller里面的OAuthController的Callback
                var redirect_uri = string.Format("{0}/OAuth2/Callback", WeixinConfig.APIDomain);
                redirect_uri = RequestUtility.UrlEncode(redirect_uri);
                var scope = WeixinConfig.OauthScope;
                //state保证唯一即可,可以用其他方式生成
                var state = Math.Abs(DateTime.Now.ToBinary()).ToString();
                //这里为了实现简单，将state和target_uri保存在Cache中，并设置过期时间为2分钟。可以采用其他方法!!!
                HttpContext.Current.Cache.Add(state, target_uri, null, DateTime.Now.AddMinutes(2), TimeSpan.Zero, CacheItemPriority.Normal, null);
                Log.Instence.LogWriteLine(string.Format("begin weixin oauth: scope: {0}, redirect_uri: {1} , state: {2} , user agent: {3} ", scope, redirect_uri, state, userAgent));
                var weixinOAuth2Url = string.Format(ApiUrlHelper.MPApiUrl.GetCodeOauth2, WeixinConfig.AppID, redirect_uri, scope, state);
                filterContext.Result = new RedirectResult(weixinOAuth2Url);
            }
            else
            {
                base.OnAuthorization(filterContext);
            }
        }
    }
}