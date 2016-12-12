using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Totyu.WeixinWeb
{
    public static class MyExtensions
    {
        /// <summary>
        /// 重写的TextBoxFor方法 配合Model中自定义特性从消息配置文件中获得MessageInfo
        /// </summary>
        public static MvcHtmlString MyTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            MemberExpression body = expression.Body as MemberExpression;
            if (body != null)
            {
                JObject jobj = JObject.FromObject(htmlAttributes);
                jobj.Add("data-val", "true");
                jobj.Add("data-val-required", jobj.GetValue("placeholder"));

                MvcHtmlString ms = htmlHelper.TextBoxFor<TModel, TValue>(expression, jobj);
                return ms;

            }
            else
            {
                throw new ArgumentException();
            }

        }

        /// <summary>
        /// 重写的PasswordFor方法 配合Model中自定义特性从消息配置文件中获得MessageInfo
        /// </summary>
        public static MvcHtmlString MyPasswordFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            MemberExpression body = expression.Body as MemberExpression;
            if (body != null)
            {
                JObject jobj = JObject.FromObject(htmlAttributes);

                jobj.Add("data-val", "true");
                jobj.Add("data-val-required", jobj.GetValue("placeholder"));

                MvcHtmlString ms = htmlHelper.PasswordFor<TModel, TValue>(expression, jobj);
                return ms;

            }
            else
            {
                throw new ArgumentException();
            }

        }

    }
}
