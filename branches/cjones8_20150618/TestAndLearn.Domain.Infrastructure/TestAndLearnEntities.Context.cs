﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestAndLearn.Domain.Infrastructure
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    internal partial class TestAndLearnEntitiesConnection : DbContext
    {
        public TestAndLearnEntitiesConnection()
            : base("name=TestAndLearnEntitiesConnection")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<UserClientMap> UserClientMaps { get; set; }
        public virtual DbSet<SuccessMetric> SuccessMetrics { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<TestStatu> TestStatus { get; set; }
        public virtual DbSet<TestType> TestTypes { get; set; }
        public virtual DbSet<UserSetting> UserSettings { get; set; }
    }
}