<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleDetail.aspx.cs" Inherits="EastElite.SMS.Manage.RoleDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>添加角色窗口</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <link rel="stylesheet" type="text/css" href="../css/style.css" media="screen" />      
</head>
<body>
    <form id="form1" method="get" runat="server">
        <div class="basic" style="width: 100%; height: 10px;">
        </div>      
        <div class="innertitle">添加角色窗口</div>
        <div class="basic">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
        </div> 
        <div class="basic" style="width: 100%; height: 10px;">
        </div>          
        <div class="basic">
            <div class="itemname2">
                角色类型<span class="star">*</span></div>
            <div class="itemvalue2">
                <asp:DropDownList ID="RoleTypeList" runat="server" Width="153px">
                </asp:DropDownList>
                <asp:RequiredFieldValidator id="RequiredFieldValidator6" runat="server" ControlToValidate="RoleTypeList"   
                  InitialValue=""  ErrorMessage="角色类型必须选择" Display="None"></asp:RequiredFieldValidator>   
            </div>
        </div> 
        <div class="basic">
            <div class="itemname2">
                角色名称<span class="star">*</span></div>
            <div class="itemvalue2">
                <asp:TextBox ID="Name1" runat="server" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Name1"
                    ErrorMessage="角色名称必须输入" Display="None"  ></asp:RequiredFieldValidator>
            </div>
        </div>            
        <div class="basic">
            <div class="itemname2">
                备&nbsp; &nbsp; &nbsp; &nbsp;注</div>
            <div class="itemvalue2">
                <asp:TextBox ID="Note1" runat="server" TextMode="MultiLine" Width="76.5%" MaxLength="500"></asp:TextBox>
            </div>
        </div>    
        <div class="basic" style="width: 100%; height: 10px;">
        </div>  
        <div class="basic" style="text-align:center">
            <asp:ImageButton ID="InsertButton" runat="server" ImageUrl="~/image/save.jpg"  OnClick="InsertButton_Click" />&nbsp; &nbsp;
            <asp:ImageButton ID="CancelButton" runat="server" ImageUrl="~/image/return.jpg" ValidationGroup="ValidationGroup1" ToolTip="返回角色目录查询列表"  OnClick="CancelButton_Click" />
        </div>
        <div class="basic" style="width: 100%; height: 10px;">
        </div>                            
    </form>
</body>
</html>
