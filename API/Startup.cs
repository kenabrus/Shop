using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

using API.Helpers;
using API.Extensions;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }

        public IConfiguration _config { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfiles));    
            services.AddControllers();
            services.AddDbContext<ApplicationDbContext>(x =>
                //  x.UseSqlServer(_config.GetConnectionString("MsSqlDefaultConnection"),
                // b => b.MigrationsAssembly("Infrastructure")));
                x.UseSqlite(_config.GetConnectionString("DefaultConnection")));

            services.AddApplicationServices();
            services.AddIdentityServices(_config);

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    //policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:5000");
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                 });
            });


            // services.AddSwaggerGen(c =>
            // {
            //     c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            // });
            services.AddSwaggerDocumentation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // app.UseSwagger();
                // app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
                app.UseSwaggerDocumentation();
            }

            //app.UseHttpsRedirection();
            // app.UseStaticFiles(new StaticFileOptions
            // {
            //     FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, @"Content")),
            //     RequestPath = "/Content"
            // });



            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
