function Add() {
    var sort=  $.toInt($("#sort").val());
    var oaName = $.toString($("#oaName").val());
    var link = $.toString($("#link").val());
    var FileUpload1 = $.toString($("#img").val());

    if (oaName == "") {
        alert("请输入系统名称！");
        return false;
    }
    if (link == "") {
        alert("请输入系统地址！");
        return false;
    }
    if (sort == 0) {
        alert("请输入系统排序,且必须为正整事！");
        return false;
    }
    if (FileUpload1 == "") {
        alert("请上传系统图标！");
        return false;
    }
    return true ;
}