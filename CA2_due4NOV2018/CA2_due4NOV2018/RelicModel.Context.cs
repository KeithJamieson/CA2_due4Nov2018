﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CA2_due4NOV2018
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RELICEntities : DbContext
    {
        public RELICEntities()
            : base("name=RELICEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Club> Clubs { get; set; }
        public virtual DbSet<Competition> Competitions { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Entry> Entries { get; set; }
        public virtual DbSet<Leaderboard> Leaderboards { get; set; }
        public virtual DbSet<leaderboard_v> leaderboard_v { get; set; }
    }
}
