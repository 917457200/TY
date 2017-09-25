using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SMSModel
    {
        //首先定义类，类下面的属性成员用来接收从table表里取出的对应字段  
        public class RoleInfo
        {
            #region Model
            private long _id;
            private string _code;
            private string _name;
            private string _note;
            private string _handledid;
            private string _handleddate;
            private string _linkcode;

            /// <summary>
            /// 
            /// </summary>
            public long ID
            {
                set { _id = value; }
                get { return _id; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string Code
            {
                set { _code = value; }
                get { return _code; }
            }
            /// <summary>
            /// 
            /// </summary>
            [DisplayName( "角色名称" )]
            [Required( ErrorMessage = "请输入角色名称" )]
            public string Name
            {
                set { _name = value; }
                get { return _name; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string Note
            {
                set { _note = value; }
                get { return _note; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string HandledID
            {
                set { _handledid = value; }
                get { return _handledid; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string HandledDate
            {
                set { _handleddate = value; }
                get { return _handleddate; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string LinkCode
            {
                set { _linkcode = value; }
                get { return _linkcode; }
            }
            #endregion Model
        }
        public partial class UserRoleInfoList
        {

            #region Model
            private long _id;
            private string _usercode;
            private string _username;
            private string _agencycode;
            private string _agencyname;
            private string _rolecode;
            private string _rolename;
            private int _usertype;
            private string _note;
            private string _handledid;
            private string _handledname;
            private string _handleddate;
            /// <summary>
            /// 
            /// </summary>
            public long ID
            {
                set { _id = value; }
                get { return _id; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string UserCode
            {
                set { _usercode = value; }
                get { return _usercode; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string UserName
            {
                set { _username = value; }
                get { return _username; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string AgencyCode
            {
                set { _agencycode = value; }
                get { return _agencycode; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string AgencyName
            {
                set { _agencyname = value; }
                get { return _agencyname; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string RoleCode
            {
                set { _rolecode = value; }
                get { return _rolecode; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string RoleName
            {
                set { _rolename = value; }
                get { return _rolename; }
            }
            /// <summary>
            /// 
            /// </summary>
            public int UserType
            {
                set { _usertype = value; }
                get { return _usertype; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string Note
            {
                set { _note = value; }
                get { return _note; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string HandledID
            {
                set { _handledid = value; }
                get { return _handledid; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string HandledName
            {
                set { _handledname = value; }
                get { return _handledname; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string HandledDate
            {
                set { _handleddate = value; }
                get { return _handleddate; }
            }
            #endregion Model

        }
        public partial class UserInfoList
        {
            #region Model
            private long _id;
            private string _usercode;
            private int _usertype;
            private string _username;
            private string _usertypeidtext;
            private int? _usersex;
            private string _agencyname;
            private string _originalpwd;
            private string _note;
            private bool _ismodified;
            private bool _isvalid;
            private string _handleddate;
            /// <summary>
            /// 
            /// </summary>
            public long ID
            {
                set { _id = value; }
                get { return _id; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string UserCode
            {
                set { _usercode = value; }
                get { return _usercode; }
            }
            /// <summary>
            /// 
            /// </summary>
            public int UserType
            {
                set { _usertype = value; }
                get { return _usertype; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string UserName
            {
                set { _username = value; }
                get { return _username; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string UserTypeIDText
            {
                set { _usertypeidtext = value; }
                get { return _usertypeidtext; }
            }
            /// <summary>
            /// 
            /// </summary>
            public int? UserSex
            {
                set { _usersex = value; }
                get { return _usersex; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string AgencyName
            {
                set { _agencyname = value; }
                get { return _agencyname; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string OriginalPwd
            {
                set { _originalpwd = value; }
                get { return _originalpwd; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string Note
            {
                set { _note = value; }
                get { return _note; }
            }
            /// <summary>
            /// 
            /// </summary>
            public bool IsModified
            {
                set { _ismodified = value; }
                get { return _ismodified; }
            }
            /// <summary>
            /// 
            /// </summary>
            public bool IsValid
            {
                set { _isvalid = value; }
                get { return _isvalid; }
            }
            /// <summary>
            /// 
            /// </summary>
            public string HandledDate
            {
                set { _handleddate = value; }
                get { return _handleddate; }
            }
            #endregion Model

        }
    }
}
