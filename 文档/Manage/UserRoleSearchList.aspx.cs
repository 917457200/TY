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
using System.IO;
using EastElite.SMS.Business;
using EastElite.SMS.Business.Data;
using EastElite.SMS.UC;

namespace EastElite.SMS.Manage
{
    public partial class UserRoleSearchList : System.Web.UI.Page
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

                lists = UserManager.GetRoleInfoNameList();
                foreach( string item in lists )
                    RoleNameList.Items.Add( item );

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

                ViewState["SortColumn"] = "UserCode";
                ViewState["OrderDirection"] = "ASC";
                SchoolGridView.EmptyDataText = "请先设置查询条件，然后点击【查询】按钮！";

                string condition = ConstructSelectCondition();
                BindSchoolGridView( condition );
            }
        }

        protected void SelectConditionButton_Click( object sender, EventArgs e )
        {
            string condition = ConstructSelectCondition();
            SchoolGridView.EmptyDataText = "现在该系统未分配用户角色信息！";
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
            if( e.Row.RowType == DataControlRowType.DataRow )
            {
                e.Row.Attributes.Add( "onmouseover", @"this.style.backgroundColor='#E6F5FA'" );
                e.Row.Attributes.Add( "onmouseout", @"this.style.backgroundColor='#FFFFFF'" );
            }

            if( e.Row.RowIndex != -1 )
            {
                int id = e.Row.RowIndex;
                ImageButton deleteBtn = (ImageButton) e.Row.FindControl( "DeleteBtn" );
                deleteBtn.CommandArgument = id.ToString();
            }
        }

        protected void SchoolGridView_PageIndexChanging( object sender, GridViewPageEventArgs e )
        {
            GridView gridview = (GridView) sender;
            gridview.PageIndex = e.NewPageIndex;

            string condition = ConstructSelectCondition();
            BindSchoolGridView( condition );
        }

        protected void SchoolGridView_RowCommand( object sender, GridViewCommandEventArgs e )
        {
            GridView gridview = (GridView) sender;
            int rowIndex;
            int.TryParse( e.CommandArgument.ToString(), out rowIndex );
            long id;
            long.TryParse( gridview.DataKeys[rowIndex]["ID"].ToString(), out id );

            switch( e.CommandName )
            {
                case "Delete":
                    if( Request.Cookies["AUserRights"].Value.Substring( 101, 1 ) != "7" && Request.Cookies["AUserRights"].Value.Substring( 101, 1 ) != "8" && Request.Cookies["AUserRights"].Value.Substring( 101, 1 ) != "9" )
                    {
                        ClientScript.RegisterStartupScript( GetType(), " ", @"<script language='javascript'> alert('您目前没有权限删除！') </script>" );
                        return;
                    }
                    else
                    {
                        UserManager.DeleteUserRoleInfoItem( id );

                        string condition = ConstructSelectCondition();
                        BindSchoolGridView( condition );
                    }
                    break;
                default:
                    break;
            }
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
            DataSet dataSet = UserManager.GetUserRoleInfoList( condition );
            DataView dataView = dataSet.Tables[0].DefaultView;
            string sortString = ViewState["SortColumn"].ToString() + " " + (string) ViewState["OrderDirection"].ToString();
            dataView.Sort = sortString;
            SchoolGridView.DataSource = dataSet.Tables[0];
            SchoolGridView.DataBind();

            PageInfo.Text = "共" + dataSet.Tables[0].Rows.Count.ToString() + "条记录  第" + ( SchoolGridView.PageIndex + 1 ).ToString() + "页/共" + SchoolGridView.PageCount.ToString() + "页";
        }

        private string ConstructSelectCondition()
        {
            string condition = string.Format( @"[UserType]=1 AND [RoleCode]!='1003' AND (SELECT SUBSTRING(AgencyCode,1,10) FROM dbo.StaffInfo WHERE Code=[UserCode])='{0}'", Request.Cookies["RootCode"].Value );
            if( SubAgencyCodeList.SelectedIndex != 0 )
            {
                if( string.IsNullOrEmpty( condition ) )
                    condition = string.Format( @"[AgencyName] LIKE N'{0}'", SubAgencyCodeList.Text );
                else
                    condition += ( " AND " + string.Format( @"[AgencyName] LIKE N'{0}'", SubAgencyCodeList.Text ) );
            }
            if( RoleNameList.SelectedIndex != 0 )
            {
                if( string.IsNullOrEmpty( condition ) )
                    condition = string.Format( @"[RoleCode]='{0}'", RoleNameList.Text.Substring( 0, 4 ) );
                else
                    condition += ( " AND " + string.Format( @"[RoleCode]='{0}'", RoleNameList.Text.Substring( 0, 4 ) ) );
            }

            return condition;
        }

        protected void ResetConditionButton_Click( object sender, EventArgs e )
        {
            SubAgencyCodeList.SelectedIndex = 0;
            RoleNameList.SelectedIndex = 0;
        }
    }
}
