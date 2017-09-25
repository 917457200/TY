<%@ Page Language="C#" MasterPageFile="~/Master/Manage.Master" AutoEventWireup="true" CodeBehind="UserRoleSetList.aspx.cs" Inherits="EastElite.SMS.Manage.UserRoleSetList" Title="�û���ɫ���䴰��" MaintainScrollPositionOnPostback="True" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="innertitle">
        �û���ɫ���䴰��</div>
    <div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
    </div>        
    <div class="basic">
        <asp:Panel ID="SelectConditionPanel" runat="server" Width="100%" BackColor="#E4EFF3">
            <div class="basic">
                <div class="itemname1">
                    ����/������<span class="star">*</span></div>
                <div class="itemvalue1">
                    <asp:DropDownList ID="SubAgencyCodeList" runat="server" Width="153px" AutoPostBack="True" OnSelectedIndexChanged="SubAgencyCodeList_SelectedIndexChanged" >
                    </asp:DropDownList>   
                    <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ControlToValidate="SubAgencyCodeList"   
                      InitialValue=""  ErrorMessage="����/���������ѡ��" Display="None"></asp:RequiredFieldValidator>                         
                </div>            
                <div class="itemname1">
                    ��ʦ����<span class="star">*</span></div>
                <div class="itemvalue1">
                    <asp:DropDownList ID="StaffCodeList" runat="server" Width="153px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" ControlToValidate="StaffCodeList"   
                      InitialValue=""  ErrorMessage="��ʦ��������ѡ��" Display="None"></asp:RequiredFieldValidator>                     
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
    <div class="basic">
        <asp:Panel ID="DataDisplayPanel" runat="server" Width="100%">
            <asp:GridView ID="SchoolGridView" runat="server" AllowPaging="True" AllowSorting="True" PageSize="20"
                AutoGenerateColumns="False" Width="100%" DataKeyNames="Code" OnPageIndexChanging="SchoolGridView_PageIndexChanging"
                OnRowCommand="SchoolGridView_RowCommand" OnRowDataBound="SchoolGridView_RowDataBound"
                OnSorting="SchoolGridView_Sorting" OnRowDeleting="SchoolGridView_RowDeleting" OnRowEditing="SchoolGridView_RowEditing">
                <PagerSettings FirstPageImageUrl="~/image/first.jpg" FirstPageText="��ҳ" LastPageImageUrl="~/image/last.jpg"  LastPageText="ĩҳ" 
                    NextPageImageUrl="~/image/next.jpg" NextPageText="��һҳ" PageButtonCount="5" Position="Top" PreviousPageImageUrl="~/image/previous.jpg" PreviousPageText="��һҳ" 
                    Mode="NextPreviousFirstLast" />
                <Columns>
                    <asp:TemplateField HeaderText="���"> 
                        <ItemTemplate> 
                            <%# Container.DataItemIndex + 1%> 
                        </ItemTemplate> 
                        <HeaderStyle Width="6%" ForeColor="blue" />
                        <ItemStyle Width="6%" HorizontalAlign="Center" />
                    </asp:TemplateField>                      
                    <asp:BoundField DataField="Code" HeaderText="��ɫ����" SortExpression="Code">
                        <HeaderStyle Width="14%" />
                        <ItemStyle Width="14%" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Name" HeaderText="��ɫ����" SortExpression="Name">
                        <HeaderStyle Width="16%" />
                        <ItemStyle Width="16%" HorizontalAlign="Center" />
                    </asp:BoundField> 
                    <asp:BoundField DataField="Note" HeaderText="��ע" SortExpression="Note">
                        <HeaderStyle Width="38%" />
                        <ItemStyle Width="38%" HorizontalAlign="Center" />
                    </asp:BoundField>   
                    <asp:BoundField DataField="StatusText" HeaderText="����״̬" SortExpression="StatusText">
                        <HeaderStyle Width="14%" />
                        <ItemStyle Width="14%" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField>
                         <ItemTemplate>
                         <asp:ImageButton ID="AgreeBtn" runat="server" ImageUrl="~/image/agreebtn.jpg"  ToolTip="�����ɫ����" CommandName="Agree"  OnClientClick="return confirm('��ȷ������ý�ɫ���ܣ�')" />
                         </ItemTemplate>
                        <ItemStyle Width="6%" HorizontalAlign="Center" />
                        <ControlStyle ForeColor="#C50000" />
                     </asp:TemplateField>                                           
                    <asp:TemplateField>
                         <ItemTemplate>
                         <asp:ImageButton ID="DeclineBtn" runat="server" ImageUrl="~/image/declinebtn.jpg"  ToolTip="ȡ�������ɫ����" CommandName="Decline" OnClientClick="return confirm('��ȷ��ȡ������ý�ɫ���ܣ�')" />
                         </ItemTemplate>
                        <ItemStyle Width="6%" HorizontalAlign="Center" />
                        <ControlStyle ForeColor="#C50000" />
                     </asp:TemplateField>                       
                </Columns>
                <SelectedRowStyle BackColor="#FFFFC0" />
                <PagerStyle HorizontalAlign="Right" />
                <HeaderStyle BackColor="#C0C0FF" />
                <EmptyDataRowStyle Font-Size="16px" ForeColor="Black" Height="200px"  VerticalAlign="Middle" />
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
