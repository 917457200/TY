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
using EastElite.SMS.Business.IO;
using EastElite.SMS.UC;

namespace EastElite.SMS.Manage
{
    public partial class RoleDetail : SubmitOncePage
    {
        protected void Page_Load( object sender, EventArgs e )
        {
            if( !IsPostBack )
            {
                ArrayList lists;
                lists = DictManager.GetDictItems( "RoleTypeID" );
                foreach( string item in lists )
                    RoleTypeList.Items.Add( item );
            }
        }

        protected void InsertButton_Click( object sender, EventArgs e )
        {
            RoleInfo item;

            if( IsRefreshed )
            {
                ClientScript.RegisterStartupScript( GetType(), " ", "<script> window.location.href=window.location.href </script>" );
                return;
            }

            if( IsValid )
            {
                item = new RoleInfo();

                string partCode1 = "0", partCode2, newCode;
                //if( RoleTypeList.SelectedIndex < 10 )
                //    partCode1 += RoleTypeList.SelectedIndex.ToString();
                //else
                partCode1 = RoleTypeList.SelectedIndex.ToString();
                int count = UserManager.GetRoleInfoCount( partCode1 );
                do
                {
                    count++;
                    if( count < 10 )
                        partCode2 = "00" + count.ToString();
                    else if( count < 100 )
                        partCode2 = "0" + count.ToString();
                    else
                        partCode2 = count.ToString().Substring( count.ToString().Length - 3, 3 );

                    newCode = partCode1 + partCode2;
                } while( UserManager.IsContainRoleInfoCode( newCode ) );

                if( UserManager.IsContainRoleInfoName( Name1.Text ) )
                {
                    ClientScript.RegisterStartupScript( GetType(), " ", @"<script language='javascript'> alert('该角色已经存在，请不要重复添加！') </script>" );
                    return;
                }

                item.code = newCode;
                item.name = Name1.Text;
                item.note = Note1.Text;
                item.handledID = Request.Cookies["AUserCode"].Value;
                item.handledDate = DateTime.Now;

                UserManager.InsertRoleInfoItem( item );

                Response.Write( @"<script>  var element=window.opener.document.all('ctl00_ctl00_ContentPlaceHolder1_ContentPlaceHolder2_SelectConditionButton'); if( element==null) element=window.opener.document.all('ContentPlaceHolder1_ContentPlaceHolder2_SelectConditionButton'); element.click(); window.close();</script>" );
            }
        }

        protected void CancelButton_Click( object sender, EventArgs e )
        {
            Response.Write( "<script>window.opener=null;window.close();</script>" );
        }
    }
}
