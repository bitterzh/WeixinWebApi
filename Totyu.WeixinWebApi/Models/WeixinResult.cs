using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Totyu.WeixinWebApi.Models
{
    /// <summary>
    /// 返回MessageHandler结果
    /// </summary>
    public class WeixinResult : ContentResult
    {
        public WeixinResult(string content)
        {
            base.Content = content;
        }
    }
}