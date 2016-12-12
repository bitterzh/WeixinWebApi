using System.Threading;
using System.Web;
using System.Web.Mvc;
using Totyu.WeixinSDK.Apis.Open;
using Totyu.WeixinSDK.Log;
using Totyu.WeixinWebApi.Attributes;
using Totyu.WeixinWebApi.Services;

namespace Totyu.WeixinWebApi.Controllers
{
    public class OpenController : Controller
    {

        [OpenOAuthAuthorizeAttribute]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Callback()
        {
            var code = Request.QueryString.Get("code");
            //没有code表示授权失败
            if (string.IsNullOrEmpty(code))
                return RedirectToAction("Failed", "OAuth");
            var state = Request.QueryString.Get("state");
            var cache_status = System.Web.HttpContext.Current.Cache.Get(state);
            //没有获取到state,就跳转到首页
            var redirect_url = cache_status == null ? "/" : cache_status.ToString();
            redirect_url = HttpUtility.UrlDecode(redirect_url);
            var scope = WeixinConfig.OauthScope;
            var access_token_scope = "";
            double expires_in = 0;
            var access_token = "";
            var openId = "";
            var token = OAuth2API.GetAccessToken(WeixinConfig.AppID, WeixinConfig.AppSecret, code);            
            dynamic userinfo;
            if (scope == "snsapi_userinfo")
            {
                userinfo = OAuth2API.GetUserInfo(token.access_token, token.openid);
            }
            else
            {
                //基础支持中的access_token
                access_token = WeixinConfig.AccessTokenHelper.GetToken();
                openId = token.openid;
                expires_in = token.expires_in;
                //TODO: 如果用户已经关注，可以用openid，获取用户信息。
                //如果本地已经存储了用户基本信息，建议在本地获取。
                userinfo = OAuth2API.GetUserInfo(access_token, openId);
            }
            if (userinfo != null)
                redirect_url += "?unionId=" + userinfo.unionid;
            //写入cookies
            AuthorizationManager.SetTicket(true, 1, openId, userinfo.nickname);
            Thread.Sleep(500);//暂停半秒钟，以等待IOS设置Cookies的延迟
            Log.Instence.LogWriteLine(string.Format("OAuth success: identity: {0} , name: {1} , redirect_rul:{2} , expires_in: {3}s ", openId, userinfo.nickname, redirect_url, expires_in));
            return new RedirectResult(redirect_url, true);
        }

        public ActionResult Failed()
        {
            ViewBag.message = "OAuth失败，您拒绝了授权申请或者公众好号没有此权限.";
            return View();
        }
    }
}