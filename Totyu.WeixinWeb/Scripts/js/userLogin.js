var curCount;//当前剩余秒数

//当点击图片时，刷新验证码
$(function () {
    $("#aValiCode,#imgValiCode").valiImgGenerate();
    $("de,#aValiCode1,#imgValiCode1").valiImgGenerate();
    $("#em2").hide();
    $("#errormsg").css("margin-bottom", "0px");

    //快速登录时，有错误，返回页面时tab选中
    if (type == 2) {
        FastLoginTab();
    }

    //tab的click事件
    $(".denglv").click(function () {
        LoginTab();
    });
    $(".kuai_denglv").click((function () {
        FastLoginTab();
    }))


    var loginHandler = function () {
        if ($("#frmLogin").valid()) {
            $("#frmLogin").submit();
        }
    };

    //submit事件
    $("#btnSubmit").on("click", loginHandler);
    $("#ValidatorCode,#login_input_pwd").keydown(function (e) {
        if (e.keyCode == 13) loginHandler();
    });

    //手机验证
    var fastLoginHandler = function () {
        if ($("#frmfastLogin").valid()) {
            $("#frmfastLogin").submit();
        }
    };
    $("#fast_login_input_pwd").keydown(function (e) {
        if (e.keyCode == 13) fastLoginHandler();
    });
    $("#btnSubmit1").on("click", fastLoginHandler);

    //点击发送验证码
    $("#btnSendSms").on("click", function () {
        if ($("#MobileNo").valid() && $("#ImageValidatorCode").valid() && typeof ($(this).attr("disabled")) == "undefined") {
            sendMessage();
        }
    });
});

//登录tab点击效果处理
function LoginTab() {
    $("#em1").show();
    $("#em2").hide();
    $(".shop_tab_2").css("display", "none");
    $(".shop_tab_1").show();
}

//快速登录tab点击效果处理
function FastLoginTab() {
    $("#em1").hide();
    $("#em2").show();
    $(".shop_tab_1").hide();
    $(".shop_tab_2").css("display", "block");
}

function sendMessage() {
    if ($("#MobileNo").valid() && $("#ImageValidatorCode").valid()) {
        var sMobileNo = $("#MobileNo").val();
        var sImageValidatorCode = $("#ImageValidatorCode").val();
        curCount = count;
        //设置button效果，开始计时

        var mobileno_ImageValidatorCode = sMobileNo + "#" + sImageValidatorCode;
        //向后台发送处理数据
        $.mvcPost({
            url: "/Common/SendFastLoginSms",
            //data: { moblieNo: sMobileNo, ImageValidatorCode: sImageValidatorCode },
            data: { moblieNo: mobileno_ImageValidatorCode },
            success: function (data) {

                if (data == 1) {
                    $("#btnSendSms").attr("disabled", "true");
                    $("#btnSendSms").text(curCount + "秒");
                    InterValObj = window.setInterval(SetRemainTime, 1000); //启动计时器，1秒执行一次
                }
                else if (data == 3) {

                }
                else if (data == 4) {
                    msgBox($.msg.ValidatorCodeError);
                }
                else {
                    msgBox($.msg.SendSmsError);
                }
            },
            errMsg: "短信发送失败！"
        });
    }
}
//timer处理函数
function SetRemainTime() {
    if (curCount == 0) {
        window.clearInterval(InterValObj);//停止计时器
        $("#btnSendSms").removeAttr("disabled");//启用按钮
        $("#btnSendSms").text("发送验证码");
        code = ""; //清除验证码。如果不清除，过时间后，输入收到的验证码依然有效
    }
    else {
        curCount--;
        $("#btnSendSms").text(curCount + "秒");
    }
}