﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TraCuuThongTIn.DBContext
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QLMauEntities : DbContext
    {
        public QLMauEntities()
            : base("name=QLMauEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tbNgheNghiep> tbNgheNghieps { get; set; }
        public virtual DbSet<tbNhomMau> tbNhomMaus { get; set; }
        public virtual DbSet<tbQuanHuyen> tbQuanHuyens { get; set; }
        public virtual DbSet<tbQuyen> tbQuyens { get; set; }
        public virtual DbSet<tbThongTinCaNhan> tbThongTinCaNhans { get; set; }
        public virtual DbSet<tbTinhThanhPho> tbTinhThanhPhoes { get; set; }
        public virtual DbSet<tbTinhTrangHonNhan> tbTinhTrangHonNhans { get; set; }
        public virtual DbSet<tbXaPhuong> tbXaPhuongs { get; set; }
    }
}
