using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL.Log
{
    public static  class UserLog
    {
        //日志添加
        public static int AddUserLog( string LogName, string LogNote )
        {
            BLL.Cookie GetCookie = new BLL.Cookie();
            int reset = 0;
            using( NETDISKDBEntities Db = new NETDISKDBEntities() )
            {
                BLL.UserLog log = new BLL.UserLog();
                log.LogCreatTime = DateTime.Now;
                log.LogName = LogName;
                log.LogNote = LogNote;
                log.UserName = GetCookie.GetUserCookie().userName;
                log.State =true;
                Db.UserLog.Add( log );
                reset = Db.SaveChanges();
            }
            return reset;
        }
    
    }
}
