$(function () {

    if (getCookie('skin-color')) {
        var c = getCookie('skin-color');
        if (c) {
            $('head').append('<link id="skinstyle" rel="stylesheet" href="/Content/css/style.' + c + '.css" type="text/css" />');
            setCookie("skin-color", c, 'd30');
        }
    }
    //获取URL参数:jQuery 参数获取url已解密
    $.getUrlParam = function (name) {
        /*
        encodeURI();加密url
        decodeURI();解密url
        */
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return decodeURI(r[2]); return null;
    }
    $.getQueryStringByName = function (name) {
        var result = location.search.match(new RegExp("[\?\&]" + name + "=([^\&]+)", "i"));

        if (result == null || result.length < 1) {
            return "";
        }
        return result[1];
    }
    //转换字符串，错误数据显示空白
    $.toString = function (str) {
        if (typeof str == "undefined" || (str + "") == "" || str == null || str == "") {
            return "";
        } else {
            return str + "";
        }
    }

    //转换int，错误数据显示0
    $.toInt = function (str) {
        if (typeof str == "undefined" || str == null || str == "" || isNaN(parseInt(str))) {
            return 0;
        } else if (parseInt(str) < 0) {
            return 0;
        } else {
            return parseInt(str);
        }
    }

    //转换浮点数，错误数据显示0
    $.toFloat = function (str) {
        if (typeof str == "undefined" || str == null || str == "" || isNaN(parseFloat(str))) {
            return 0;
        } else {
            return $.toFixed(str, 2);
        }
    }

    //转换成XX.XX为小数，默认两位
    $.toFixed = function (str, length) {
        length = arguments.length > 1 ? length : 2;
        if (typeof str == "undefined" || str == null || str == "" || isNaN(parseFloat(str))) {
            return 0;
        } else {
            return parseFloat(parseFloat(str).toFixed(length));
        }
    }

    ///删除首位逗号
    $.toTrimComma = function (str) {
        return str.replace(/,$/g, "").replace(/^,/g, "")

    }
    $.currentDate = function () {
        /// <summary>日期格式化</summary>     
        /// <returns type="currentDate">The area.</returns>
        var myDate = new Date();

        var currentDate = {};
        currentDate.Year = myDate.getFullYear().toString();
        currentDate.Month = (myDate.getMonth() + 1).toString().length < 2 ? "0" + (myDate.getMonth() + 1).toString() : (myDate.getMonth() + 1).toString();
        currentDate.Day = myDate.getDate().toString().length < 2 ? "0" + myDate.getDate().toString() : myDate.getDate().toString();
        currentDate.Hours = myDate.getHours().toString().length < 2 ? "0" + myDate.getHours().toString() : myDate.getHours().toString();
        currentDate.Minutes = myDate.getMinutes().toString().length < 2 ? "0" + myDate.getMinutes().toString() : myDate.getMinutes().toString();
        currentDate.Seconds = myDate.getSeconds().toString().length < 2 ? myDate.getSeconds().toString() : myDate.getSeconds().toString();
        currentDate.ShortDate = currentDate.Year + "-" + currentDate.Month + "-" + currentDate.Day;
        currentDate.ShortTime = currentDate.Hours + ":" + currentDate.Minutes + ":" + currentDate.Seconds;
        currentDate.DateTime = currentDate.ShortDate + " " + currentDate.ShortTime;
        return currentDate;
    }
    //关闭页面弹出
    $.closezhezhao = function (elem, fullbg) {
        $("#" + fullbg).fadeOut(500);
        $("#" + elem).fadeOut(500);

    }
    //定位及显示
    $.dingwei = function (elem, fullbg) {
        var h = document.documentElement;
        var w = ($(document).width() - $("#" + elem).width()) / 2;
        $("#" + elem).css("top", ((h.clientHeight - $("#" + elem).height()) / 2) + "px");
        $("#" + elem).css("left", w + "px");
        $("#" + fullbg).fadeIn(1000);
        $("#" + elem).fadeIn(1000);
    }

});
String.prototype.trim = function () {
    return this.replace(/(^\s*)|(\s*$)/g, "");
}
Date.prototype.format = function (mask) {

    var d = this;

    var zeroize = function (value, length) {

        if (!length) length = 2;

        value = String(value);

        for (var i = 0, zeros = ''; i < (length - value.length) ; i++) {

            zeros += '0';

        }

        return zeros + value;

    };

    return mask.replace(/"[^"]*"|'[^']*'|\b(?:d{1,4}|m{1,4}|yy(?:yy)?|([hHMstT])\1?|[lLZ])\b/g, function ($0) {

        switch ($0) {

            case 'd': return d.getDate();

            case 'dd': return zeroize(d.getDate());

            case 'ddd': return ['Sun', 'Mon', 'Tue', 'Wed', 'Thr', 'Fri', 'Sat'][d.getDay()];

            case 'dddd': return ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'][d.getDay()];

            case 'M': return d.getMonth() + 1;

            case 'MM': return zeroize(d.getMonth() + 1);

            case 'MMM': return ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'][d.getMonth()];

            case 'MMMM': return ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'][d.getMonth()];

            case 'yy': return String(d.getFullYear()).substr(2);

            case 'yyyy': return d.getFullYear();

            case 'h': return d.getHours() % 12 || 12;

            case 'hh': return zeroize(d.getHours() % 12 || 12);

            case 'H': return d.getHours();

            case 'HH': return zeroize(d.getHours());

            case 'm': return d.getMinutes();

            case 'mm': return zeroize(d.getMinutes());

            case 's': return d.getSeconds();

            case 'ss': return zeroize(d.getSeconds());

            case 'l': return zeroize(d.getMilliseconds(), 3);

            case 'L': var m = d.getMilliseconds();

                if (m > 99) m = Math.round(m / 10);

                return zeroize(m);

            case 'tt': return d.getHours() < 12 ? 'am' : 'pm';

            case 'TT': return d.getHours() < 12 ? 'AM' : 'PM';

            case 'Z': return d.toUTCString().match(/[A-Z]+$/);

                // Return quoted strings with the surrounding quotes removed      

            default: return $0.substr(1, $0.length - 2);

        }

    });

};
/*Ajax方法
  data：需要上传的数据
  beforeback：Ajax执行之前(回调函数)
  callback：Ajax获取请求后的数据(回调函数)
  url：请求地址
  datatype：数据类型
  lodingState：是否显示loding
  lodingMsg：loding 提示语
  async:：是否异步，默认异步
*/
function EDUCAjax() {
    var data = arguments[0];
    var beforeback = arguments[1];
    var callback = arguments[2];
    var url = arguments[3] + "?" + Math.random();
    var datatype = arguments.length > 4 ? arguments[4] : "json";
    var lodingState = arguments.length > 5 ? arguments[5] : false;
    var lodingMsg = arguments.length > 6 ? arguments[6] : "数据加载中，请耐心等待...";
    var async = arguments.length > 7 ? arguments[7] : true;

    // var loding = $("#id_loding");//loading提示
    $.ajax({
        type: "POST",
        dataType: datatype.toLowerCase(),
        //contentType: "application/json;charset=utf-8",
        url: url + "&" + Math.random(),
        async: async,
        beforeSend: function () {
            //显示loding提示
            if (lodingState) {
                //$("#id_lodingMsg").html(lodingMsg);
                //loding.show();
            }
            beforeback();
        },
        data: data,
        success: function (res) {
            //if (lodingState) {
            //    loding.hide();
            //}
            callback(res);
        },
        error: function () { }

    });
}

var getCookie = function (name) {
    var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
    if (arr = document.cookie.match(reg))
        return unescape(arr[2]);
    else
        return null;
}
var delCookie = function (name) {
    var exp = new Date();
    exp.setTime(exp.getTime() - 1);
    var cval = getCookie(name);
    if (cval != null)
        document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString();
}
var setCookie = function (name, value, time) {
    var strsec = arguments.length > 2 ? getsec(arguments[2]) : getsec('d30');
    var exp = new Date();
    exp.setTime(exp.getTime() + strsec * 1);
    document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString() + ";path=/";
}
function getsec(str) {

    var str1 = str.substring(1, str.length) * 1;
    var str2 = str.substring(0, 1);
    if (str2 == "s") {
        return str1 * 1000;
    }
    else if (str2 == "h") {
        return str1 * 60 * 60 * 1000;
    }
    else if (str2 == "d") {
        return str1 * 24 * 60 * 60 * 1000;
    }
}
//跳转地址，加随机数url:跳转路径，isparent是否从父窗体打开，默认false
function gotourl(url, isparent) {
    /*
     encodeURI();加密url
     decodeURI();解密url
     */
    isparent = arguments.length > 1 ? isparent : false;
    var par = '';
    if (url.indexOf('?') < 0) {
        par = '?';
    }
    else {
        par = '&';
    }
    if (isparent) {
        //window.parent.location.href = encodeURI(url + par + 'v=' + String(random(1111, 999999)));
        window.open(encodeURI(url + par + 'v=' + String(random(1111, 999999))));
    } else {
        location.href = encodeURI(url + par + 'v=' + String(random(1111, 999999)));
    }
}
function OpenFile() {
    var path = document.getElementById('FileUpload1').value;
    document.getElementById('fileupload').innerHTML = path;
    var Img_O = $.toString($("#Img_O").attr("src"));
    if (path != "") {
        $("#img").val("1");
    }
    if (Img_O != "") {
        $("#Img_O").remove();
    }
}
function GetH() {
    var height = $(".maincontent").height();  
    if (height == 0) {
        height = 635;
    }
    parent.window.document.getElementById("left").height = height;
}