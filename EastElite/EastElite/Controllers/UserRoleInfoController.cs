using EastElite.SMS.Business.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EastElite.Controllers
{
    public class UserRoleInfoController : Controller
    {
        BLL.Cookie GetCookie = new BLL.Cookie();
        BLL.Role.UserRoleInfo UserRoleInfo = new BLL.Role.UserRoleInfo();

        public ActionResult UserRoleList()
        {
            BLL.Cookie.TeUser U = GetCookie.GetUserCookie();

            ViewBag.Agencylists = AgencyManager.GetAgencyNameList( U.rootCode, "( [SubAgencyTypeID]=1 OR [SubAgencyTypeID]=2 OR [SubAgencyTypeID]=9 )" );

            return View();
        }
        // GET: /UserRoleInfo/
        public ActionResult UserRoleSeachList()
        {
            BLL.Cookie.TeUser U = GetCookie.GetUserCookie();
            ViewBag.Agencylists = AgencyManager.GetAgencyNameList( U.rootCode, "( [SubAgencyTypeID]=1 OR [SubAgencyTypeID]=2 OR [SubAgencyTypeID]=9 )" );
            ViewBag.Rolelists = UserManager.GetRoleInfoNameList();

            return View();
        }

        public void AddRoleList( string UserCode, string RoleCode, byte UserType, string UserName )
        {
            UserRoleInfo.AddRoleList( UserCode, RoleCode, UserType, UserName );
        }
        public void Del( int Code, string RoleName,string userName )
        {
            try
            {
                BLL.Log.UserLog.AddUserLog( "删除授权成功", GetCookie.GetUserCookie().userName + " 成功删除授权给 " + userName + " 的 " + RoleName + "角色 " );
                UserManager.DeleteUserRoleInfoItem( Code );
            }
            catch( Exception ex)
            {
                BLL.Log.UserLog.AddUserLog( "删除授权失败",ex.Message );
                throw;
            }
           
        }

    }
}