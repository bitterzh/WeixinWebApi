using Totyu.WeixinSDK.Enums;
using Totyu.WeixinSDK.Helper.Http;

namespace Totyu.WeixinSDK.Apis._01World
{
    public class My01API
    {
        public static dynamic GetMy01Info(int userID)
        {
            string _url = "http://api.t.totyu.cn/api/My01Info/SearchMy01Info?userId=" + userID;
            return DynamicJsonSend.SendAsync(_url, null, RequestMethod.GET, ContentType.String, true);
        }
    }
}
