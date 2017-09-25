$(function () {
    GetOaMeun();
    if (getCookie('skin-color')) {
        var c = getCookie('skin-color');
        if (c) {
            $('head').append('<link id="skinstyle" rel="stylesheet" href="/Content/css/style.' + c + '.css" type="text/css" />');
            setCookie("skin-color", c, 'd30');
        }
    }


    $(".breadcrumbs .right").click(function () {
        $(".skin-color").show();
    })

    $('.skin-color a').click(function () {
        var s = $(this).attr('class');
        if ($('#skinstyle').length > 0) {
            if (s != 'default') {
                delCookie('skin-color');
                $('#skinstyle').attr('href', '/Content/css/style.' + s + '.css');
                setCookie('skin-color', s, 'd30');
            } else {
                $('#skinstyle').remove();
                setCookie("skin-color", '', 'd30');
            }
        } else {
            if (s != 'default') {
                delCookie('skin-color');
                $('head').append('<link id="skinstyle" rel="stylesheet" href="/Content/css/style.' + s + '.css" type="text/css" />');
                setCookie("skin-color", s, 'd30');
            }
        }
        $(".skin-color").hide();
        return false;
    })
   
})
// 加载iframe 主窗体高度
function lowheight(iframe) {
    iframe.height = $(window).height() - 176;
}

function GetOaMeun() {
    var url = "/Ajax/GetOaMeun";
    var par = "";
    EDUCAjax(par, function () {

    },
    function (re) {
        if (undefined != re && re.length > 0) {
            var Str = "";
            for (var i = 0; i < re.length; i++) {
                if (re[i].MenuCode.length == 2) {
                    Str += "<li class=\"downmenu\"><a><span class=\"iconfa-briefcase\"></span> " + re[i].MenuName + "</a>";
                    Str += "<ul>";
                    for (var a = 0; a < re.length; a++) {
                        var MenuCode = re[a].MenuCode.substring(0, 2);
                        if (MenuCode == re[i].MenuCode && re[a].MenuCode != re[i].MenuCode) {
                            Str += "<li><a href=\"" + re[a].Url + "\" target=\"left\" >" + re[a].MenuName + "</a></li>";
                        }
                    }
                    Str += "</ul>";
                    Str += "</li> ";
                }
            }
            $(".nav-tabs").append(Str);
            $(".dropdown").click(function () {
                if ($(this).children(".dropdown-menu").css("display") == "none") {
                    $(this).addClass("open").siblings().removeClass("open");
                }
                else {
                    $(this).removeClass("open");
                }
            });
            $(".downmenu > a").click(function () {
                if ($(this).next().css("display") == "none") {
                    $(this).next().slideDown('fast');
                }
                else { $(this).next().slideUp('fast'); }
            });
        }
        else {
            $('.err').show();

        }
    }, url, "json", false, "数据加载中，请耐心等待...", true);
}

