using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Totyu.WeixinSDK.Apis.Pay;
using Business = Totyu.WeixinSDK.Apis.Pay.Business;
using Totyu.WeixinSDK.Helper;
using Totyu.WeixinSDK.Entiyies;

namespace Totyu.WeixinWebApi.Controllers
{
    public class WeixinPay2Controller : Controller
    {
        #region demo
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult MicroPay()
        {
            return View();
        }
        [HttpPost]
        public void MicroPay(string body, string fee, string auth_code)
        {
            string _result = Business.MicroPay.Run(body, fee, auth_code);
            Response.Write("<textarea rows='3' cols='2' style='color:#00CD00;font-size:20px;width:98%;' >" + _result + "</textarea>");
        }

        /// <summary>
        /// 静态链接二维码支付，二种模式
        /// 唯一要确定的参数是产品的ID号
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult NativePay(string productId = "")
        {
            if (!string.IsNullOrEmpty(productId))
            {
                string _model1 = Business.NativePay.GetPrePayUrl(productId);
                //统一下单二维码生成
                string _model2 = Business.NativePay.GetPayUrl(productId);
                ViewBag.Model1 = _model1;
                ViewBag.Model2 = _model2;
            }
            return View();
        }

        [HttpGet]
        public ActionResult OrderQuery()
        {
            return View();
        }
        [HttpPost]
        public void OrderQuery(string transactionId, string outTradeNo)
        {
            string _result = Business.OrderQuery.Run(transactionId, outTradeNo);
            Response.Write("<textarea rows='3' cols='2' style='color:#00CD00;font-size:20px;width:98%;' >" + _result + "</textarea>");
        }

        [HttpGet]
        public ActionResult RefundOrder()
        {
            return View();
        }

        [HttpPost]
        public void RefundOrder(string transactionId, string outTradeNo, string totalFee, string refundFee)
        {
            dynamic _result = Business.RefundOrder.Run(transactionId, outTradeNo, totalFee, refundFee);
            Response.Write("<textarea rows='3' cols='2' style='color:#00CD00;font-size:20px;width:98%;' >" + _result + "</textarea>");
        }
        [HttpGet]
        public ActionResult RefundQuery()
        {
            return View();
        }

        [HttpPost]
        public void RefundQuery(string transactionId, string outTradeNo, string outRefoundNo, string refundId)
        {
            string _result = Business.RefundQuery.Run(transactionId, outTradeNo, outRefoundNo, refundId);
            Response.Write("<textarea rows='3' cols='2' style='color:#00CD00;font-size:20px;width:98%;' >" + _result + "</textarea>");
        }

        [HttpGet]
        public ActionResult DownloadBill()
        {
            return View();
        }

        [HttpPost]
        public void DownloadBill(string billDate, string billType)
        {
            string _result = Business.DownloadBill.Run(billDate, billType);
            Response.Write("<textarea rows='3' cols='2' style='color:#00CD00;font-size:20px;width:98%;' >" + _result + "</textarea>");
        }
        [HttpGet]
        public ActionResult JsApiPay(string openid, string total_fee)
        {
            ViewBag.PayOpenId = openid;
            ViewBag.TotleFee = total_fee;
            return View();
        }

        [HttpPost]
        public void JsApiPayPost(string openid, string total_fee)
        {
            ViewBag.PayOpenId = openid;
            ViewBag.TotleFee = total_fee;
            //检测是否给当前页面传递了相关参数
            if (string.IsNullOrEmpty(openid) || string.IsNullOrEmpty(total_fee))
                Response.Write("<span style='color:#FF0000;font-size:20px'>" + "页面传参出错,请返回重试" + "</span>");

            //若传递了相关参数，则调统一下单接口，获得后续相关接口的入口参数
            Business.JsApiPay jsApiPay = new Business.JsApiPay();
            jsApiPay.Request = this.Request;
            jsApiPay.Response = this.Response;
            jsApiPay.openid = openid;
            jsApiPay.total_fee = int.Parse(total_fee);

            //JSAPI支付预处理
            try
            {
                WxPayData unifiedOrderResult = jsApiPay.GetUnifiedOrderResult();
                ViewBag.WxJsApiParam = jsApiPay.GetJsApiParameters();//获取H5调起JS API参数          
                //在页面上显示订单信息
                Response.Write("<span style='color:#00CD00;font-size:20px'>订单详情：</span><br/>");
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + unifiedOrderResult.ToPrintStr() + "</span>");

            }
            catch (Exception ex)
            {
                Response.Write("<span style='color:#FF0000;font-size:20px'>" + "下单失败，请返回重试" + "</span>");
            }
        }

        #endregion
    }
}