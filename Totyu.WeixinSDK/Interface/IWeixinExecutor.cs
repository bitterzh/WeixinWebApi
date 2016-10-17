using Totyu.WeixinSDK.Entiyies;

namespace Totyu.WeixinSDK.Interface
{
    public interface IWeixinExecutor
    {
        /// <summary>
        /// 接受消息后返回XML
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        string Execute(WeixinMessage message);
    }
}
