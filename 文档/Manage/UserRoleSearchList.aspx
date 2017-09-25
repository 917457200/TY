<%@ Page Language="C#" MasterPageFile="~/Master/Manage.Master" AutoEventWireup="true" CodeBehind="UserRoleSearchList.aspx.cs" Inherits="EastElite.SMS.Manage.UserRoleSearchList" Title="用户角色查询窗口" MaintainScrollPositionOnPostback="True" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>    

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="innertitle">
        用户角色查询窗口</div>
    <div class="basic">
        <asp:Panel ID="SelectConditionPanel" runat="server" Width="100%" BackColor="#E4EFF3">
            <div class="basic">
                <div class="itemname1">
                    处室/教研组</div>
                <div class="itemvalue1">
                    <asp:DropDownList ID="SubAgencyCodeList" runat="server" Width="153px">
                    </asp:DropDownList>   
                </div>            
                <div class="itemname1">
                    角色列表</div>
                <div class="itemvalue1">
                    <asp:DropDownList ID="RoleNameList" runat="server" Width="153px">
                    </asp:DropDownList>
                </div>
            </div> 
            <div class="basic" style="height: 10px;">
            </div>              
            <div class="basic">
                <div style="float: left; width:60%">
                </div>
                <div style="float: left; width:28%">
                    <asp:ImageButton ID="SelectConditionButton" runat="server" ImageUrl="~/image/search.jpg" ToolTip="查询" OnClick="SelectConditionButton_Click" /> &nbsp;&nbsp; 
                    <asp:ImageButton ID="ResetConditionButton" runat="server" ImageUrl="~/image/reset.jpg"  ToolTip="重置" OnClick="ResetConditionButton_Click" />              
                </div>                  
            </div> 
        </asp:Panel>
    </div>
    <div class="basic" style="width: 100%; height: 10px;">
    </div>                
    <div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    </div>
    <div class="basic" style="width: 100%; height: 10px;">
    </div> 
    <div class="basic">
        <asp:Panel ID="DataDisplayPanel" runat="server" Width="100%">
            <asp:GridView ID="SchoolGridView" runat="server" AllowPaging="True" AllowSorting="True" PageSize="20"
                AutoGenerateColumns="False" Width="100%" DataKeyNames="ID" OnPageIndexChanging="SchoolGridView_PageIndexChanging"
                OnRowDataBound="SchoolGridView_RowDataBound" 
                OnSorting="SchoolGridView_Sorting" OnRowCommand="SchoolGridView_RowCommand" OnRowDeleting="SchoolGridView_RowDeleting">
                <PagerSettings FirstPageImageUrl="~/image/first.jpg" FirstPageText="首页" LastPageImageUrl="~/image/last.jpg"  LastPageText="末页" 
                    NextPageImageUrl="~/image/next.jpg" NextPageText="下一页" PageButtonCount="5" Position="Top" PreviousPageImageUrl="~/image/previous.jpg" PreviousPageText="上一页" 
                    Mode="NextPreviousFirstLast" />
                <Columns>
                    <asp:TemplateField HeaderText="序号"> 
                        <ItemTemplate> 
                            <%# Container.DataItemIndex + 1%> 
                        </ItemTemplate> 
                        <HeaderStyle Width="5%" ForeColor="blue" />
                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                    </asp:TemplateField>                      
                    <asp:BoundField DataField="UserCode" HeaderText="用户代码" SortExpression="UserCode">
                        <HeaderStyle Width="12%" />
                        <ItemStyle Width="12%" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="UserName" HeaderText="用户姓名" SortExpression="UserName">
                        <HeaderStyle Width="12%" />
                        <ItemStyle Width="12%" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="AgencyName" HeaderText="处室/教研组" SortExpression="AgencyName">
                        <HeaderStyle Width="15%" />
                        <ItemStyle Width="15%" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RoleCode" HeaderText="角色代码" SortExpression="RoleCode">
                        <HeaderStyle Width="10%" />
                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                    </asp:BoundField>   
                    <asp:BoundField DataField="RoleName" HeaderText="角色名称" SortExpression="RoleName">
                        <HeaderStyle Width="17%" />
                        <ItemStyle Width="17%" HorizontalAlign="Center" />
                    </asp:BoundField>                      
                    <asp:BoundField DataField="HandledName" HeaderText="批准人" SortExpression="HandledName">
                        <HeaderStyle Width="12%" />
                        <ItemStyle Width="12%" HorizontalAlign="Center" />
                    </asp:BoundField>                                   
                    <asp:TemplateField HeaderText="批准日期" SortExpression="HandledDate">
                        <ItemTemplate>
                            <asp:Label ID="HandledDate" runat="server" Text='<%# Bind("HandledDate", "{0:yyyy-MM-dd}" ) %>' />
                        </ItemTemplate>
                        <HeaderStyle Width="12%" />
                        <ItemStyle Width="12%" HorizontalAlign="Center" />
                    </asp:TemplateField>  
                    <asp:TemplateField>
                         <ItemTemplate>
                         <asp:ImageButton ID="DeleteBtn" runat="server" ImageUrl="~/image/deletebtn.jpg"  ToolTip="刪除" CommandName="Delete" OnClientClick="return confirm('您确认删除该条用户角色分配信息？')" />
                         </ItemTemplate>
                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                        <ControlStyle ForeColor="#C50000" />
                     </asp:TemplateField>                    
                </Columns>
                <SelectedRowStyle BackColor="#FFFFC0" />
                <PagerStyle HorizontalAlign="Right" />
                <HeaderStyle BackColor="#C0C0FF" />
                <EmptyDataRowStyle Font-Size="16px" ForeColor="Black" Height="200px" VerticalAlign="Middle" />
            </asp:GridView>
        </asp:Panel>
    </div>
    <div class="basic" style="width: 100%; height: 3px;">
    </div>     
    <div class="basic">
        <asp:Panel ID="Panel1" runat="server" Width="100%">
            <div>
                <div style="float: left; width: 6%;">
                </div>
                <div style="float: left; width: 24%">
                </div>
                <div style="float: left; width: 3%">
                </div>
                <div style="float: left; width: 22%">
                    <%--<asp:ImageButton ID="ExportExcelButton" runat="server"  ImageUrl="~/image/export.jpg" ToolTip="输出到Excel文档" OnClick="ExportExcelButton_Click"  />--%>
                </div>
                <div style="float: left; width: 24%">
                    <asp:Label ID="PageInfo" runat="server" Height="20px"></asp:Label>
                </div>
                <div style="float: left; width: 6%">
                    <asp:Label ID="Label1" runat="server" Height="20px" Text="转到第"></asp:Label>
                </div>
                <div style="float: left; width: 4%">
                    <asp:TextBox ID="PageIndex" runat="server" MaxLength="3"  Width="20px" Height="13px"></asp:TextBox>
                </div>
                <div style="float: left; width: 3%">
                    <asp:Label ID="Label2" runat="server" Height="20px" Text="页"></asp:Label>
                </div>                                                
                <div style="float: left; width: 6%">
                    <asp:ImageButton ID="GoButton" runat="server" ImageUrl="~/image/go.jpg" ToolTip="转到某页" OnClick="GoButton_Click"  />
                </div>
            </div>
        </asp:Panel>
    </div>
    <div class="basic" style="width: 100%; height: 10px;">
    </div>      
</asp:Content>
