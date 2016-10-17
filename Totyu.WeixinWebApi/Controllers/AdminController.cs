using System.Web.Mvc;
using Totyu.WeixinSDK.Apis.MP;

namespace Totyu.WeixinWebApi.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult GetUserInfo(string openId)
        {
            dynamic userinfo = AdminAPI.GetUserInfo(WeixinConfig.AccessToken, openId);
            return new ContentResult
            {
                Content = userinfo,
                ContentType = "text/xml",
                ContentEncoding = System.Text.UTF8Encoding.UTF8
            };
        }
    }
}
