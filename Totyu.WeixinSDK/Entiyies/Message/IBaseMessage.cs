using Totyu.WeixinSDK.Enums;

namespace Totyu.WeixinSDK.Entiyies.Message
{
    public interface IMessageBase : IEntityBase
    {
        string Title { get; set; }
        string ToUserName { get; set; }
        string FromUserName { get; set; }
        string CreateTime { get; set; }
        string Context { get; set; }
    }
}
