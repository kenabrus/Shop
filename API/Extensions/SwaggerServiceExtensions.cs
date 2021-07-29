using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace API.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            // services.AddSwaggerGen( s => 
            //     {
            //         s.SwaggerDoc("v1", new OpenApiInfo{Title = "Praca Inzynierska", Version = "v1"});

            //         var securitySchema = new OpenApiSecurityScheme
            //         {
            //             Description = "JWT Auth",
            //             Name = "Authorization",
            //             In = ParameterLocation.Header,
            //             Type = SecuritySchemeType.Http,
            //             Scheme = "bearer",
            //             Reference = new OpenApiReference
            //             {
            //                 Type = ReferenceType.SecurityScheme,
            //                 Id = "Bearer"
            //             }
            //         };
            //         s.AddSecurityDefinition("Bearer", securitySchema);
            //         var securityRequirement = new OpenApiSecurityRequirement{{securitySchema, new[]
            //         {"Bearer"}}};
            //         s.AddSecurityRequirement(securityRequirement);

            //     });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "My API",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                    },
                    new string[] { }
                    }
                });
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => {c.SwaggerEndpoint("/swagger/v1/swagger.json", "Praca Api v1");});

            return app;
        }
    }
}