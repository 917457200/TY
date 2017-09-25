using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity.Infrastructure;
using EastElite.SMS.Business.Data;
using EastElite.SMS.Business;
using BLL;

namespace EastElite.Controllers
{
    public class RoleController : Controller
    {
        BLL.Cookie GetCookie = new BLL.Cookie();
        BLL.WEB.PageList Page = new BLL.WEB.PageList();
        BLL.Role.UserRoleInfo Role = new BLL.Role.UserRoleInfo();

        // 角色管理列表
        public ActionResult RoleList()
        {
            return View();
        }
        // 角色添加页面，
        public ActionResult Add()
        {
            List<BLL.Link> model = Page.GetPostList<BLL.Link>( 1, 1000, "", "Link", "Code" ).ToList();
            ViewBag.Link = model;
            return View();
        }
        // 角色添加方法
        [HttpPost]
        public ActionResult Add( BLL.SMSModel.RoleInfo model )
        {
            try
            {
                string partCode2, newCode;
                BLL.Cookie.TeUser U = GetCookie.GetUserCookie();
                ///系统验证
                string LinkCodes = Request.Form["Link"];
                if( string.IsNullOrEmpty( LinkCodes ) )
                {
                    return Content( @"<script language='javascript'> alert('请选择系统权限！');location.href='" + Url.Action( "Add" ) + "'; </script>", "Text/html" );
                }
                ///系统验证
                if( UserManager.IsContainRoleInfoName( model.Name ) )
                {
                    return Content( @"<script language='javascript'> alert('该角色已经存在，请不要重复添加！');location.href='" + Url.Action( "Add" ) + "'; </script>", "Text/html" );
                }
                ///Role加载
                RoleInfo item = new RoleInfo();
                int count = UserManager.GetRoleInfoCount( "2" );//自定义
                do
                {
                    count++;
                    if( count < 10 )
                        partCode2 = "00" + count.ToString();
                    else if( count < 100 )
                        partCode2 = "0" + count.ToString();
                    else
                        partCode2 = count.ToString().Substring( count.ToString().Length - 3, 3 );

                    newCode = "2" + partCode2;
                }
                while( UserManager.IsContainRoleInfoCode( newCode ) );


                item.code = newCode;
                item.handledID = U.userCode;
                item.handledDate = DateTime.Now;
                item.name = model.Name;
                item.note = model.Note == null ? "" : model.Note;
                //添加数据
                UserManager.InsertRoleInfoItem( item );
                //添加数据
                if( LinkCodes.ToString().IndexOf( "," ) > -1 )
                {
                    string[] LinkCode = LinkCodes.ToString().Split( ',' );
                    for( int i = 0; i < LinkCode.Length; i++ )
                    {
                        using( BLL.NETDISKDBEntities Db = new BLL.NETDISKDBEntities() )
                        {
                            BLL.OaRole R = new BLL.OaRole();
                            R.LinkCode = LinkCode[i].ToString();
                            R.RoleCode = newCode.ToString();
                            Db.OaRole.Add( R );
                            Db.SaveChanges();
                        }
                    }
                }
                else
                {

                    using( BLL.NETDISKDBEntities Db = new BLL.NETDISKDBEntities() )
                    {
                        BLL.OaRole R = new BLL.OaRole();
                        R.LinkCode = LinkCodes.ToString();
                        R.RoleCode = newCode.ToString();
                        Db.OaRole.Add( R );
                        Db.SaveChanges();
                    }
                }
                BLL.Log.UserLog.AddUserLog( "添加角色成功", GetCookie.GetUserCookie().userName + " 成功创建 " + model.Name + " 角色 " );

            }
            catch( Exception ex )
            {
                BLL.Log.UserLog.AddUserLog( "创建角色失败", ex.Message );
                throw;
            }
            return View( "RoleList" );
        }
        // 角色修改页面
        public ActionResult Edit( string Code )
        {
            BLL.SMSModel.RoleInfo model = new BLL.SMSModel.RoleInfo();
            RoleInfo item = UserManager.GetRoleInfoItem( Code );
            ViewBag.Link = Page.GetPostList<BLL.Link>( 1, 1000, "", "Link", "Code" ).ToList();
            string OaRole = Role.GetOaRoleByRoleCode( Code );
            ViewBag.OaRole = OaRole == null ? "" : OaRole;
            model.Name = item.name;
            model.Note = item.note;
            model.Code = item.code;
            TempData["Name"] = item.name;

            return View( model );
        }
        // 角色修改方法
        [HttpPost]
        public ActionResult Edit( BLL.SMSModel.RoleInfo model )
        {
            try
            {

                BLL.Cookie.TeUser U = GetCookie.GetUserCookie();
                RoleInfo item = new RoleInfo();
                ///系统验证

                if( string.IsNullOrEmpty( Request.Form["Link"] ) )
                {

                    return Content( @"<script language='javascript'> alert('请选择系统权限！');location.href='" + Url.Action( "Edit", new { Code = model.Code } ) + "'; </script>", "Text/html" );
                }
                if( UserManager.IsContainRoleInfoName( model.Name ) && model.Name != TempData["Name"].ToString() )
                {
                    return Content( @"<script language='javascript'> alert('该角色已经存在，请不要重复！');location.href='" + Url.Action( "Edit", new { Code = model.Code } ) + "'; </script>", "Text/html" );
                }
                item.code = model.Code;
                item.handledID = U.userCode;
                item.handledDate = DateTime.Now;
                item.name = model.Name;
                item.note = model.Note == null ? "" : model.Note;
                UserManager.UpdateRoleInfoItem( item );
                Role.delOaRoleByRoleCode( model.Code );

                string LinkCodes = Request.Form["Link"].ToString();
                //添加数据
                using( BLL.NETDISKDBEntities Db = new BLL.NETDISKDBEntities() )
                {
                    if( LinkCodes.IndexOf( "," ) > -1 )
                    {
                        string[] LinkCode = LinkCodes.Split( ',' );
                        for( int i = 0; i < LinkCode.Length; i++ )
                        {

                            BLL.OaRole R = new BLL.OaRole();
                            R.LinkCode = LinkCode[i].ToString();
                            R.RoleCode = model.Code.ToString();
                            Db.OaRole.Add( R );
                        }
                    }
                    else
                    {
                        BLL.OaRole R = new BLL.OaRole();
                        R.LinkCode = LinkCodes.ToString();
                        R.RoleCode = model.Code.ToString();
                        Db.OaRole.Add( R );
                    }
                    Db.SaveChanges();
                }
                BLL.Log.UserLog.AddUserLog( "添加修改成功", GetCookie.GetUserCookie().userName + " 成功修改 " + model.Name + " 角色 " );

            }
            catch( Exception ex )
            {
                BLL.Log.UserLog.AddUserLog( "修改角色失败", ex.Message );
                throw;
            }
            return View( "RoleList" );
        }
        public string Del( string code )
        {
            try
            {
                if( SqlToNull.CheckBadWord( code ) )
                {
                    code = SqlToNull.Filter( code );
                }
                if( UserManager.IsContainUserRoleInfoCode( GetCookie.GetUserCookie().userCode, code, byte.Parse( GetCookie.GetUserCookie().userType ) ) )
                {
                    return "该角色下有用户正在使用，删除失败！";
                }
                else
                {
                    Role.delOaRoleByRoleCode( code );
                    EastElite.SMS.Business.RoleInfo RoleIN = UserManager.GetRoleInfoItem( code );
                    UserManager.DeleteRoleInfoItem( code );
                    BLL.Log.UserLog.AddUserLog( "删除角色成功", GetCookie.GetUserCookie().userName + " 删除修改 " + RoleIN.name + " 角色 " );
                    return "删除成功";
                }
            }
            catch( Exception ex )
            {
                BLL.Log.UserLog.AddUserLog( "删除角色失败", ex.Message );
                throw;
            }
        }
    }
}