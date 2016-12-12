//var ResourcePath = "http://localhost:8083/resource";

var ResourcePath = "https://01world.blob.core.chinacloudapi.cn/resource";

jQuery.fn.extend({
    displayNoDataArea: function (msg, imgurl) {
        var htmltxt = "<center class='no_info' style='margin:5em 0;'>"

        if (imgurl == undefined || imgurl == "") {
            htmltxt = htmltxt + "<img src='http://m.t.totyu.cn/Content/images/no_info_pic.png'/>";
        } else {
            htmltxt = htmltxt + "<img src='" + imgurl + "'/>";
        }

        if (msg == undefined || msg == "") {
            htmltxt = htmltxt + "<p>" + $.msg.NoDataDefaultMsg + "</p></center>";
        } else {
            htmltxt = htmltxt + "<p>" + msg + "</p></center>";
        }

        $(this).html(htmltxt);

    },

    valiImgGenerate: function (pageId) {
        pageId = pageId || "login";
        var validImg;
        $(this).each(function () {
            if ($(this).prop("tagName").toLowerCase() == "img") {
                $(this).attr("src", "http://m.t.totyu.cn/User/GetValidatorGraphics?pageId=" + pageId + "&time=" + (new Date()).getTime());
                validImg = $(this);
                validImg.refresh = function () {
                    validImg.attr("src", "http://m.t.totyu.cn/User/GetValidatorGraphics?pageId=" + pageId + "&time=" + (new Date()).getTime());
                };
            }
        });

        $(this).each(function () {
            $(this).on("click", function () {
                validImg.refresh();
            });
        });

        return validImg;
    }
});

(function ($) { });