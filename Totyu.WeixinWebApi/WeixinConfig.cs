using Totyu.WeixinSDK.Apis.MP;
using Totyu.WeixinSDK.Helper;

namespace Totyu.WeixinWebApi
{
    public class WeixinConfig
    {
        public static string Token { private set; get; }
        public static string EncodingAESKey { private set; get; }
        public static string AppID { private set; get; }
        public static string AppSecret { private set; get; }
        public static string AccessToken { set; get; }
        /// <summary>
        /// 网页授权使用
        /// </summary>
        public static string WebAccessToken { set; get; }
        public static string PartnerKey { private set; get; }
        public static string Domain { private set; get; }
        public static string APIDomain { private set; get; }
        public static string WeixinDomain { set; get; }
        public static string mch_id { private set; get; }
        public static string DeviceInfo { private set; get; }
        public static string SpbillCreateIp { private set; get; }
        public static string OauthScope { private set; get; }
        public static int Report_Levenl { set; get; }
        public static string PayNotifyUrl { set; get; }

        public static AccessTokenHelper AccessTokenHelper { private set; get; }

        public static void Register()
        {
            Token = System.Configuration.ConfigurationManager.AppSettings["Token"];
            EncodingAESKey = System.Configuration.ConfigurationManager.AppSettings["EncodingAESKey"];
            AppID = System.Configuration.ConfigurationManager.AppSettings["AppID"];
            AppSecret = System.Configuration.ConfigurationManager.AppSettings["AppSecret"];
            PartnerKey = System.Configuration.ConfigurationManager.AppSettings["PartnerKey"];
            Domain = System.Configuration.ConfigurationManager.AppSettings["Domain"];
            APIDomain = System.Configuration.ConfigurationManager.AppSettings["APIDomain"];
            mch_id = System.Configuration.ConfigurationManager.AppSettings["mch_id"];
            DeviceInfo = System.Configuration.ConfigurationManager.AppSettings["device_info"];
            SpbillCreateIp = System.Configuration.ConfigurationManager.AppSettings["spbill_create_ip"];
            var openJSSDK = int.Parse(System.Configuration.ConfigurationManager.AppSettings["OpenJSSDK"]) > 0;
            OauthScope = System.Configuration.ConfigurationManager.AppSettings["OauthScope"];
            Report_Levenl = int.Parse(System.Configuration.ConfigurationManager.AppSettings["Report_Levenl"]);
            PayNotifyUrl = System.Configuration.ConfigurationManager.AppSettings["PayNotifyUrl"];

            WeixinDomain = "";
            AccessToken = BasicAPI.GetAccessToken(AppID, AppSecret).access_token;

            WeixinSDK.GlobalContext.AppID = AppID;
            WeixinSDK.GlobalContext.AppSecret = AppSecret;
            WeixinSDK.GlobalContext.AccessToken = AccessToken;
            WeixinSDK.GlobalContext.WebAccessToken = WebAccessToken;
            WeixinSDK.GlobalContext.Domain = Domain;
            WeixinSDK.GlobalContext.MCHId = mch_id;
            WeixinSDK.GlobalContext.PartnerKey = PartnerKey;
            WeixinSDK.GlobalContext.DeviceInfo = DeviceInfo;
            WeixinSDK.GlobalContext.SpbillCreateIp = SpbillCreateIp;
            WeixinSDK.GlobalContext.Report_Levenl = Report_Levenl;
            WeixinSDK.GlobalContext.PayNotifyUrl = PayNotifyUrl;
            AccessTokenHelper = new AccessTokenHelper(6000, AppID, AppSecret, openJSSDK);
            AccessTokenHelper.Run();
        }
    }
}