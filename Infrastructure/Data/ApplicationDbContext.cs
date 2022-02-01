using System;
using System.Linq;
using System.Reflection;
using Core.Entities;
using Core.Entities.Identity;
using Infrastructure.Data.Config;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data
{
public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProductBrand> ProductBrands {get; set;}
        public DbSet<ProductType> ProductTypes {get; set; }

        public DbSet<Product> Products {get; set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>(entity =>
            {
                entity.ToTable(name: "User");
            });

            builder.Entity<AppRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });

            builder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole");
                 //in case you chagned the TKey type
                 entity.HasKey(key => new { key.UserId, key.RoleId });
             });

            builder.Entity<IdentityUserClaim<int>>(entity =>
            {
                entity.ToTable("UserClaim");
            });

            builder.Entity<IdentityUserLogin<int>>(entity =>
            {
                entity.ToTable("UserLogin");
                 //in case you chagned the TKey type
                 //  entity.HasKey(key => new { key.ProviderKey, key.LoginProvider });       
             });

            builder.Entity<IdentityRoleClaim<int>>(entity =>
            {
                entity.ToTable("RoleClaim");

            });

            builder.Entity<IdentityUserToken<int>>(entity =>
            {
                entity.ToTable("UserToken");
                 //in case you chagned the TKey type
                 // entity.HasKey(key => new { key.UserId, key.LoginProvider, key.Name });

             });

            builder.ApplyConfiguration(new ProductConfiguration());

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            if(Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach( var entityType in builder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));
                    var dateTimeProperties = entityType.ClrType.GetProperties().Where(x => x.PropertyType == typeof(DateTimeOffset));

                    foreach(var property in properties)
                    {
                        builder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
                    }

                    foreach(var property in dateTimeProperties)
                    {
                        builder.Entity(entityType.Name).Property(property.Name).HasConversion(new DateTimeOffsetToBinaryConverter());
                    }
                }
            }
        }
    }
}