using System.Web.Mvc;
using Totyu.WeixinSDK.Apis.MP;

namespace Totyu.WeixinWebApi.Controllers
{
    public class CustomMenuController : Controller
    {
        // GET: CustomMenu
        public ActionResult Create()
        {
            var json = System.IO.File.ReadAllText(Server.MapPath("/Content/json/WeiXinMenu.json"));
            bool _result = CustomMenuAPI.Create(WeixinConfig.AccessTokenHelper.GetToken(), json);
            if (!_result)
                return Content("menu创建错误！");
            return Content(json);
        }

        public ActionResult Delete()
        {
            bool _result = CustomMenuAPI.Delete(WeixinConfig.AccessTokenHelper.GetToken());
            if (!_result)
                return Content("menu删除错误！");
            return View();
        }
    }
}