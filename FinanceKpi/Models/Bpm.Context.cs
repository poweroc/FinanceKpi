﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FinanceKpi.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BPMDBEntities : DbContext
    {
        public BPMDBEntities()
            : base("name=BPMDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BPMInstProcSteps> BPMInstProcSteps { get; set; }
        public virtual DbSet<BPMInstTasks> BPMInstTasks { get; set; }
        public virtual DbSet<BPMSysUsers> BPMSysUsers { get; set; }
        public virtual DbSet<iAssetDetail> iAssetDetail { get; set; }
        public virtual DbSet<iAsset> iAsset { get; set; }
    }
}