using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class ApplicationDbContextSeed
    {
        public static async Task Seed(IServiceProvider serviceProvider, IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Console.WriteLine("Seed.CreateRolesAndUsers()");
            //adding customs roles
            var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            string[] roleNames = { "Admin", "User" };
            IdentityResult result;
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    result = await roleManager.CreateAsync(new AppRole(roleName));
                }
            }

            try
            {
                if (!userManager.Users.Any())
                {
                    var admin = new AppUser
                    {
                        DisplayName = "Admin",
                        Email = "admin@email.com",
                        UserName = "admin@email.com",
                        Address = new Address
                        {
                            Street = "Example Street",
                            City = "Example City",
                            State = " EC ",
                            Zippcode = "15-400"
                        }
                    };
                    var createAdminResult = await userManager.CreateAsync(admin, "qwerty");
                    if (createAdminResult.Succeeded)
                    {
                        await userManager.AddToRoleAsync(admin, "Admin");
                    }

                    var user = new AppUser
                    {
                        DisplayName = "User",
                        Email = "user@email.com",
                        UserName = "user@email.com",
                        Address = new Address
                        {
                            Street = "Example Street",
                            City = "Example City",
                            State = " EC ",
                            Zippcode = "15-400"
                        }
                    };
                    var createUserResult = await userManager.CreateAsync(user, "qwerty");
                    if (createUserResult.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "User");
                    }

                }
                if (!context.Products.Any())
                {
                    if (!context.ProductBrands.Any())
                    {
                        var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");

                        var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                        foreach (var item in brands)
                        {
                            context.ProductBrands.Add(item);
                        }

                        await context.SaveChangesAsync();
                    }

                    if (!context.ProductTypes.Any())
                    {
                        var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");

                        var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                        foreach (var item in types)
                        {
                            context.ProductTypes.Add(item);
                        }

                        await context.SaveChangesAsync();
                    }

                    if (!context.Products.Any())
                    {
                        var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");

                        var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                        foreach (var item in products)
                        {
                            context.Products.Add(item);
                        }

                        await context.SaveChangesAsync();
                    }
                }

        

    }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<ApplicationDbContextSeed>();
                logger.LogError(e.Message);
            }
        }
    }
}