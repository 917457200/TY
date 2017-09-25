using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EastElite.SMS.Business.Data;
using EastElite.SMS.Business;

namespace BLL.WEB
{
    public class PageList
    {
        public List<T> GetPostList<T>( int pageIndex, int pageSize, string strWhere, string TableName, string Order )
        {
            string wherestr = "";
            if( !string.IsNullOrEmpty( strWhere ) )
            {
                wherestr = " oaName like '%" + strWhere + "%' and ";
            }
            int startRow = ( pageIndex - 1 ) * pageSize;
            StringBuilder strSql = new StringBuilder();

            strSql.Append( " SELECT TOP " + pageSize + " * From (SELECT ROW_NUMBER() OVER (ORDER BY " + Order + " desc) AS RowNumber,* FROM " + TableName + " where " + wherestr + " State =1 ) as A " );
            strSql.Append( "WHERE  RowNumber > " + startRow + " ORDER BY RowNumber ASC " );
            using( BLL.NETDISKDBEntities Db = new BLL.NETDISKDBEntities() )
            {
                DbRawSqlQuery<T> query = Db.Database.SqlQuery<T>( strSql.ToString() );
                return query.ToList();
            }

        }
        public int GetPostCount()
        {
            int Count = 0;
            using( BLL.NETDISKDBEntities Db = new BLL.NETDISKDBEntities() )
            {
                Count = Db.Link.Count();
                return Count;
            }
        }
        public int GetUserLogCount()
        {
            int Count = 0;
            using( BLL.NETDISKDBEntities Db = new BLL.NETDISKDBEntities() )
            {
                Count = Db.UserLog.Count();
                return Count;
            }
        }
        /// <summary>
        /// 角色列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">一页展示多少条</param>
        /// <param name="Name">名称</param>
        /// <param name="Count">总数返回</param>
        /// <returns></returns>
        public List<SMSModel.RoleInfo> GetPageList( int pageIndex, int pageSize, string RoleName, ref int Count )
        {
            DataSetToList List = new DataSetToList();

            StringBuilder where = new StringBuilder();
            where.Append( " 1=1 " );

            //where.Append( " Code Like '2%' " );
            if( !string.IsNullOrEmpty( RoleName ) )
            {
                where.Append( " and Name like '%" + RoleName + "%' " );
            }
            where.Append( " order by Code asc " );

            DataSet dt = UserManager.GetRoleList( where.ToString() );

            List<SMSModel.RoleInfo> RoleInfolist = List.ToList<SMSModel.RoleInfo>( dt, 0 );
            Count = RoleInfolist.Count();
            int startRow = ( pageIndex - 1 ) * pageSize;
            return RoleInfolist.Skip( startRow ).Take( pageSize ).ToList();
        }
        /// <summary>
        /// 人员列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">一页展示多少条</param>
        /// <param name="Name">名称</param>
        /// <param name="Count">总数返回</param>
        /// <returns></returns>
        public List<SMSModel.UserRoleInfoList> GetUserRoleList( int pageIndex, int pageSize, string condition, ref int Count )
        {
            DataSetToList List = new DataSetToList();

            DataSet dataSet = UserManager.GetUserRoleInfoList( condition );

            List<SMSModel.UserRoleInfoList> RoleInfolist = List.ToList<SMSModel.UserRoleInfoList>( dataSet, 0 );
            Count = RoleInfolist.Count();
            int startRow = ( pageIndex - 1 ) * pageSize;
            return RoleInfolist.Skip( startRow ).Take( pageSize ).ToList();
        }

        public List<SMSModel.UserInfoList> GetUserList( int pageIndex, int pageSize, string condition, ref int Count )
        {
            DataSetToList List = new DataSetToList();

            DataSet dataSet = UserManager.GetUserInfoList( condition );

            List<SMSModel.UserInfoList> RoleInfolist = List.ToList<SMSModel.UserInfoList>( dataSet, 0 );
            Count = RoleInfolist.Count();
            int startRow = ( pageIndex - 1 ) * pageSize;
            return RoleInfolist.Skip( startRow ).Take( pageSize ).ToList();
        }
    }
}
