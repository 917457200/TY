using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EastElite.Controllers
{
    public class LinkController : Controller
    {
        BLL.Cookie GetCookie = new BLL.Cookie();

        //
        // GET: /Link/
        public ActionResult List()
        {
            GetCookie.ExistCookie();

            return View();
        }
        public ActionResult Add()
        {
            GetCookie.ExistCookie();
            return View();
        }
        [HttpPost]
        public ActionResult Add( BLL.Link L )
        {
            using( BLL.NETDISKDBEntities Db = new BLL.NETDISKDBEntities() )
            {
                try
                {
                    var image = Request.Files["FileUpload1"];
                    if( image != null && image.ContentLength > 0 )
                    {
                        string fileName = DateTime.Now.ToString( "yyyyMMdd" ) + "-" + Path.GetFileName( image.FileName );
                        string filePath = Path.Combine( Server.MapPath( "~/Content/Img" ), fileName );
                        image.SaveAs( filePath );
                        L.img = fileName;
                    }
                    L.State = 1;
                    Db.Link.Add( L );
                    int re = Db.SaveChanges();
                    if( re > 0 )
                    {
                        BLL.Log.UserLog.AddUserLog( "添加系统成功", GetCookie.GetUserCookie().userName + " 成功创建 " + L.oaName + " 系统 " );
                    }
                    else
                    {
                        BLL.Log.UserLog.AddUserLog( "创建系统失败", "数据插入失败！" );
                    }
                }
                catch( Exception ex )
                {
                    BLL.Log.UserLog.AddUserLog( "创建系统失败", ex.Message );
                    throw;
                }
                return View( "List" );
            }
        }
        public ActionResult Edit( int Code )
        {
            GetCookie.ExistCookie();
            using( BLL.NETDISKDBEntities Db = new BLL.NETDISKDBEntities() )
            {
                var Model = ( from B in Db.Link
                              where B.Code == Code
                              select B ).ToList().FirstOrDefault();

                return View( Model );
            }
        }
        [HttpPost]
        public ActionResult Edit( BLL.Link L )
        {
            GetCookie.ExistCookie();
            using( BLL.NETDISKDBEntities Db = new BLL.NETDISKDBEntities() )
            {
                try
                {
                    var image = Request.Files["FileUpload1"];
                    if( image != null && image.ContentLength > 0 )
                    {
                        string fileName = DateTime.Now.ToString( "yyyyMMdd" ) + "-" + Path.GetFileName( image.FileName );
                        string filePath = Path.Combine( Server.MapPath( "~/Content/Img" ), fileName );
                        image.SaveAs( filePath );
                        L.img = fileName;
                    }
                    DbEntityEntry<BLL.Link> entry = Db.Entry<BLL.Link>( L );
                    entry.State = System.Data.Entity.EntityState.Modified;
                    int re = Db.SaveChanges();
                    if( re > 0 )
                    {
                        BLL.Log.UserLog.AddUserLog( "修改系统成功", GetCookie.GetUserCookie().userName + " 成功修改 " + L.oaName + " 系统 " );
                    }
                    else
                    {
                        BLL.Log.UserLog.AddUserLog( "修改系统失败", "数据修改失败！" );
                    }

                }
                catch( Exception ex )
                {
                    BLL.Log.UserLog.AddUserLog( "修改系统失败", ex.Message );
                    throw;
                }
                return View( "List" );
            }
        }
        public ActionResult Del( int code )
        {
            using( BLL.NETDISKDBEntities Db = new BLL.NETDISKDBEntities() )
            {
                try
                {
                    BLL.Link L = new BLL.Link();
                    L.State = 0;
                    L.Code = code;
                    Db.Link.Attach( L );
                    Db.Entry( L ).Property( x => x.State ).IsModified = true;

                    string oaName = ( from B in Db.Link
                                      where B.Code == code
                                      select B.oaName ).ToList().FirstOrDefault();

                    int re = Db.SaveChanges();
                    if( re > 0 )
                    {
                        BLL.Log.UserLog.AddUserLog( "删除系统成功", GetCookie.GetUserCookie().userName + " 成功删除 " + oaName + " 系统 " );
                    }
                    else
                    {
                        BLL.Log.UserLog.AddUserLog( "删除系统失败", "数据删除失败！" );
                    }
                }
                catch( Exception ex)
                {
                    BLL.Log.UserLog.AddUserLog( "删除系统失败", ex.Message );
                    throw;
                }
                return View( "List" );
            }
        }
        //
        // GET: /Link/
        public ActionResult Pai()
        {
            return View();
        }
    }
}