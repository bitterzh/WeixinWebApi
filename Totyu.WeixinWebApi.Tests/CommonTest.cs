using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Totyu.WeixinSDK.Helper;

namespace Totyu.WeixinWebApi.Tests
{
    [TestClass]
    public class CommonTest : BaseApiTest
    {
        [TestMethod]
        public void GetUserInfo()
        {
            var _my01 = HttpClientHelper.HttpClientGetAsync("http://api.t.totyu.cn/api/My01Info/SearchMy01Info?userId=30080", true);
        }
    }
}
