﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class clinicEntities2 : DbContext
    {
        public clinicEntities2()
            : base("name=clinicEntities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<accepted_services_statuses> accepted_services_statuses { get; set; }
        public virtual DbSet<accepted_servises> accepted_servises { get; set; }
        public virtual DbSet<analyzers> analyzers { get; set; }
        public virtual DbSet<countries> countries { get; set; }
        public virtual DbSet<insurances> insurances { get; set; }
        public virtual DbSet<orders> orders { get; set; }
        public virtual DbSet<patients> patients { get; set; }
        public virtual DbSet<services> services { get; set; }
        public virtual DbSet<social_types> social_types { get; set; }
        public virtual DbSet<users> users { get; set; }
        public virtual DbSet<user_in> user_in { get; set; }
        public virtual DbSet<users_type> users_type { get; set; }
    }
}
