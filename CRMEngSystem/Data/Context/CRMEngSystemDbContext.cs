﻿using Microsoft.EntityFrameworkCore;
using System.Reflection;
using CRMEngSystem.Data.Entities.Catalog;
using CRMEngSystem.Data.Entities.Comment;
using CRMEngSystem.Data.Entities.Contact;
using CRMEngSystem.Data.Entities.Enterprise;
using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Data.Entities.User;
using CRMEngSystem.Data.Entities.WareHouse;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CRMEngSystem.Data.Context
{
    public sealed class CRMEngSystemDbContext : IdentityDbContext<UserEntity>
    {
        public CRMEngSystemDbContext(DbContextOptions<CRMEngSystemDbContext> options)
        : base(options)
        {
        }
        public DbSet<ContactEntity> Contacts { get; set; }
        public DbSet<ContactDetailsEntity> ContactsDetails { get; set; }
        public DbSet<EnterpriseEntity> Enterprises { get; set; }
        public DbSet<EnterpriseDetailsEntity> EnterprisesDetails { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<ContactOrderEntity> ContactOrders { get; set; }
        public DbSet<EquipmentCatalogPositionEntity> EquipmentCatalogPositions { get; set; }
        public DbSet<EquipmentOrderPositionEntity> EquipmentOrderPositions { get; set; }
        public DbSet<WareHouseEntity> WareHouses { get; set; }
        public DbSet<EquipmentWareHousePositionEntity> EquipmentWareHousePositions { get; set; }
        public DbSet<EquipmentWareHouseOrderEntity> EquipmentWareHouseOrder { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("IdentityUserRoles").HasNoKey();
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("IdentityUserClaims").HasNoKey();
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("IdentityUserLogins").HasNoKey();
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("IdentityUserTokens").HasNoKey();
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("IdentityRoleClaims").HasNoKey();
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
