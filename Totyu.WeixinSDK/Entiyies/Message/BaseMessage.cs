using Totyu.WeixinSDK.Enums;

namespace Totyu.WeixinSDK.Entiyies.Message
{
    public class BaseMessage
    {
        public string Title { get; set; }
        public string ToUserName { get; set; }
        public string FromUserName { get; set; }
        public string CreateTime { get; set; }
        public WeixinMessageType MsgType { get; set; }

        public string Context { get; set; }
    }
}
