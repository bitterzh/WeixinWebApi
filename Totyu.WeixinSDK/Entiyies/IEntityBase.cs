using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Totyu.WeixinSDK.Entiyies
{
    public interface IEntityBase
    {
    }
    public interface IJsonIgnoreNull : IEntityBase
    {

    }

    public class JsonIgnoreNull : IJsonIgnoreNull
    {

    }
}
