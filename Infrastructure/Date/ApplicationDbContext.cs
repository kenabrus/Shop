using Core.Entities;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, string, IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

<<<<<<< HEAD
        public DbSet<Product> Products {get; set;}
=======
        public DbSet<ProductBrand> ProductBrands {get; set;}
        public DbSet<ProductType> ProductTypes {get; set; }

        public DbSet<Product> Producs {get; set;}
>>>>>>> test

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

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaim");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogin");
                 //in case you chagned the TKey type
                 //  entity.HasKey(key => new { key.ProviderKey, key.LoginProvider });       
             });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaim");

            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserToken");
                 //in case you chagned the TKey type
                 // entity.HasKey(key => new { key.UserId, key.LoginProvider, key.Name });

             });
        }
    }
}