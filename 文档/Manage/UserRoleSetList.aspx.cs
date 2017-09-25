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
using EastElite.SMS.UC;

namespace EastElite.SMS.Manage
{
    public partial class UserRoleSetList : System.Web.UI.Page
    {
        protected void Page_Load( object sender, EventArgs e )
        {
            if( !IsPostBack )
            {
                ArrayList lists;

                lists = AgencyManager.GetAgencyNameList( Request.Cookies["RootCode"].Value, "( [SubAgencyTypeID]=1 OR [SubAgencyTypeID]=2 OR [SubAgencyTypeID]=9 )" );
                if( lists.Count == 1 )
                {
                    SubAgencyCodeList.Items.Clear();
                    SubAgencyCodeList.Items.Add( "" );
                    SubAgencyCodeList.Enabled = false;
                }
                else
                {
                    SubAgencyCodeList.Items.Clear();
                    SubAgencyCodeList.Enabled = true;
                    foreach( string item in lists )
                        SubAgencyCodeList.Items.Add( item );
                }

                StaffCodeList.Enabled = false;

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
                BindSchoolGridView( "000000" );
            }
        }

        protected void SubAgencyCodeList_SelectedIndexChanged( object sender, EventArgs e )
        {
            if( SubAgencyCodeList.SelectedIndex != 0 )
            {
                string agencyCode;
                if( SubAgencyCodeList.SelectedIndex == 0 )
                    agencyCode = Request.Cookies["RootCode"].Value;
                else
                    agencyCode = AgencyManager.GetAgencyCodeByName( Request.Cookies["RootCode"].Value, SubAgencyCodeList.Text );

                ArrayList lists = StaffManager.GetStaffNameList( agencyCode );
                if( lists.Count == 1 )
                {
                    StaffCodeList.Items.Clear();
                    StaffCodeList.Items.Add( "" );
                    StaffCodeList.Enabled = false;
                }
                else
                {
                    StaffCodeList.Items.Clear();
                    StaffCodeList.Enabled = true;
                    foreach( string item in lists )
                        StaffCodeList.Items.Add( item );
                }
            }
            else
            {
                StaffCodeList.Items.Clear();
                StaffCodeList.Items.Add( "" );
                StaffCodeList.Enabled = false;
            }
        }

        protected void SelectConditionButton_Click( object sender, EventArgs e )
        {
            string condition = ConstructSelectCondition();
            SchoolGridView.EmptyDataText = "现在该系统未注册角色信息！";
            BindSchoolGridView( condition );
        }

        protected void SchoolGridView_PageIndexChanging( object sender, GridViewPageEventArgs e )
        {
            GridView gridview = (GridView) sender;
            gridview.PageIndex = e.NewPageIndex;

            string condition = ConstructSelectCondition();
            BindSchoolGridView( condition );
        }

        protected void SchoolGridView_RowDataBound( object sender, GridViewRowEventArgs e )
        {
            if( e.Row.RowType == DataControlRowType.DataRow )
            {
                e.Row.Attributes.Add( "onmouseover", @"this.style.backgroundColor='#E6F5FA'" );
                e.Row.Attributes.Add( "onmouseout", @"this.style.backgroundColor='#FFFFFF'" );
            }

            if( e.Row.RowIndex != -1 )
            {
                int id = e.Row.RowIndex;
                ImageButton agreeBtn = (ImageButton) e.Row.FindControl( "AgreeBtn" );
                agreeBtn.CommandArgument = id.ToString();
                ImageButton declineBtn = (ImageButton) e.Row.FindControl( "DeclineBtn" );
                declineBtn.CommandArgument = id.ToString();
            }
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

        protected void SchoolGridView_RowCommand( object sender, GridViewCommandEventArgs e )
        {
            GridView gridview = (GridView) sender;
            int rowIndex;
            string condition;
            int.TryParse( e.CommandArgument.ToString(), out rowIndex );
            GridViewRow currentRow = gridview.Rows[rowIndex];
            string code = gridview.DataKeys[rowIndex]["Code"].ToString();
            switch( e.CommandName )
            {
                case "Agree":
                    if( Request.Cookies["AUserRights"].Value.Substring( 101, 1 ) == "0" || Request.Cookies["AUserRights"].Value.Substring( 101, 1 ) == "1" || Request.Cookies["AUserRights"].Value.Substring( 101, 1 ) == "2" || Request.Cookies["AUserRights"].Value.Substring( 101, 1 ) == "3" )
                    {
                        ClientScript.RegisterStartupScript( GetType(), " ", @"<script language='javascript'> alert('您目前没有权限授予角色功能！') </script>" );
                        return;
                    }
                    else
                    {
                        if( currentRow.Cells[4].Text == "已授予" )
                        {
                            ClientScript.RegisterStartupScript( GetType(), " ", @"<script language='javascript'> alert('该角色已经授予！') </script>" );
                            return;
                        }

                        if( code == "1044" && ( Request.Cookies["AUserCode"].Value != "366888" && Request.Cookies["AUserCode"].Value != "366999" ) )
                        {
                            ClientScript.RegisterStartupScript( GetType(), " ", @"<script language='javascript'> alert('工资管理负责人角色需要特定授予！') </script>" );
                            return;
                        }

                        UserRoleInfo userRoleInfo = new UserRoleInfo();
                        userRoleInfo.roleCode = code;
                        userRoleInfo.userCode = ViewState["UserCd"].ToString();
                        userRoleInfo.userType = 1;
                        userRoleInfo.note = "";
                        userRoleInfo.handledID = Request.Cookies["AUserCode"].Value;
                        userRoleInfo.handledDate = DateTime.Now;

                        UserManager.InsertUserRoleInfoItem( userRoleInfo );

                        if( code == "1059" )
                            StaffManager.UpdateStaffUserClassID( ViewState["UserCd"].ToString(), 2 );

                        condition = ConstructSelectCondition();
                        BindSchoolGridView( condition );
                    }
                    break;
                case "Decline":
                    if( Request.Cookies["AUserRights"].Value.Substring( 101, 1 ) == "0" || Request.Cookies["AUserRights"].Value.Substring( 101, 1 ) == "1" || Request.Cookies["AUserRights"].Value.Substring( 101, 1 ) == "2" || Request.Cookies["AUserRights"].Value.Substring( 101, 1 ) == "3" )
                    {
                        ClientScript.RegisterStartupScript( GetType(), " ", @"<script language='javascript'> alert('您目前没有权限取消授予角色功能！') </script>" );
                        return;
                    }
                    else
                    {
                        if( currentRow.Cells[4].Text == "" || currentRow.Cells[4].Text == "&nbsp;" )
                        {
                            ClientScript.RegisterStartupScript( GetType(), " ", @"<script language='javascript'> alert('该角色未授予！') </script>" );
                            return;
                        }

                        UserManager.DeleteUserRoleInfoItem( ViewState["UserCd"].ToString(), code, 1 );

                        if( code == "1059" )
                            StaffManager.UpdateStaffUserClassID( ViewState["UserCd"].ToString(), 1 );

                        condition = ConstructSelectCondition();
                        BindSchoolGridView( condition );
                    }
                    break;
            }
        }

        protected void SchoolGridView_RowEditing( object sender, GridViewEditEventArgs e )
        {

        }

        protected void SchoolGridView_RowDeleting( object sender, GridViewDeleteEventArgs e )
        {

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
            DataSet dataSet = new DataSet();
            if( condition == "000000" ) 
            {
                DataTable table = new DataTable();
                DataColumn column;
                column = new DataColumn( "Code", Type.GetType( "System.String" ) );
                table.Columns.Add( column );
                column = new DataColumn( "Name", Type.GetType( "System.String" ) );
                table.Columns.Add( column );
                column = new DataColumn( "Note", Type.GetType( "System.String" ) );
                table.Columns.Add( column );
                column = new DataColumn( "StatusText", Type.GetType( "System.String" ) );
                table.Columns.Add( column );
                //DataRow row = table.NewRow();
                //table.Rows.Add( row );

                dataSet.Tables.Add( table );
            }
            else
                dataSet = UserManager.GetUserRoleInfoList( condition, 1 );

            DataView dataView = dataSet.Tables[0].DefaultView;
            string sortString = ViewState["SortColumn"].ToString() + " " + (string) ViewState["OrderDirection"].ToString();
            dataView.Sort = sortString;
            SchoolGridView.DataSource = dataSet.Tables[0];
            SchoolGridView.DataBind();

            PageInfo.Text = "共" + dataSet.Tables[0].Rows.Count.ToString() + "条记录  第" + ( SchoolGridView.PageIndex + 1 ).ToString() + "页/共" + SchoolGridView.PageCount.ToString() + "页";
        }

        private string ConstructSelectCondition()
        {
            string condition = "000000";
            if( StaffCodeList.SelectedIndex > 0 )
            {
                condition = StaffCodeList.Text.Substring( 0, 6 );
            }
            ViewState["UserCd"] = condition;

            return condition;
        }

        protected void ResetConditionButton_Click( object sender, EventArgs e )
        {
            SubAgencyCodeList.SelectedIndex = 0;
            StaffCodeList.Items.Clear();
            StaffCodeList.Items.Add( "" );
            StaffCodeList.Enabled = false;
        }
    }
}
