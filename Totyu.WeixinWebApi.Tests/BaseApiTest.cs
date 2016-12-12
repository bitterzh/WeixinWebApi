using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Totyu.WeixinWebApi.Tests
{
    [TestClass]
    public class BaseApiTest
    {
        private void RegisterConfig()
        {
            if (string.IsNullOrEmpty(WeixinConfig.AppID))
                WeixinConfig.Register();
        }

        [TestInitialize]
        public void ClassInitialize()
        {
            RegisterConfig();

        }
    }
}
