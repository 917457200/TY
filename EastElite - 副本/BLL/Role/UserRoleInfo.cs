using EastElite.SMS.Business.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Role
{
    public class UserRoleInfo
    {
        BLL.Cookie GetCookie = new BLL.Cookie();
        DataSetToList List = new DataSetToList();

        public bool IsAdmin()
        {

            UserRoleInfo item = new UserRoleInfo();
            BLL.Cookie.TeUser U = GetCookie.GetUserCookie();
            bool A = false;
            DataSet dt = UserManager.GetUserRoleInfoList( "UserCode ='" + U.userCode + "'" );
            for( int i = 0; i < dt.Tables[0].Rows.Count; i++ )
            {
                if( dt.Tables[0].Rows[i]["RoleCode"].ToString() == "1001" )
                {
                    A = true;
                }
            }
            return A;
        }
        public string GetOaRoleByRoleCode( string RoleCode )
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append( "select (stuff((select '★' + LinkCode + '★' from OaRole" );
            strSql.Append( string.Format( " where RoleCode = {0}", RoleCode ) );
            strSql.Append( " for xml path('')),1,1,'★')) as LinkCodes" );

            using( NETDISKDBEntities Db = new NETDISKDBEntities() )
            {
                string result1 = Db.Database.SqlQuery<string>( strSql.ToString() ).FirstOrDefault();

                return result1;
            }
        }
        public void delOaRoleByRoleCode( string RoleCode )
        {
            using( NETDISKDBEntities Db = new NETDISKDBEntities() )
            {
                IQueryable<OaRole> list = Db.OaRole.Where( p => p.RoleCode == RoleCode );
                foreach( var item in list )
                {
                    Db.OaRole.Remove( item );
                }
                Db.SaveChanges();
            }
        }

        public void AddRoleList( string UserCode, string RoleCode, byte UserType, string UserName )
        {
            try
            {
                BLL.Cookie.TeUser U = GetCookie.GetUserCookie();
                //string UserCodeStr = UserCode + "' and SUBSTRING([RoleCode],1,1)='2";
                UserManager.DeleteUserRoleInfoItem( UserCode, UserType );
                if( RoleCode.IndexOf( "," ) > -1 )
                {
                    string[] RoleCodes = RoleCode.Split( ',' );
                    for( int i = 0; i < RoleCodes.Length; i++ )
                    {
                        EastElite.SMS.Business.UserRoleInfo User = new EastElite.SMS.Business.UserRoleInfo();
                        User.userCode = UserCode;
                        User.roleCode = RoleCodes[i];
                        User.userType = UserType;
                        User.note = "";
                        User.handledID = U.userCode;
                        User.handledDate = DateTime.Now;
                        string RoleInfoName = UserManager.GetRoleInfoName( RoleCodes[i] );
                        UserManager.InsertUserRoleInfoItem( User );
                        BLL.Log.UserLog.AddUserLog( "授权成功", GetCookie.GetUserCookie().userName + " 成功授权给 " + UserName + " " + RoleInfoName + "角色 " );
                    }
                }
                else
                {
                    EastElite.SMS.Business.UserRoleInfo User = new EastElite.SMS.Business.UserRoleInfo();
                    User.userCode = UserCode;
                    User.roleCode = RoleCode;
                    User.userType = UserType;
                    User.note = "";
                    User.handledID = U.userCode;
                    User.handledDate = DateTime.Now;
                    string RoleInfoName = UserManager.GetRoleInfoName( RoleCode );
                    UserManager.InsertUserRoleInfoItem( User );
                    BLL.Log.UserLog.AddUserLog( "授权成功", GetCookie.GetUserCookie().userName + " 成功授权给 " + UserName + " " + RoleInfoName + "角色 " );
                }
            }
            catch( Exception ex )
            {
                BLL.Log.UserLog.AddUserLog( "授权失败", ex.Message );
                throw;
            }
        }

        public List<Link> GetLink()
        {
            BLL.Cookie.TeUser U = GetCookie.GetUserCookie();

            string condition = string.Format( @"[UserType]=1  and UserCode ='{0}' ",  U.userCode );

            DataSet dataSet = UserManager.GetUserRoleInfoList( condition );
            string RoleCode = "";
            for( int i = 0; i < dataSet.Tables[0].Rows.Count; i++ )
            {
                RoleCode += "'" + dataSet.Tables[0].Rows[i]["RoleCode"].ToString() + "',";
            }
            if( RoleCode != "" )
            {
                RoleCode = RoleCode.Substring( 0, RoleCode.Length - 1 );
                StringBuilder strSql = new StringBuilder();
                strSql.Append( "SELECT DISTINCT " );
                strSql.Append( "[Code],[oaName],[link],[sort],[img],[State]" );
                strSql.Append( " FROM ViewRoleLink where " );
                strSql.Append( string.Format( "RoleCode In ({0}) order by sort asc", RoleCode ) );
                using( BLL.NETDISKDBEntities Db = new BLL.NETDISKDBEntities() )
                {
                    DataSet Dt = SqlQueryForDataTatable1( Db.Database, strSql.ToString() );
                    List<Link> result1 = List.ToList<Link>( Dt, 0 );
                    return result1.ToList();
                }
            }
            else
            {
                return null;
            }

        }
        /// <summary>
        /// EF SQL 语句返回 dataTable
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataSet SqlQueryForDataTatable1( Database db, string sql )
        {
            SqlConnection conn = new System.Data.SqlClient.SqlConnection();
            conn = (SqlConnection) db.Connection;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            SqlDataAdapter adapter = new SqlDataAdapter( cmd );
            DataSet table = new DataSet();
            adapter.Fill( table );
            conn.Close();//连接需要关闭
            conn.Dispose();
            return table;
        }
    }
}
