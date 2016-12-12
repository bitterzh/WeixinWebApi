using System.IO;
using System.Web.Mvc;
using Totyu.WeixinSDK.Apis.MP;
using Totyu.WeixinSDK.Cryptography;
using Totyu.WeixinSDK.Entiyies;
using Totyu.WeixinSDK.Executor;
using Totyu.WeixinSDK.Log;
using Totyu.WeixinWebApi.Models;

namespace Totyu.WeixinWebApi.Controllers
{
    /// <summary>
    /// 用于处理公众号接受消息
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// 微信后台验证地址（使用Get），微信后台的“接口配置信息”的Url
        /// </summary>
        [HttpGet]
        [ActionName("Auth")]
        public ActionResult Get(PostModel postModel, string echostr)
        {
            if (string.IsNullOrEmpty(WeixinConfig.Token)) return Content("请先设置Token！");
            var ent = "";
            if (!BasicAPI.CheckSignature(postModel.Signature, postModel.Timestamp, postModel.Nonce, WeixinConfig.Token, out ent))
            {
                return Content("参数错误！");
            }
            return Content(echostr); //返回随机字符串则表示验证通过
        }

        /// <summary>
        /// 用户发送消息后，微信平台自动Post一个请求到这里，并等待响应XML。
        /// </summary>
        [HttpPost]
        [ActionName("Auth")]
        public ActionResult Post(PostModel postModel)
        {
            postModel.Token = WeixinConfig.Token;
            //postModel.EncodingAESKey = WeixinConfig.EncodingAESKey;
            postModel.AppId = WeixinConfig.AppID;

            var ent = "";
            if (!BasicAPI.CheckSignature(postModel.Signature, postModel.Timestamp, postModel.Nonce, WeixinConfig.Token, out ent))
            {
                return Content("参数错误！");
            }

            var encryptMsg = string.Empty;
            WeixinMessage message = null;
            var safeMode = Request.QueryString.Get("encrypt_type") == "aes";
            using (var streamReader = new StreamReader(Request.InputStream))
            {
                var decryptMsg = string.Empty;
                var msg = streamReader.ReadToEnd();

                #region 解密
                if (safeMode)
                {
                    var msg_signature = Request.QueryString.Get("msg_signature");
                    var wxBizMsgCrypt = new WXBizMsgCrypt(WeixinConfig.Token, WeixinConfig.EncodingAESKey, WeixinConfig.AppID);
                    var ret = wxBizMsgCrypt.DecryptMsg(msg_signature, postModel.Timestamp, postModel.Nonce, msg, ref decryptMsg);
                    if (ret != 0)//解密失败
                    {
                        //TODO：开发者解密失败的业务处理逻辑
                        Log.Instence.LogWriteLine(string.Format("decrypt message return {0}, request body {1}", ret, msg));
                    }
                }
                else
                {
                    decryptMsg = msg;
                }
                #endregion

                message = AcceptMessageAPI.Parse(decryptMsg);
            }

            var response = new WeixinExecutor().Execute(message);

            #region 加密
            if (safeMode)
            {
                var msg_signature = Request.QueryString.Get("msg_signature");
                var wxBizMsgCrypt = new WXBizMsgCrypt(WeixinConfig.Token, WeixinConfig.EncodingAESKey, WeixinConfig.AppID);
                var ret = wxBizMsgCrypt.EncryptMsg(response, postModel.Timestamp, postModel.Nonce, ref encryptMsg);
                if (ret != 0)//加密失败
                {
                    //TODO：开发者加密失败的业务处理逻辑
                    Log.Instence.LogWriteLine(string.Format("encrypt message return {0}, response body {1}", ret, response));
                }
            }
            else
            {
                encryptMsg = response;
            }
            #endregion
            return new ContentResult
            {
                Content = encryptMsg,
                ContentType = "text/xml",
                ContentEncoding = System.Text.UTF8Encoding.UTF8
            };
        }

        /// <summary>
        /// 不使用加密解密
        /// </summary>
        [HttpPost]
        [ActionName("MiniAuth")]
        public ActionResult MiniPost(PostModel postModel)
        {
            var ent = "";
            if (!BasicAPI.CheckSignature(postModel.Signature, postModel.Timestamp, postModel.Nonce, WeixinConfig.Token, out ent))
            {
                //return Content("参数错误！");//v0.7-
                return new WeixinResult("参数错误！");//v0.8+
            }

            var encryptMsg = string.Empty;
            WeixinMessage message = null;
            using (var streamReader = new StreamReader(Request.InputStream))
            {
                var decryptMsg = streamReader.ReadToEnd();
                message = AcceptMessageAPI.Parse(decryptMsg);
            }
            encryptMsg = new WeixinExecutor().Execute(message);

            return new WeixinResult(encryptMsg)
            {
                ContentType = "text/xml",
                ContentEncoding = System.Text.UTF8Encoding.UTF8
            };
        }
    }
}