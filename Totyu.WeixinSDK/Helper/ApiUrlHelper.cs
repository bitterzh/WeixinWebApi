using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Totyu.WeixinSDK.Helper
{
    /// <summary>
    /// 微信各平台api url
    /// </summary>
    public class ApiUrlHelper
    {
        #region Open 开放平台 

        /// <summary>
        /// 开放平台Api
        /// https://open.weixin.qq.com/cgi-bin/showdocument?action=dir_list&t=resource/res_list&verify=1&id=1417674108&token=&lang=zh_CN
        /// </summary>
        public static class OpenApiUrl
        {
            #region Basic
            /*
             * scope:
             * 1)snsapi_base:
             * 1 /sns/oauth2/access_token--通过code换取access_token、refresh_token和已授权scope
               2 /sns/oauth2/refresh_token	刷新或续期access_token使用
               3 /sns/auth	检查access_token有效性
             * 2)snsapi_userinfo:获取用户个人信息
             * 3)snsapi_login: web授权
             * 
             * state: 
             * 
             */
            /// <summary>
            /// step 1(web)
            /// 
            /// (android)
            /// Final SendAuth.Req req = new SendAuth.Req();
            /// req.scope = "snsapi_userinfo";
            /// req.state = "wechat_sdk_demo_test";
            /// api.sendReq(req);
            /// 
            /// http请求方式: GET
            /// 请求CODE
            /// </summary>
            public static string GetCode = "https://open.weixin.qq.com/connect/qrconnect?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_login&state=WEINXIN_LOGIN#wechat_redirect";
            /// <summary>
            /// step 2
            /// http请求方式: GET
            /// 通过code获取access_token
            /// </summary>
            public static string GetAccessToken = "https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code=CODE&grant_type=authorization_code";
            /// <summary>
            /// step 3
            /// http请求方式: GET
            /// 获取用户个人信息（UnionID机制）
            /// </summary>
            public static string GetUserInfo = "https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}";
            #endregion

        }

        #endregion

        #region MP 公众平台 
        /// <summary>
        /// 公众平台Api
        /// http://mp.weixin.qq.com/wiki/8/f9a0b8382e0b77d87b3bcc1ce6fbc104.html
        /// </summary>
        public static class MPApiUrl
        {
            #region Basic
            /*
             * 开始开发
             * http://mp.weixin.qq.com/wiki/14/9f9c82c1af308e3b14ba9b973f99a8ba.html
             */
            /// <summary>
            /// http请求方式: GET
            /// 获取AccessToken
            /// grant_type [client_credential(获取access_token),]
            /// </summary>
            public static string GetAccessToken = "https://api.weixin.qq.com/cgi-bin/token?grant_type={0}&appid={1}&secret={2}";
            /// <summary>
            /// http请求方式: GET
            /// 获取微信服务器IP地址
            /// </summary>
            public static string GetCallbackIP = "https://api.weixin.qq.com/cgi-bin/getcallbackip?access_token={0}";
            #endregion

            #region Admin

            #region Group
            /// <summary>
            /// http请求方式: POST（请使用https协议）
            /// 创建分组
            /// </summary>
            public static string CreateGroup = "https://api.weixin.qq.com/cgi-bin/groups/create?access_token={0}";
            /// <summary>
            /// http请求方式: GET（请使用https协议）
            /// 查询所有分组
            /// </summary>
            public static string GetGroups = "https://api.weixin.qq.com/cgi-bin/groups/get?access_token={0}";
            /// <summary>
            /// http请求方式: POST（请使用https协议）
            /// 查询用户所在分组
            /// </summary>
            public static string GetUserGroup = "https://api.weixin.qq.com/cgi-bin/groups/getid?access_token={0}";
            /// <summary>
            /// http请求方式: POST（请使用https协议）
            /// 修改分组名
            /// </summary>
            public static string UpdateUserGroup = "https://api.weixin.qq.com/cgi-bin/groups/update?access_token={0}";
            /// <summary>
            /// http请求方式: POST（请使用https协议）
            /// 移动用户分组
            /// </summary>
            public static string UpdateMemberGroup = "https://api.weixin.qq.com/cgi-bin/groups/members/update?access_token={0}";
            /// <summary>
            /// http请求方式: POST（请使用https协议）
            /// 批量移动用户分组
            /// </summary>
            public static string UpdateMembersGroup = "https://api.weixin.qq.com/cgi-bin/groups/members/batchupdate?access_token={0}";
            /// <summary>
            /// http请求方式: POST（请使用https协议）
            /// 删除分组
            /// </summary>
            public static string DeleteUserGroup = "https://api.weixin.qq.com/cgi-bin/groups/delete?access_token={0}";
            #endregion

            #region User
            /// <summary>
            /// http请求方式: POST（请使用https协议）
            /// 设置备注名
            /// </summary>
            public static string UpdateUserRemark = "https://api.weixin.qq.com/cgi-bin/user/info/updateremark?access_token={0}";
            /// <summary>
            /// http请求方式: GET
            /// 获取用户基本信息
            /// </summary>
            public static string GetUserInfo = "https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN";
            /// <summary>
            /// http请求方式: POST
            /// 批量获取用户基本信息
            /// </summary>
            public static string ListUserInfo = "https://api.weixin.qq.com/cgi-bin/user/info/batchget?access_token={0}";
            /// <summary>
            /// http请求方式: GET（请使用https协议）
            /// 获取用户列表
            /// </summary>
            public static string GetUserList = "https://api.weixin.qq.com/cgi-bin/user/get?access_token={0}&next_openid={1}";

            #endregion

            #region Account
            /*
             * 账户管理
             * http://mp.weixin.qq.com/wiki/18/167e7d94df85d8389df6c94a7a8f78ba.html
             */

            /// <summary>
            /// http请求方式: POST
            /// 创建二维码ticket
            /// 临时 {"expire_seconds": 604800, "action_name": "QR_SCENE", "action_info": {"scene": {"scene_id": 123}}}
            /// 永久 {"action_name": "QR_LIMIT_SCENE", "action_info": {"scene": {"scene_id": 123}}}
            ///      {"action_name": "QR_LIMIT_STR_SCENE", "action_info": {"scene": {"scene_str": "123"}}}
            /// </summary>
            public static string CreateQRCodeTicket = "https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token={0}";

            /// <summary>
            /// HTTP GET请求（请使用https协议）
            /// 通过ticket换取二维码
            /// </summary>
            public static string CreateQRCode = "https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket={0}";

            /// <summary>
            /// http请求方式: POST
            /// 长链接转短链接接口
            /// </summary>
            public static string ShortUrl = "https://api.weixin.qq.com/cgi-bin/shorturl?access_token={0}";
            #endregion

            #endregion

            #region message
            /*
             * 消息管理
             * http://mp.weixin.qq.com/wiki/17/f298879f8fb29ab98b2f2971d42552fd.html
             */

            #region 客服消息
            /*
             * 客服消息
             * http://mp.weixin.qq.com/wiki/11/c88c270ae8935291626538f9c64bd123.html
             */

            /// <summary>
            /// http请求方式: POST
            /// 添加客服帐号
            /// </summary>
            public static string AddCustomerService = "https://api.weixin.qq.com/customservice/kfaccount/add?access_token={0}";
            /// <summary>
            /// http请求方式: POST
            /// 修改客服帐号
            /// </summary>
            public static string UpdateCustomerService = "https://api.weixin.qq.com/customservice/kfaccount/update?access_token={0}";
            /// <summary>
            /// http请求方式: GET
            /// 修改客服帐号
            /// </summary>
            public static string DeleteCustomerService = "https://api.weixin.qq.com/customservice/kfaccount/del?access_token={0}";
            /// <summary>
            /// http请求方式: GET
            /// 获取所有客服账号
            /// </summary>
            public static string ListCustomerService = "https://api.weixin.qq.com/cgi-bin/customservice/getkflist?access_token={0}";
            /// <summary>
            /// http请求方式: POST
            /// 发送(主动) 客服消息
            /// 消息格式
            /// http://mp.weixin.qq.com/wiki/11/c88c270ae8935291626538f9c64bd123.html
            /// </summary>
            public static string RepayActiveText = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}";

            #endregion

            #region 群发消息
            /*
             * 群发消息
             * http://mp.weixin.qq.com/wiki/15/40b6865b893947b764e2de8e4a1fb55f.html
             */

            #endregion

            #endregion

            #region CustomMenu
            /*
             * 自定义菜单
             * http://mp.weixin.qq.com/wiki/10/0234e39a2025342c17a7d23595c6b40a.html
             */
            /// <summary>
            /// http请求方式：POST
            /// 自定义菜单创建接口
            /// {
            /// "button":[
            ///      {	
            ///           "type":"click",
            ///           "name":"今日歌曲",
            ///           "key":"V1001_TODAY_MUSIC"
            ///      },
            ///       {
            ///            "name":"菜单",
            ///            "sub_button":[
            ///            {	
            ///                "type":"view",
            ///                "name":"搜索",
            ///                "url":"http://www.soso.com/"
            ///             },
            ///             {
            ///                "type":"click",
            ///                "name":"赞一下我们",
            ///                "key":"V1001_GOOD"
            ///             }]
            ///       }]
            /// }
            /// </summary>
            public static string CustomMenuCreate = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}";
            /// <summary>
            /// http请求方式：GET
            /// 自定义菜单查询接口
            /// </summary>
            public static string CustomMenuQuery = "https://api.weixin.qq.com/cgi-bin/menu/get?access_token={0}";
            /// <summary>
            /// http请求方式：GET
            /// 自定义菜单删除接口
            /// </summary>
            public static string CustomMenuDelete = "https://api.weixin.qq.com/cgi-bin/menu/delete?access_token={0}";
            /// <summary>
            /// http请求方式：POST
            /// 创建个性化菜单
            /// </summary>
            public static string IndividualizationMenuCreat = "https://api.weixin.qq.com/cgi-bin/menu/addconditional?access_token={0}";
            /// <summary>
            /// http请求方式：GET
            /// 获取自定义菜单配置接口
            /// </summary>
            public static string CustomMenuInfo = "https://api.weixin.qq.com/cgi-bin/get_current_selfmenu_info?access_token={0}";
            #endregion

            #region oauth2

            /*
             * scope:
             * 1)snsapi_base:为scope的网页授权，就静默授权的，用户无感知；
             * 2)snsapi_userinfo:对于已关注公众号的用户，如果用户从公众号的会话或者自定义菜单进入本公众号的网页授权页，即使是scope为snsapi_userinfo，也是静默授权，用户无感知。
             * 
             * state: 
             * 
             */

            /// <summary>
            /// step 1(web)
            /// http请求方式: GET
            /// 用户同意授权，获取code
            /// </summary>
            public static string GetCode = "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_userinfo&state=STATE#wechat_redirect";
            #endregion 
        }
        #endregion

        #region QY 企业平台

        /// <summary>
        /// 企业平台Api
        /// http://qydev.weixin.qq.com/wiki/index.php?title=%E9%A6%96%E9%A1%B5
        /// </summary>
        public static class QYApiUrl
        {

        }

        #endregion
    }

}
