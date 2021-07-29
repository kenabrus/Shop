using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedUsers(IServiceProvider serviceProvider, IConfiguration configuration)
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


            if(!userManager.Users.Any())
            {
                var admin = new AppUser{
                    DisplayName = "Admin",
                    Email = "admin@email.com",
                    UserName = "admin@email.com",
                    Address = new Address
                    {
                        FirstName = "Admin",
                        LastName = "Admin",
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

                var user = new AppUser{
                    DisplayName = "User",
                    Email = "user@email.com",
                    UserName = "user@email.com",
                    Address = new Address
                    {
                        FirstName = "User",
                        LastName = "User",
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

        }
    }
}