var Dfbg = { OA: {} }
Dfbg.OA.Home = {
    AjaxList: function (url, data) {
        $.ajax({
            type: "POST",
            url: url,
            data: data,
            dataType: "html",
            beforeSend: function (XMLHttpRequest) {
            },
            success: function (msg) {
            },
            complete: function (XMLHttpRequest, textStatus) {

            },
            error: function (e, x) {
            }
        });
    },
    Pai: function (Code, orderCode, order) {
        var _base = Dfbg.OA.Home;
        var url = "/Ajax/OderOa";
        var par = "Code=" + Code + "&orderCode=" + orderCode + "&order=" + order + "";
        _base.AjaxList(url, par);
    },
    GetPai: function () {
        var url = "/Ajax/GetMeun";
        var par = "";
        EDUCAjax(par, function () {
        },
        function (re) {
            if (undefined != re && re.length > 0) {
                var SumLine = re.length % 6 == 0 ? re.length / 6 : parseInt(re.length / 6) + 1;
                var Count = re.length > 6 ? SumLine * 20 + "%" : "20%";
                $(".List").css("height", Count);
                var str = "";
                for (var i = 0; i < re.length; i++) {
                    var Count2 = re.length > 6 ? 100 / SumLine + "%" : "100%";
                    str += "<div Code=" + re[i].Code + " class=\"col-xs-71 grid \" style=\"height:" + Count2 + "\">";
                    if (i != 0)
                        str += "<div class=\"grid-menu-left\" onclick=\"UpThis(this,'" + re[i].Code + "')\"> </div>";
                    if (i != re.length - 1)
                        str += "<div class=\"grid-menu-right\" onclick=\"DownThis(this,'" + re[i].Code + "')\"> </div>";
                    str += "<a class=\"flip\" href=\"#\">";
                    str += "<span class=\"back-color\"></span> <img src=\"/Content/Img/" + re[i].img + "\" />";
                    str += "<span>" + re[i].oaName + "</span>";
                    str += "</a>";
                    str += "</div>";
                }
                $(".ListImg").append(str);
            }
            else {
                $('.err').show();
            }
        }, url);
    },
    GetMeun: function () {
        var url = "/Ajax/GetMeun";
        var par = "";
        EDUCAjax(par, function () {

        }, function (re) {
          
            if (undefined != re && re.length > 0 && re !="0") {
                var SumLine = re.length % 6 == 0 ? re.length / 6 : parseInt(re.length / 6) + 1;
                var Count = re.length > 6 ? SumLine * 32 + "%" : "32%";
                $(".List").css("height", Count);
                var str = "";
                for (var i = 0; i < re.length; i++) {
                    var Count2 = re.length > 6 ? 100/ SumLine + "%" : "100%";

                    str += "<span class=\"col-xs-71 grid\" style=\"height:" + Count2 + "\"> ";
                    str += "<a class=\"flip\" target=\"_blank\" href=\"/Home/Redir?Code=" + re[i].Code + "\">";
                    str += "<span class=\"back-color\"></span> <img src=\"/Content/Img/" + re[i].img + "\" />";
                    str += "<span>" + re[i].oaName + "</span>";
                    str += "</a>";
                    str += "</span>";
                }
                $(".ListImg").append(str);
            }
            else {
                $('.err').show();
            }
        }, url, "json", false,"数据加载中，请耐心等待...", true);
    },
    GetMeun2: function () {
        var url = "/Ajax/GetMeun";
        var par = "";
        EDUCAjax(par, function () {

        }, function (re) {
            if (undefined != re && re.length > 0) {
                var SumLine = re.length % 6 == 0 ? re.length / 6 : parseInt(re.length / 6) + 1;
                var Count = re.length > 6 ? SumLine * 25 + "%" : "25%";
                $(".List").css("height", Count);
                var str = "";
                for (var i = 0; i < re.length; i++) {
                    var Count2 = re.length > 6 ? 100 / SumLine + "%" : "100%";

                    str += "<span class=\"col-xs-81 grid\" style=\"height:" + Count2 + "\"> ";
                    str += "<a class=\"flip\" target=\"_blank\" href=\"/Home/Redir?Code=" + re[i].Code + "\">";
                    str += "<span class=\"back-color\"></span> <img src=\"/Content/Img/" + re[i].img + "\" />";
                    str += "<span>" + re[i].oaName + "</span>";
                    str += "</a>";
                    str += "</span>";
                }
                $(".ListImg").append(str);

            }
            else {
                $('.err').show();

            }
        }, url);
    }
}
function UpThis(Obj, Code) {
    var Up = $(Obj).parent().closest("div");
    var t = $(Obj).parent().index();
    var All = $(Obj).parent().siblings().length;
    var UpCode = Up.prev().attr("Code");
    if (t == 1) {
        $(Obj).remove();
        Up.prev().append("<div class=\"grid-menu-left\" onclick=\"UpThis(this,'" + UpCode + "')\"> </div>");
    }
    if (t == All) {
        Up.append("<div class=\"grid-menu-right\" onclick=\"DownThis(this,'" + Code + "')\"> </div>");
        Up.prev().children().remove("div .grid-menu-right");
    }
    Up.after(Up.prev());

    Dfbg.OA.Home.Pai(Code, UpCode, 'Up');
}

function DownThis(Obj, Code) {
  
    var Down = $(Obj).parent().closest("div");
    var t = $(Obj).parent().index();
    var All = $(Obj).parent().siblings().length;
    var DownCode = Down.next().attr("Code");
    if (t == All - 1) {
        $(Obj).remove();
        Down.next().append("<div class=\"grid-menu-right\" onclick=\"DownThis(this,'" + DownCode + "')\"> </div>");
    }
    if (t == 0) {
        Down.append("<div class=\"grid-menu-left\" onclick=\"UpThis(this,'" + Code + "')\"> </div>");
        Down.next().children().remove("div .grid-menu-left");
    }
    Down.before(Down.next());
    Dfbg.OA.Home.Pai(Code, DownCode, 'Down');
}

