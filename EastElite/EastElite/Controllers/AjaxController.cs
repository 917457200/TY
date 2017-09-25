using BLL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Configuration;


namespace EastElite.Controllers
{
    public class AjaxController : Controller
    {
        BLL.Cookie GetCookie = new BLL.Cookie();
        BLL.WEB.PageList List = new BLL.WEB.PageList();
        BLL.Role.UserRoleInfo UserRole = new BLL.Role.UserRoleInfo();

        // GET: /Ajax/
        /// <summary>
        /// 首页系统展示 无分页
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetMeun( string d, string e )
        {

            GetCookie.ExistCookie();
            var link = UserRole.GetLink();
            if( link == null )
            {
                return Json( "0", JsonRequestBehavior.AllowGet );
            }
            else
            {
                return Json( link.ToList(), JsonRequestBehavior.AllowGet );
            }
        }
        /// <summary>
        /// 链接管理分页
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public ActionResult GetLink( int pageIndex, string strWhere )
        {
            GetCookie.ExistCookie();
            if( SqlToNull.CheckBadWord( strWhere ) )
            {
                strWhere = SqlToNull.Filter( strWhere );
            }
            int pageSize = 10;
            BLL.WEB.PageList Page = new BLL.WEB.PageList();

            int pageCount = ( Page.GetPostCount() + pageSize - 1 ) / pageSize;

            List<BLL.Link> model = Page.GetPostList<BLL.Link>( pageIndex, pageSize, strWhere, "Link", "Code" ).ToList();

            return Json( new { model, pageCount, pageIndex }, JsonRequestBehavior.AllowGet );
        }
        /// <summary>
        /// 日志分页
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult GetLog( int pageIndex )
        {
            GetCookie.ExistCookie();

            int pageSize = 10;
            BLL.WEB.PageList Page = new BLL.WEB.PageList();

            int pageCount = ( Page.GetUserLogCount() + pageSize - 1 ) / pageSize;

            List<BLL.UserLog> model = Page.GetPostList<BLL.UserLog>( pageIndex, pageSize, "", "UserLog", "Id" ).ToList();

            return Json( new { model, pageCount, pageIndex }, JsonRequestBehavior.AllowGet );
        }
        /// <summary>
        /// 获取当前人员的角色
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public ActionResult GetRole( string Code )
        {
            if( SqlToNull.CheckBadWord( Code ) )
            {
                Code = SqlToNull.Filter( Code );
            }
            int Count = 0;//总条数
            BLL.Cookie.TeUser U = GetCookie.GetUserCookie();
            string condition = string.Format( @"[UserType]=1 AND (SELECT SUBSTRING(AgencyCode,1,10) FROM dbo.StaffInfo WHERE Code=[UserCode])='{0}' and UserCode ='{1}' ", U.rootCode, Code );

            List<BLL.SMSModel.RoleInfo> model = List.GetPageList( 1, 1000, "", ref Count );
            List<BLL.SMSModel.UserRoleInfoList> UserRole = List.GetUserRoleList( 1, 1000, condition, ref Count );
            string UserRoleStr = "";
            for( int i = 0; i < UserRole.Count; i++ )
            {
                UserRoleStr += UserRole[i].RoleCode + ",";
            }
            UserRoleStr = UserRoleStr.Length > 0 ? UserRoleStr.Substring( 0, UserRoleStr.Length - 1 ) : "";

            return Json( new { model, UserRoleStr }, JsonRequestBehavior.AllowGet );

        }

        /// <summary>
        /// 链接排序
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="orderCode"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        public string OderOa( int Code, int orderCode, string order )
        {
            GetCookie.ExistCookie();
            BLL.NETDISKDBEntities Db = new BLL.NETDISKDBEntities();
            BLL.NETDISKDBEntities Db2 = new BLL.NETDISKDBEntities();
            int sort;
            BLL.Link L = ( from B in Db.Link
                           where B.Code == Code
                           select B ).ToList().FirstOrDefault();

            BLL.Link L2 = ( from B in Db.Link
                            where B.Code == orderCode
                            select B ).ToList().FirstOrDefault();

            sort = (int) L.sort;

            L.sort = L2.sort;
            L2.sort = sort;

            Db.Link.Attach( L );
            Db.Entry( L ).Property( x => x.sort ).IsModified = true;
            Db.SaveChanges();
            Db2.Link.Attach( L2 );
            Db2.Entry( L2 ).Property( x => x.sort ).IsModified = true;
            Db2.SaveChanges();
            return "Suc";

        }
        /// <summary>
        /// 获取系统目录
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public ActionResult GetOaMeun( string d, string e )
        {
            GetCookie.ExistCookie();
            using( BLL.NETDISKDBEntities Db = new BLL.NETDISKDBEntities() )
            {
                //数据库访问模式
                var Menu = from b in Db.Menu
                           orderby b.MenuCode
                           select b;
                return Json( Menu.ToList(), JsonRequestBehavior.AllowGet );
            }
        }
        /// <summary>
        ///获取分页
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public ActionResult GetRoleList( int pageIndex, string strWhere )
        {
            if( SqlToNull.CheckBadWord( strWhere ) )
            {
                strWhere = SqlToNull.Filter( strWhere );
            }
            int pageSize = 10;//每页条数
            int Count = 0;//总条数
            List<BLL.SMSModel.RoleInfo> model = List.GetPageList( pageIndex, pageSize, strWhere, ref Count );
            int pageCount = ( Count + pageSize - 1 ) / pageSize;//页码
            return Json( new { model, pageCount, pageIndex }, JsonRequestBehavior.AllowGet );
        }
        /// <summary>
        ///获取人员权限分页
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public ActionResult GetUserRoleList( int pageIndex, string strWhere )
        {
            if( SqlToNull.CheckBadWord( strWhere ) )
            {
                strWhere = SqlToNull.Filter( strWhere );
            }
            string SubAgencyCode = "", RoleName = "";
            if( strWhere != "" )
            {
                string[] str = strWhere.Split( '★' );
                if( strWhere.IndexOf( '★' ) > -1 )
                {
                    SubAgencyCode = str[0].ToString();
                    RoleName = str[1].ToString();
                }
                else
                {
                    SubAgencyCode = str[0].ToString();
                }
            }

            BLL.Cookie.TeUser U = GetCookie.GetUserCookie();
            string condition = string.Format( @"[UserType]=1 AND (SELECT SUBSTRING(AgencyCode,1,10) FROM dbo.StaffInfo WHERE Code=[UserCode])='{0}'", U.rootCode );
            if( !string.IsNullOrEmpty( SubAgencyCode ) )
            {
                if( string.IsNullOrEmpty( condition ) )
                    condition = string.Format( @"[AgencyName] LIKE N'{0}'", SubAgencyCode );
                else
                    condition += ( " AND " + string.Format( @"[AgencyName] LIKE N'{0}'", SubAgencyCode ) );
            }
            if( !string.IsNullOrEmpty( RoleName ) )
            {
                if( string.IsNullOrEmpty( condition ) )
                    condition = string.Format( @"[RoleCode]='{0}'", RoleName.Substring( 0, 4 ) );
                else
                    condition += ( " AND " + string.Format( @"[RoleCode]='{0}'", RoleName.Substring( 0, 4 ) ) );
            }

            int pageSize = 10;//每页条数
            int Count = 0;//总条数
            List<BLL.SMSModel.UserRoleInfoList> model = List.GetUserRoleList( pageIndex, pageSize, condition, ref Count );
            int pageCount = ( Count + pageSize - 1 ) / pageSize;//页码
            return Json( new { model, pageCount, pageIndex }, JsonRequestBehavior.AllowGet );
        }
        /// <summary>
        ///获取人员分页
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public ActionResult GetUserList( int pageIndex, string strWhere )
        {
            if( SqlToNull.CheckBadWord( strWhere ) )
            {
                strWhere = SqlToNull.Filter( strWhere );
            }
            string SubAgencyCode = "", UserName = "";
            if( strWhere != "" )
            {
                string[] str = strWhere.Split( '★' );
                if( strWhere.IndexOf( '★' ) > -1 )
                {
                    SubAgencyCode = str[0].ToString();
                    UserName = str[1].ToString();
                }
                else
                {
                    SubAgencyCode = str[0].ToString();
                }
            }

            BLL.Cookie.TeUser U = GetCookie.GetUserCookie();
            string condition = string.Format( @"[UserType]=1  AND (SELECT SUBSTRING(AgencyCode,1,10) FROM dbo.StaffInfo WHERE Code=[UserCode])='{0}'", U.rootCode );
            if( !string.IsNullOrEmpty( SubAgencyCode ) )
            {
                if( string.IsNullOrEmpty( condition ) )
                    condition = string.Format( @"[AgencyName] LIKE N'{0}'", SubAgencyCode );
                else
                    condition += ( " AND " + string.Format( @"[AgencyName] LIKE N'{0}'", SubAgencyCode ) );
            }
            if( !string.IsNullOrEmpty( UserName ) )
            {
                if( string.IsNullOrEmpty( condition ) )
                    condition = string.Format( @"[UserName] like '%{0}%'", UserName );
                else
                    condition += ( " AND " + string.Format( @"[UserName] like '%{0}%'", UserName ) );
            }

            int pageSize = 10;//每页条数
            int Count = 0;//总条数
            List<BLL.SMSModel.UserInfoList> model = List.GetUserList( pageIndex, pageSize, condition, ref Count );
            int pageCount = ( Count + pageSize - 1 ) / pageSize;//页码
            return Json( new { model, pageCount, pageIndex }, JsonRequestBehavior.AllowGet );
        }
        //电子班牌
        public string Url = ConfigurationManager.AppSettings["DianUrl"].ToString();
        public string  GetGradeCode( string userCode, string rootCode )
        {
            using( var client = new HttpClient() )
            {
                client.MaxResponseContentBufferSize = 256000;
                client.DefaultRequestHeaders.Add( "user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36" );

                string url =Url+ "01-05";

                List<KeyValuePair<String, String>> paramList = new List<KeyValuePair<String, String>>();
                paramList.Add( new KeyValuePair<string, string>( "userCode", userCode ) );
                paramList.Add( new KeyValuePair<string, string>( "rootCode", rootCode ) );

                HttpResponseMessage response = client.PostAsync( new Uri( url ), new FormUrlEncodedContent( paramList ) ).Result;
                string result = response.Content.ReadAsStringAsync().Result;

                return result;
            }
        }

        public string GetClassCode( string userCode, string rootCode, string gradeCode )
        {
            using( var client = new HttpClient() )
            {
                client.MaxResponseContentBufferSize = 256000;
                client.DefaultRequestHeaders.Add( "user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36" );

                string url = Url + "01-26";

                List<KeyValuePair<String, String>> paramList = new List<KeyValuePair<String, String>>();
                paramList.Add( new KeyValuePair<string, string>( "userCode", userCode ) );
                paramList.Add( new KeyValuePair<string, string>( "rootCode", rootCode ) );
                paramList.Add( new KeyValuePair<string, string>( "gradeCode", gradeCode ) );


                HttpResponseMessage response = client.PostAsync( new Uri( url ), new FormUrlEncodedContent( paramList ) ).Result;
                string result = response.Content.ReadAsStringAsync().Result;

                return result;
            }
        }

        public string GetElectronicData( string ClassCode, string pageIndex, string gradeCode, string rootCode )
        {
           
            using( var client = new HttpClient() )
            {
                client.MaxResponseContentBufferSize = 256000;
                client.DefaultRequestHeaders.Add( "user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36" );

                string url = Url + "01-02";

                List<KeyValuePair<String, String>> paramList = new List<KeyValuePair<String, String>>();
                paramList.Add( new KeyValuePair<string, string>( "ClassCode", ClassCode ) );
                paramList.Add( new KeyValuePair<string, string>( "HeartBeatStatus", "" ) );
                paramList.Add( new KeyValuePair<string, string>( "InstallerHeartBeatStatus", "" ) );
                paramList.Add( new KeyValuePair<string, string>( "InstallerVersion", "" ) );
                paramList.Add( new KeyValuePair<string, string>( "JPushIDStatus", "" ) );
                paramList.Add( new KeyValuePair<string, string>( "StartProgramSwitchStatus ", "" ) );
                paramList.Add( new KeyValuePair<string, string>( "classTypeID", "" ) );
                paramList.Add( new KeyValuePair<string, string>( "currentPage", pageIndex ) );
                paramList.Add( new KeyValuePair<string, string>( "gradeCode", gradeCode ) );
                paramList.Add( new KeyValuePair<string, string>( "order", "SUBSTRING(classcode,1,10) asc,SUBSTRING([ClassCode],11,2) DESC,SUBSTRING([ClassCode],13,4) DESC,SUBSTRING([ClassCode],17,2) asc" ) );
                paramList.Add( new KeyValuePair<string, string>( "pageSize", "10" ) );
                paramList.Add( new KeyValuePair<string, string>( "rootCode", rootCode ) );
                paramList.Add( new KeyValuePair<string, string>( "sourceType", "1" ) );
                paramList.Add( new KeyValuePair<string, string>( "subjectTypeID", "" ) );
                paramList.Add( new KeyValuePair<string, string>( "version", "" ) );

                HttpResponseMessage response = client.PostAsync( new Uri( url ), new FormUrlEncodedContent( paramList ) ).Result;
                string result = response.Content.ReadAsStringAsync().Result;

                return result;
            }
        }
    }
}