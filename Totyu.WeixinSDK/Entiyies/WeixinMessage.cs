using Totyu.WeixinSDK.Enums;

namespace Totyu.WeixinSDK.Entiyies
{
    public class WeixinMessage
    {
        public virtual WeixinMessageType Type { set; get; }
        public virtual dynamic Body { set; get; }
    }
}
