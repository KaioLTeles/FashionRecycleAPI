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
            services.AddScoped<IClientBusiness, ClientBusiness>();
            services.AddScoped<IPartnerBusiness, PartnerBusiness>();
            services.AddScoped<IProviderBusiness, ProviderBusiness>();
            services.AddScoped<IProductBusiness, ProductBusiness>();
            services.AddScoped<ISalesBusiness, SalesBusiness>();
            services.AddScoped<IPaymentBusiness, PaymentBusiness>();


            //Infraestructure.Data
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IPartnerRepository, PartnerRepository>();
            services.AddScoped<IProviderRepository, ProviderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISalesRepository, SalesRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();

            return services;

        }
    }
}
