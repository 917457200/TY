using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using Web.Core;


namespace EastElite.Controllers
{
    public class HomeController : Controller
    {
        BLL.Cookie GetCookie = new BLL.Cookie();
        BLL.digitalCampus digital = new BLL.digitalCampus();
        //
        // GET: /Home/
        [LoginNeedsFilter( IsCheck = false )]
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Login2()
        {
            return View();
        }
        //登陆
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LoginNeedsFilter( IsCheck = false )]
        public ActionResult ToLogin()
        {
            //登录接口加载
            EastEliteICMSWS.EastEliteICMSWSSoapClient Service = new EastEliteICMSWS.EastEliteICMSWSSoapClient();
            //登录错误信息
            string err = "";

            StringBuilder script = new StringBuilder();
            BLL.Cookie.TeUser U = new BLL.Cookie.TeUser();

            //加载数据
            string UserCode = Request.Form["inputUserCode"].ToString();
            string Password = Request.Form["inputPassword"].ToString();
            byte userType = byte.Parse( Request.Form["inputUserType"].ToString() );
            //验证
            if( string.IsNullOrEmpty( UserCode ) )
            {
                err = "姓名或代码不能为空！";
                script.Append( String.Format( "<script>alert('{0}');location.href='{1}'</script>", err, Url.Action( "Login" ) ) );
                return Content( script.ToString(), "Text/html" );
            }
            if( string.IsNullOrEmpty( Password ) )
            {
                err = "密码不能为空！";
                script.Append( String.Format( "<script>alert('{0}');location.href='{1}'</script>", err, Url.Action( "Login" ) ) );
                return Content( script.ToString(), "Text/html" );
            }

            //Password = BLL.MD5.Lower32(Password);//用户密码加密
            string result = Service.CheckUserLoginDeviceItem( UserCode, userType, Password );//访问接口验证登录

            ///登录失败
            if( result.IndexOf( "FAIL" ) > -1 )
            {
                int Start = result.IndexOf( "FAIL" ) + 9;
                int End = result.IndexOf( "!" ) + 1;
                err = result.Substring( Start, End - Start );
                script.Append( String.Format( "<script>alert('{0}');location.href='{1}'</script>", err, Url.Action( "Login" ) ) );
            
                return Content( script.ToString(), "Text/html" );
            }
            else if( result.IndexOf( "SUCC" ) > -1 )//登录成功
            {
                U = GetCookie.GetUserNameForSerVice( result );
                U.userName = U.userName;
                GetCookie.SetCookie( "Dfbg_OAUser", Newtonsoft.Json.JsonConvert.SerializeObject( U ), 30 );
                FormsAuthentication.SetAuthCookie( UserCode, false );
                BLL.Log.UserLog.AddUserLog( "登录成功", U.userName + " 成功登录统一管理平台系统 " );
                return Redirect( "~/Home/Index" );
            }
            else
            {
                //非法登录
                script.Append( String.Format( "<script>alert('{0}');location.href='{1}'</script>", "非法登录", Url.Action( "Login" ) ) );
                return Content( script.ToString(), "Text/html" );
            }


        }
        public ActionResult Index()
        {
            BLL.Role.UserRoleInfo Role = new BLL.Role.UserRoleInfo();
            ViewBag.IsAdmin = Role.IsAdmin();
            BLL.Cookie.TeUser U = GetCookie.GetUserCookie();
            U.userName = U.userName;
            return View( U );
        }
        public ActionResult Index2()
        {
            //Cookie 验证
            GetCookie.ExistCookie();
            return View();
        }
        public ActionResult Redir( int Code )
        {
            //Cookie 验证
            BLL.Cookie.TeUser U = GetCookie.GetUserCookie();
            //获取数据
            using( BLL.NETDISKDBEntities Db = new BLL.NETDISKDBEntities() )
            {
                var link = from b in Db.Link
                           where b.Code == Code && b.State == 1
                           orderby b.sort
                           select b;
                //假数据模式
                //BLL.StringDb Db2 = new BLL.StringDb();
                //List<BLL.StringDb.Link> linkDb = Db2.getLink();
                //var link = from b in linkDb
                //           where b.Code == Code
                //           select b;

                foreach( var item in link )
                {
                    string Url = "";
                    switch( Code )
                    {
                        case 12:
                            Url = digital.getUrl( item.link, U.userCode, U.userType );
                            return Redirect( "Http://" + Url );
                        case 2:
                            Url = digital.GetDianUrl( item.link, U.userCode, U.userType );
                            return Redirect( "Http://" + Url );
                        case 5:
                            //Url = digital.GuangBo("user1", "111111");
                            return Redirect( "/VidoLogin/Login" );

                        //return Redirect("Http://" + Url);

                        default:
                            break;
                    }

                    //其余直接跳转Url
                    if( item.Code == Code )
                    {
                        return Redirect( "Http://" + item.link );
                    }
                }
            }
            return Redirect( "~/Home/Index" );
        }
        public ActionResult OutLogin()
        {
            //Cookie 验证
            GetCookie.DelCookeis( "Dfbg_OAUser" );
            return View( "Login" );
        }

    }
}