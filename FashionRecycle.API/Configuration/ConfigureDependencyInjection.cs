using FashionRecycle.API.Core.Interface;
using FashionRecycle.Application.Business;
using FashionRecycle.Infrastructure.Data.Repository;

namespace FashionRecycle.API.Configuration
{
    public static class ConfigureDependencyInjection
    {
        public static IServiceCollection Resolvependency(this IServiceCollection services)
        {
            // Application
            services.AddScoped<IUserBusiness, UserBusiness>();


            //Infraestructure.Data
            services.AddScoped<IUserRepository, UserRepository>();

            return services;

        }
    }
}
