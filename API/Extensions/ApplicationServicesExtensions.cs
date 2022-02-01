using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
            return services;
        }
        
    }
}