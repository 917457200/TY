<%@ Page Language="C#" MasterPageFile="~/Master/Manage.Master" AutoEventWireup="true" CodeBehind="RoleList.aspx.cs" Inherits="EastElite.SMS.Manage.RoleList" Title="角色目录查询列表" MaintainScrollPositionOnPostback="True" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="innertitle">
        角色目录查询列表</div>
    <div class="basic">
        <asp:Panel ID="SelectConditionPanel" runat="server" Width="100%" Height="30px" BackColor="#E4EFF3">
            <div>
                <div class="itemname1">
                    角色类型</div>
                <div class="itemvalue1">
                    <asp:DropDownList ID="RoleTypeList" runat="server" Width="153px">
                    </asp:DropDownList>
                </div>
                <div class="itemname1">
                    </div>
                <div class="itemname1">
                </div>
                <div class="itemvalue1">
                    <asp:ImageButton ID="SelectConditionButton" runat="server" ImageUrl="~/image/search.jpg" ToolTip="查询" OnClick="SelectConditionButton_Click" />&nbsp;&nbsp;
                    <asp:ImageButton ID="AddRoleButton" runat="server" ImageUrl="~/image/addrolebutton.jpg" ToolTip="添加角色" OnClick="AddRoleButton_Click" />
                </div>                  
            </div>
        </asp:Panel>
    </div>
    <div class="basic" style="width: 100%; height: 10px;">
    </div>     
    <div class="basic">
        <asp:Panel ID="DataDisplayPanel" runat="server" Width="100%">
            <asp:GridView ID="SchoolGridView" runat="server" AllowPaging="True" AllowSorting="True" PageSize="20"
                AutoGenerateColumns="False" Width="100%" DataKeyNames="Code" OnPageIndexChanging="SchoolGridView_PageIndexChanging"
                OnRowDataBound="SchoolGridView_RowDataBound" OnSorting="SchoolGridView_Sorting" OnRowCommand="SchoolGridView_RowCommand" 
                OnRowCancelingEdit="SchoolGridView_RowCancelingEdit" OnRowEditing="SchoolGridView_RowEditing" OnRowUpdating="SchoolGridView_RowUpdating" OnRowDeleting="SchoolGridView_RowDeleting" OnRowCreated="SchoolGridView_RowCreated">
                <PagerSettings FirstPageImageUrl="~/image/first.jpg" FirstPageText="首页" LastPageImageUrl="~/image/last.jpg"  LastPageText="末页" 
                    NextPageImageUrl="~/image/next.jpg" NextPageText="下一页" PageButtonCount="5" Position="Top" PreviousPageImageUrl="~/image/previous.jpg" PreviousPageText="上一页" 
                    Mode="NextPreviousFirstLast" />
                <FooterStyle BackColor="#C0C0FF" />
                <Columns>
                    <asp:TemplateField HeaderText="序号"> 
                        <ItemTemplate> 
                            <%# Container.DataItemIndex + 1%> 
                        </ItemTemplate> 
                        <EditItemTemplate>
                            <asp:Label ID="IDSerial" runat="server" Text='<%# Container.DataItemIndex + 1 %>'  />
                        </EditItemTemplate>                         
                        <HeaderStyle Width="6%" ForeColor="blue" />
                        <ItemStyle Width="6%" HorizontalAlign="Center" />
                    </asp:TemplateField>                     
                   <asp:TemplateField HeaderText="角色代码"> 
                        <ItemTemplate> 
                            <asp:Label ID="Code" runat="server" Text='<%# Bind("Code") %>'  />
                        </ItemTemplate> 
                        <EditItemTemplate>
                            <asp:Label ID="Code" runat="server" Text='<%# Bind("Code") %>'  />
                        </EditItemTemplate>                         
                        <HeaderStyle Width="15%" ForeColor="blue" />
                        <ItemStyle Width="15%" HorizontalAlign="Center" />
                    </asp:TemplateField> 
                    <asp:BoundField DataField="Name" HeaderText="角色名称" SortExpression="Name" >
                        <HeaderStyle Width="25%" />
                        <ItemStyle Width="25%" HorizontalAlign="Center" />
                    </asp:BoundField>                                                       
                    <asp:BoundField DataField="Note" HeaderText="备注" SortExpression="Note" >
                        <HeaderStyle Width="40%" />
                        <ItemStyle Width="40%" HorizontalAlign="Center" />
                    </asp:BoundField>            
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:ImageButton ID="UpdateBtn" runat="server" ImageUrl="~/image/updatebtn.jpg"  ToolTip="更新" CommandName="Update" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:ImageButton ID="CancelBtn" runat="server" ImageUrl="~/image/cancelbtn.jpg"  ToolTip="取消" CommandName="Cancel" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="EditBtn" runat="server" ImageUrl="~/image/editbtn.jpg"  ToolTip="编辑" CommandName="Edit" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:ImageButton ID="DeleteBtn" runat="server" ImageUrl="~/image/deletebtn.jpg"  ToolTip="刪除" CommandName="Delete" OnClientClick="return confirm('您确认删除该条模块角色信息吗？')" />
                        </ItemTemplate>
                        <ItemStyle Width="14%" HorizontalAlign="Center" />
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
        <asp:Panel ID="OperationPanel" runat="server" Width="100%">
            <div>
                <div style="float: left; width: 6%;">
                </div>
                <div style="float: left; width: 6%">
                </div>
                <div style="float: left; width: 18%">
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