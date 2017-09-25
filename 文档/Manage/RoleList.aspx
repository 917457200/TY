<%@ Page Language="C#" MasterPageFile="~/Master/Manage.Master" AutoEventWireup="true" CodeBehind="RoleList.aspx.cs" Inherits="EastElite.SMS.Manage.RoleList" Title="��ɫĿ¼��ѯ�б�" MaintainScrollPositionOnPostback="True" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="innertitle">
        ��ɫĿ¼��ѯ�б�</div>
    <div class="basic">
        <asp:Panel ID="SelectConditionPanel" runat="server" Width="100%" Height="30px" BackColor="#E4EFF3">
            <div>
                <div class="itemname1">
                    ��ɫ����</div>
                <div class="itemvalue1">
                    <asp:DropDownList ID="RoleTypeList" runat="server" Width="153px">
                    </asp:DropDownList>
                </div>
                <div class="itemname1">
                    </div>
                <div class="itemname1">
                </div>
                <div class="itemvalue1">
                    <asp:ImageButton ID="SelectConditionButton" runat="server" ImageUrl="~/image/search.jpg" ToolTip="��ѯ" OnClick="SelectConditionButton_Click" />&nbsp;&nbsp;
                    <asp:ImageButton ID="AddRoleButton" runat="server" ImageUrl="~/image/addrolebutton.jpg" ToolTip="��ӽ�ɫ" OnClick="AddRoleButton_Click" />
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
                <PagerSettings FirstPageImageUrl="~/image/first.jpg" FirstPageText="��ҳ" LastPageImageUrl="~/image/last.jpg"  LastPageText="ĩҳ" 
                    NextPageImageUrl="~/image/next.jpg" NextPageText="��һҳ" PageButtonCount="5" Position="Top" PreviousPageImageUrl="~/image/previous.jpg" PreviousPageText="��һҳ" 
                    Mode="NextPreviousFirstLast" />
                <FooterStyle BackColor="#C0C0FF" />
                <Columns>
                    <asp:TemplateField HeaderText="���"> 
                        <ItemTemplate> 
                            <%# Container.DataItemIndex + 1%> 
                        </ItemTemplate> 
                        <EditItemTemplate>
                            <asp:Label ID="IDSerial" runat="server" Text='<%# Container.DataItemIndex + 1 %>'  />
                        </EditItemTemplate>                         
                        <HeaderStyle Width="6%" ForeColor="blue" />
                        <ItemStyle Width="6%" HorizontalAlign="Center" />
                    </asp:TemplateField>                     
                   <asp:TemplateField HeaderText="��ɫ����"> 
                        <ItemTemplate> 
                            <asp:Label ID="Code" runat="server" Text='<%# Bind("Code") %>'  />
                        </ItemTemplate> 
                        <EditItemTemplate>
                            <asp:Label ID="Code" runat="server" Text='<%# Bind("Code") %>'  />
                        </EditItemTemplate>                         
                        <HeaderStyle Width="15%" ForeColor="blue" />
                        <ItemStyle Width="15%" HorizontalAlign="Center" />
                    </asp:TemplateField> 
                    <asp:BoundField DataField="Name" HeaderText="��ɫ����" SortExpression="Name" >
                        <HeaderStyle Width="25%" />
                        <ItemStyle Width="25%" HorizontalAlign="Center" />
                    </asp:BoundField>                                                       
                    <asp:BoundField DataField="Note" HeaderText="��ע" SortExpression="Note" >
                        <HeaderStyle Width="40%" />
                        <ItemStyle Width="40%" HorizontalAlign="Center" />
                    </asp:BoundField>            
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:ImageButton ID="UpdateBtn" runat="server" ImageUrl="~/image/updatebtn.jpg"  ToolTip="����" CommandName="Update" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:ImageButton ID="CancelBtn" runat="server" ImageUrl="~/image/cancelbtn.jpg"  ToolTip="ȡ��" CommandName="Cancel" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="EditBtn" runat="server" ImageUrl="~/image/editbtn.jpg"  ToolTip="�༭" CommandName="Edit" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:ImageButton ID="DeleteBtn" runat="server" ImageUrl="~/image/deletebtn.jpg"  ToolTip="�h��" CommandName="Delete" OnClientClick="return confirm('��ȷ��ɾ������ģ���ɫ��Ϣ��')" />
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