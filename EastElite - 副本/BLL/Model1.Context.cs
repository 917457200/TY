﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace BLL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class NETDISKDBEntities : DbContext
    {
        public NETDISKDBEntities()
            : base("name=NETDISKDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Link> Link { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<OaRole> OaRole { get; set; }
        public virtual DbSet<UserLog> UserLog { get; set; }
    }
}
