using System.Net.Http;
using Totyu.WeixinSDK.Helper;

namespace Totyu.WeixinSDK.Apis.MP
{
    public class CustomMenuAPI : BaseAPI
    {
        /// <summary>
        /// 自定义菜单创建接口
        /// </summary>
        /// <param name="token"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool Create(string accesstoken, string content)
        {
            var url = string.Format(ApiUrlHelper.MPApiUrl.CustomMenuCreate, accesstoken);
            var result = PostStringAsync(url, content).errcode;
            return result == 0;
        }

        /// <summary>
        /// 自定义菜单查询接口
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static dynamic Query(string accesstoken)
        {
            var url = string.Format(ApiUrlHelper.MPApiUrl.CustomMenuQuery, accesstoken);
            return GetAsync(url);
        }

        /// <summary>
        /// 自定义菜单删除接口
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool Delete(string accesstoken)
        {
            var url = string.Format(ApiUrlHelper.MPApiUrl.CustomMenuDelete, accesstoken);
            var result = GetAsync(url);
            return result.errmsg == "ok";
        }
    }
}
