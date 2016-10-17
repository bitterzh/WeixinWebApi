using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using System.Text;
using Totyu.WeixinSDK.Log;

namespace Totyu.WeixinWebApi
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            WeixinConfig.Register();
            Log.Instence.LogWriteLine("app started.");
        }

        protected void Application_End()
        {
            Log.Instence.LogWriteLine("app stopped.");
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError().GetBaseException();
            StringBuilder str = new StringBuilder();
            str.Append("\r\n.客户信息：");
            string ip = "";
            if (Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR") != null)
            {
                ip = Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR").ToString().Trim();
            }
            else
            {
                ip = Request.ServerVariables.Get("Remote_Addr").ToString().Trim();
            }
            str.Append("\r\n\tIp:" + ip);
            str.Append("\r\n\t浏览器:" + Request.Browser.Browser.ToString());
            str.Append("\r\n\t浏览器版本:" + Request.Browser.MajorVersion.ToString());
            str.Append("\r\n\t操作系统:" + Request.Browser.Platform.ToString());
            str.Append("\r\n.错误信息：");
            str.Append("\r\n\t页面：" + Request.Url.ToString());
            str.Append("\r\n\t错误信息：" + ex.Message);
            str.Append("\r\n\t错误源：" + ex.Source);
            str.Append("\r\n\t异常方法：" + ex.TargetSite);
            str.Append("\r\n\t堆栈信息：" + ex.StackTrace);
            str.Append("\r\n--------------------------------------------------------------------------------------------------");
            //创建路径 
            Log.Instence.LogWriteLine(str.ToString());
        }
    }
}