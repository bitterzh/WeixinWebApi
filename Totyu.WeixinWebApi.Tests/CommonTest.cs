using Microsoft.VisualStudio.TestTools.UnitTesting;
using Totyu.WeixinSDK.Enums;
using Totyu.WeixinSDK.Helper.Http;

namespace Totyu.WeixinWebApi.Tests
{
    [TestClass]
    public class CommonTest : BaseApiTest
    {
        [TestMethod]
        public void GetUserInfo()
        {
            var _my01 = DynamicJsonSend.SendAsync("http://api.t.totyu.cn/api/My01Info/SearchMy01Info?userId=30080", null, RequestMethod.GET, ContentType.String, true);
        }
    }
}
