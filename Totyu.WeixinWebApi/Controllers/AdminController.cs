using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Web.Mvc;
using Totyu.WeixinSDK.Apis.MP;
using Totyu.WeixinWebApi.Models;

namespace Totyu.WeixinWebApi.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult GetUserInfo(string openId)
        {
            dynamic userinfo = AdminAPI.GetUserInfo(WeixinConfig.AccessTokenHelper.GetToken(), openId);
            return new ContentResult
            {
                Content = userinfo,
                ContentType = "text/xml",
                ContentEncoding = System.Text.UTF8Encoding.UTF8
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult TempQRCode()
        {
            dynamic _ticket = AdminAPI.CreateQRCodeTicket(WeixinConfig.AccessTokenHelper.GetToken(), "{" +
                                                                                                        "\"expire_seconds\": 1800," +
                                                                                                        "\"action_name\": \"QR_SCENE\"," +
                                                                                                        "\"action_info\": { \"scene\": { \"scene_id\": 100000 } }" +
                                                                                                    "}");
            var _data = AdminAPI.CreateQrcode(_ticket.ticket);
            return new ImageResult()
            {
                Image = Image.FromStream(_data),
                ImageFormat = ImageFormat.Png
            };
            //return File((_data as MemoryStream).ToArray(), "image/jpg");            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateQRCode()
        {
            dynamic _ticket = AdminAPI.CreateQRCodeTicket(WeixinConfig.AccessTokenHelper.GetToken(), "{" +
                                                                                                        "\"action_name\": \"QR_LIMIT_STR_SCENE\"," +
                                                                                                        "\"action_info\": { \"scene\": { \"scene_str\": \"01World\" } }" +
                                                                                                    "}");
            var _data = AdminAPI.CreateQrcode(_ticket.ticket);
            return new ImageResult()
            {
                Image = Image.FromStream(_data),
                ImageFormat = ImageFormat.Png
            };
        }

    }
}
