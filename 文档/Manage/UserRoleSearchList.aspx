<%@ Page Language="C#" MasterPageFile="~/Master/Manage.Master" AutoEventWireup="true" CodeBehind="UserRoleSearchList.aspx.cs" Inherits="EastElite.SMS.Manage.UserRoleSearchList" Title="�û���ɫ��ѯ����" MaintainScrollPositionOnPostback="True" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>    

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="innertitle">
        �û���ɫ��ѯ����</div>
    <div class="basic">
        <asp:Panel ID="SelectConditionPanel" runat="server" Width="100%" BackColor="#E4EFF3">
            <div class="basic">
                <div class="itemname1">
                    ����/������</div>
                <div class="itemvalue1">
                    <asp:DropDownList ID="SubAgencyCodeList" runat="server" Width="153px">
                    </asp:DropDownList>   
                </div>            
                <div class="itemname1">
                    ��ɫ�б�</div>
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
                    <asp:ImageButton ID="SelectConditionButton" runat="server" ImageUrl="~/image/search.jpg" ToolTip="��ѯ" OnClick="SelectConditionButton_Click" /> &nbsp;&nbsp; 
                    <asp:ImageButton ID="ResetConditionButton" runat="server" ImageUrl="~/image/reset.jpg"  ToolTip="����" OnClick="ResetConditionButton_Click" />              
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
                <PagerSettings FirstPageImageUrl="~/image/first.jpg" FirstPageText="��ҳ" LastPageImageUrl="~/image/last.jpg"  LastPageText="ĩҳ" 
                    NextPageImageUrl="~/image/next.jpg" NextPageText="��һҳ" PageButtonCount="5" Position="Top" PreviousPageImageUrl="~/image/previous.jpg" PreviousPageText="��һҳ" 
                    Mode="NextPreviousFirstLast" />
                <Columns>
                    <asp:TemplateField HeaderText="���"> 
                        <ItemTemplate> 
                            <%# Container.DataItemIndex + 1%> 
                        </ItemTemplate> 
                        <HeaderStyle Width="5%" ForeColor="blue" />
                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                    </asp:TemplateField>                      
                    <asp:BoundField DataField="UserCode" HeaderText="�û�����" SortExpression="UserCode">
                        <HeaderStyle Width="12%" />
                        <ItemStyle Width="12%" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="UserName" HeaderText="�û�����" SortExpression="UserName">
                        <HeaderStyle Width="12%" />
                        <ItemStyle Width="12%" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="AgencyName" HeaderText="����/������" SortExpression="AgencyName">
                        <HeaderStyle Width="15%" />
                        <ItemStyle Width="15%" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RoleCode" HeaderText="��ɫ����" SortExpression="RoleCode">
                        <HeaderStyle Width="10%" />
                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                    </asp:BoundField>   
                    <asp:BoundField DataField="RoleName" HeaderText="��ɫ����" SortExpression="RoleName">
                        <HeaderStyle Width="17%" />
                        <ItemStyle Width="17%" HorizontalAlign="Center" />
                    </asp:BoundField>                      
                    <asp:BoundField DataField="HandledName" HeaderText="��׼��" SortExpression="HandledName">
                        <HeaderStyle Width="12%" />
                        <ItemStyle Width="12%" HorizontalAlign="Center" />
                    </asp:BoundField>                                   
                    <asp:TemplateField HeaderText="��׼����" SortExpression="HandledDate">
                        <ItemTemplate>
                            <asp:Label ID="HandledDate" runat="server" Text='<%# Bind("HandledDate", "{0:yyyy-MM-dd}" ) %>' />
                        </ItemTemplate>
                        <HeaderStyle Width="12%" />
                        <ItemStyle Width="12%" HorizontalAlign="Center" />
                    </asp:TemplateField>  
                    <asp:TemplateField>
                         <ItemTemplate>
                         <asp:ImageButton ID="DeleteBtn" runat="server" ImageUrl="~/image/deletebtn.jpg"  ToolTip="�h��" CommandName="Delete" OnClientClick="return confirm('��ȷ��ɾ�������û���ɫ������Ϣ��')" />
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
                    <%--<asp:ImageButton ID="ExportExcelButton" runat="server"  ImageUrl="~/image/export.jpg" ToolTip="�����Excel�ĵ�" OnClick="ExportExcelButton_Click"  />--%>
                </div>
                <div style="float: left; width: 24%">
                    <asp:Label ID="PageInfo" runat="server" Height="20px"></asp:Label>
                </div>
                <div style="float: left; width: 6%">
                    <asp:Label ID="Label1" runat="server" Height="20px" Text="ת����"></asp:Label>
                </div>
                <div style="float: left; width: 4%">
                    <asp:TextBox ID="PageIndex" runat="server" MaxLength="3"  Width="20px" Height="13px"></asp:TextBox>
                </div>
                <div style="float: left; width: 3%">
                    <asp:Label ID="Label2" runat="server" Height="20px" Text="ҳ"></asp:Label>
                </div>                                                
                <div style="float: left; width: 6%">
                    <asp:ImageButton ID="GoButton" runat="server" ImageUrl="~/image/go.jpg" ToolTip="ת��ĳҳ" OnClick="GoButton_Click"  />
                </div>
            </div>
        </asp:Panel>
    </div>
    <div class="basic" style="width: 100%; height: 10px;">
    </div>      
</asp:Content>
