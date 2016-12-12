using com.totyu.LYWorld.Common.BaseMvc;
using com.totyu.LYWorld.Common.Extentions;
using com.totyu.LYWorld.Common.Messages;
using com.totyu.LYWorld.Web.Models.User;
using System;
using System.Web;
using System.Web.Mvc;

namespace Totyu.WeixinWeb.Controllers
{
    public class UserBindController : Controller
    {
        // GET: UserBind
        public ActionResult Index(string unionId)
        {
            string returnUrl = "http%3A%2F%2Flyhuangshan.m.d.totyu.cn%2FStore%2FHome%3Fcode%3Dlyhuangshan%26from%3Ds";
            ViewBag.ReturnUrl = Uri.EscapeDataString(returnUrl.ToStringOrDefault());
            ViewBag.UnionId = unionId;
            ViewBag.Type = 1;
            ViewBag.smsTime = 120;
            return View();
        }

        [HttpPost]
        public ActionResult Bind(LoginModel model, string returnUrl, string unionId = "")
        {
            ViewBag.Type = model.Type;
            ViewBag.ReturnUrl = Uri.EscapeDataString(returnUrl.ToStringOrDefault());
            ViewBag.smsTime = 120;

            //普通登录
            try
            {
                UserBaseInfo uInfo = new UserBaseInfo();
                if (login(ref model, out uInfo))
                {
                    //登录成功
                    AddAccountTokenToCookie(model.Token);

                    returnUrl = com.totyu.LYWorld.Common.GlobalFunc.Decrypt(returnUrl, "UrlHelper");

                    if (returnUrl.Contains("Register") || returnUrl.Contains("Login"))
                    {
                        Response.Redirect("/Home/Index");
                    }
                    else
                        return RedirectToLocal(returnUrl);
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("LoginOtherError", MessageHelper.Get("LoginOtherError"));
            }

            ModelState.Clear();
            model.ValidatorCode = "";
            ModelState.FillError(model.ErrList);
            return View(model);
        }

        /// <summary>
        /// 追加内容到cookie
        /// </summary>
        /// <param name="value"></param>
        private void AddAccountTokenToCookie(string value)
        {
            HttpCookie _cookie = CookieHelper.Create("AccountToken", value);
            Response.Cookies.Add(_cookie);
        }

        /// <summary>
        /// 登陆统一处理
        /// </summary>
        /// <param name="model"></param>
        /// <param name="uInfo"></param>
        /// <returns></returns>
        private static bool login(ref LoginModel model, out UserBaseInfo uInfo)
        {

            if (LoginModel.Login(ref model, out uInfo))
            {
                CurrentUser.LoginTryTimes = 0;
                CurrentUser.UserName = uInfo.Account;
                CurrentUser.UserId = uInfo.UserId;
                CurrentUser.NewUserId = uInfo.NewUserID;
                CurrentUser.UserLevel = uInfo.Mlevel;
                CurrentUser<UserBaseInfo>.BaseInfo = uInfo;
                return true;
            }
            CurrentUser.LoginTryTimes++;
            return false;

        }

        /// <summary>
        /// 跳转URL 并检查是否为本服务器路径
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        protected ActionResult RedirectToLocal(string returnUrl = null)
        {
            if (returnUrl.HasValue())
            {
                return Redirect(returnUrl.Replace("%AND%", "&"));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}