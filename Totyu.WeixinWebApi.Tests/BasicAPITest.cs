using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Totyu.WeixinSDK.Apis.MP;

namespace Totyu.WeixinWebApi.Tests
{
    [TestClass]
    public class BasicAPITest : BaseApiTest
    {
        [TestMethod]
        public void CheckSignature()
        {

        }
        [TestMethod]
        public void GetAccessToken()
        {
            WeixinConfig.AccessToken = BasicAPI.GetAccessToken(WeixinConfig.AppID, WeixinConfig.AppSecret).access_token;
        }
        [TestMethod]
        public void GetCallbackIP()
        {
            BasicAPI.GetCallbackIP(WeixinConfig.AccessTokenHelper.GetToken());
        }


    }
}
