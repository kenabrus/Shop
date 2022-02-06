using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
//using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {

                    // var context = services.GetRequiredService<StoreContext>();
                    // await context.Database.MigrateAsync();
                    // await StoreContextSeed.SeedAsync(context, loggerFactory);

                    //var userManager = services.GetRequiredService<UserManager<AppUser>>();
                    //var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
                    var identityContext = services.GetRequiredService<ApplicationDbContext>();
                    await identityContext.Database.MigrateAsync();
                    //await AppIdentityDbContextSeed.SeedUsers(userManager, roleManager);

                    var serviceProvider = services.GetRequiredService<IServiceProvider>();
                    var configuration = services.GetRequiredService<IConfiguration>();
                    // InitializeUsersAndRoles.InitializeAsync(serviceProvider, configuration).Wait();
                    ApplicationDbContextSeed.Seed(serviceProvider, configuration, loggerFactory).Wait();

                }
                catch(Exception e)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(e, "Error occured during migrations");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        var env = hostingContext.HostingEnvironment;
                        config.AddJsonFile("appsettings.json", true, true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);
                        config.AddEnvironmentVariables();
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
