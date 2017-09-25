using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EastElite.SMS.Business;
using EastElite.SMS.Business.Data;

namespace EastElite.SMS.Manage
{
    public partial class RoleList : System.Web.UI.Page
    {
        protected void Page_Load( object sender, EventArgs e )
        {
            if( !IsPostBack )
            {
                ArrayList lists;
                lists = DictManager.GetDictItems( "RoleTypeID" );
                foreach( string item in lists )
                    RoleTypeList.Items.Add( item );

                TreeView treeView = (TreeView) Page.Form.FindControl( "ContentPlaceHolder1" ).FindControl( "SchoolTreeView" );
                TreeNode treeNode = treeView.FindNode( "20" );
                if( treeNode != null )
                {
                    if( treeNode.Depth == 0 )
                    {
                        treeNode.Expand();
                    }
                    else
                    {
                        treeNode.Parent.Expand();
                    }
                }
                else
                {
                    treeView.CollapseAll();
                }

                ViewState["SortColumn"] = "Code";
                ViewState["OrderDirection"] = "ASC";
                SchoolGridView.EmptyDataText = "请先设置查询条件，然后点击【查询】按钮！";
                //BindSchoolGridView( "[Code]='0000'" );
                string condition = ConstructSelectCondition();
                BindSchoolGridView( condition );
            }

        }

        protected void SelectConditionButton_Click( object sender, EventArgs e )
        {
            string condition = ConstructSelectCondition();
            SchoolGridView.EmptyDataText = "现在该系统没有添加模块角色！";
            BindSchoolGridView( condition );
        }

        protected void AddRoleButton_Click( object sender, EventArgs e )
        {
            string function = @"<script language=""javascript""> 
                                                    window.open( 'RoleDetail.aspx','_blank','width=500,height=320, top=100, left=100, toolbar=no, menubar=no, scrollbars=no, resizable=no, location=no, status=no');
                                                </script>";
            ClientScript.RegisterStartupScript( GetType(), " ", function );
        }

        protected void SchoolGridView_PageIndexChanging( object sender, GridViewPageEventArgs e )
        {
            GridView gridview = (GridView) sender;
            gridview.PageIndex = e.NewPageIndex;

            string condition = ConstructSelectCondition();
            BindSchoolGridView( condition );
        }

        protected void SchoolGridView_Sorting( object sender, GridViewSortEventArgs e )
        {
            if( ViewState["SortColumn"].ToString() == e.SortExpression )
            {
                if( ViewState["OrderDirection"].ToString().ToUpper() == "DESC" )
                    ViewState["OrderDirection"] = "ASC";
                else
                    ViewState["OrderDirection"] = "DESC";
            }
            else
            {
                ViewState["SortColumn"] = e.SortExpression;
            }

            string condition = ConstructSelectCondition();
            BindSchoolGridView( condition );
        }

        protected void SchoolGridView_RowDataBound( object sender, GridViewRowEventArgs e )
        {
            if( e.Row.RowType == DataControlRowType.Header )
            {
                if( e.Row.Cells.Count > 0 )
                {
                    e.Row.Cells[0].Width = 48;
                    e.Row.Cells[1].Width = 120;
                    e.Row.Cells[2].Width = 200;
                    e.Row.Cells[3].Width = 320;
                    e.Row.Cells[4].Width = 112;
                }
            }

            if( e.Row.RowType == DataControlRowType.DataRow )
            {
                e.Row.Attributes.Add( "onmouseover", @"this.style.backgroundColor='#E6F5FA'" );
                e.Row.Attributes.Add( "onmouseout", @"this.style.backgroundColor='#FFFFFF'" );

                if( e.Row.Cells.Count > 0 )
                {
                    e.Row.Cells[1].CssClass = "gridviewdataitem";
                    e.Row.Cells[2].CssClass = "gridviewdataitem";
                    e.Row.Cells[3].CssClass = "gridviewdataitem";
                }
            }

            if( ( e.Row.RowState & DataControlRowState.Edit ) != 0 )
            {
                Label label;
                TextBox textBox;
                //CheckBox chBox;
                label = (Label) e.Row.Cells[0].Controls[1];
                label.Width = 48;
                label = (Label) e.Row.Cells[1].Controls[1];  //角色代码
                label.Width = 120;
                textBox = (TextBox) e.Row.Cells[2].Controls[0];
                textBox.Attributes.Add( "maxlength", "50" );
                textBox.Attributes.Add( "class", "TextBox" );
                textBox.Width = 200;
                textBox = (TextBox) e.Row.Cells[3].Controls[0];
                textBox.Attributes.Add( "maxlength", "500" );
                textBox.Attributes.Add( "class", "TextBox" );
                textBox.Width = 320;
                e.Row.Cells[4].Width = 112;
            }

            if( e.Row.RowIndex != -1 )
            {
                int id = e.Row.RowIndex;
                if( ( e.Row.RowState & DataControlRowState.Edit ) != 0 )
                {
                    ImageButton updateBtn = (ImageButton) e.Row.FindControl( "UpdateBtn" );
                    updateBtn.CommandArgument = id.ToString();
                    ImageButton cancelBtn = (ImageButton) e.Row.FindControl( "CancelBtn" );
                    cancelBtn.CommandArgument = id.ToString();
                }
                else
                {
                    ImageButton editBtn = (ImageButton) e.Row.FindControl( "EditBtn" );
                    editBtn.CommandArgument = id.ToString();
                    ImageButton deleteBtn = (ImageButton) e.Row.FindControl( "DeleteBtn" );
                    deleteBtn.CommandArgument = id.ToString();
                }
            }
        }

        protected void SchoolGridView_RowCreated( object sender, GridViewRowEventArgs e )
        {

        }

        protected void SchoolGridView_RowCommand( object sender, GridViewCommandEventArgs e )
        {
        }

        protected void SchoolGridView_RowEditing( object sender, GridViewEditEventArgs e )
        {
            SchoolGridView.EditIndex = e.NewEditIndex;
            string condition = ConstructSelectCondition();
            BindSchoolGridView( condition );
        }

        protected void SchoolGridView_RowCancelingEdit( object sender, GridViewCancelEditEventArgs e )
        {
            SchoolGridView.EditIndex = -1;
            string condition = ConstructSelectCondition();
            BindSchoolGridView( condition );
        }

        protected void SchoolGridView_RowUpdating( object sender, GridViewUpdateEventArgs e )
        {
            string condition;
            RoleInfo item = new RoleInfo();
            item.code = SchoolGridView.DataKeys[e.RowIndex]["Code"].ToString();
            item.name = ( (TextBox) SchoolGridView.Rows[e.RowIndex].Cells[2].Controls[0] ).Text;
            item.note = ( (TextBox) SchoolGridView.Rows[e.RowIndex].Cells[3].Controls[0] ).Text;
            item.handledID = Request.Cookies["AUserCode"].Value;
            item.handledDate = DateTime.Now;

            UserManager.UpdateRoleInfoItem( item );

            SchoolGridView.EditIndex = -1;
            condition = ConstructSelectCondition();
            BindSchoolGridView( condition );
        }

        protected void SchoolGridView_RowDeleting( object sender, GridViewDeleteEventArgs e )
        {
            string code = SchoolGridView.DataKeys[e.RowIndex]["Code"].ToString();

            UserManager.DeleteRoleInfoItem( code );

            SchoolGridView.EditIndex = -1;
            string condition = ConstructSelectCondition();
            BindSchoolGridView( condition );
        }

        protected void GoButton_Click( object sender, EventArgs e )
        {
            int pageIndex = 0;

            int.TryParse( PageIndex.Text, out pageIndex );
            if( pageIndex > 0 && pageIndex <= SchoolGridView.PageCount )
            {
                SchoolGridView.PageIndex = pageIndex - 1;
                string condition = ConstructSelectCondition();
                BindSchoolGridView( condition );
            }
        }

        private void BindSchoolGridView( string condition )
        {
            DataSet dataSet = UserManager.GetRoleList( condition );
            DataView dataView = dataSet.Tables[0].DefaultView;
            string sortString = ViewState["SortColumn"].ToString() + " " + (string) ViewState["OrderDirection"].ToString();
            dataView.Sort = sortString;
            SchoolGridView.DataSource = dataSet.Tables[0];
            SchoolGridView.DataBind();

            PageInfo.Text = "共" + dataSet.Tables[0].Rows.Count.ToString() + "条记录  第" + ( SchoolGridView.PageIndex + 1 ).ToString() + "页/共" + SchoolGridView.PageCount.ToString() + "页";
        }

        private string ConstructSelectCondition()
        {
            string condition = null;
            string partCode = "0";
            if( RoleTypeList.SelectedIndex != 0 )
            {
                //if( RoleTypeList.SelectedIndex < 10 )
                //    partCode += RoleTypeList.SelectedIndex.ToString();
                //else
                partCode = RoleTypeList.SelectedIndex.ToString();

                condition = string.Format( @" SUBSTRING([Code],1,1)='{0}' ", partCode );
            }

            return condition;
        }
    }
}
