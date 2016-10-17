using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Totyu.WeixinSDK.Apis.MP;

namespace Totyu.WeixinWebApi.Tests
{
    [TestClass]
    public class AdminAPITest : BaseApiTest
    {
        [TestMethod]
        public void GetUserInfo()
        {
            var userInfo = AdminAPI.GetUserInfo(WeixinConfig.AccessToken, "oKFB8v0Svn1KTl5h_yRJvr6b2FyY");
            var unionId = AdminAPI.GetUnionId(WeixinConfig.AccessToken, "oKFB8v0Svn1KTl5h_yRJvr6b2FyY");
        }
    }
}
