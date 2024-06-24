using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleBankApp.Application.Common.Interfaces.Authentication;
using SimpleBankApp.Application.Common.Interfaces.Persistance;
using SimpleBankApp.Infrastructure.Authentication;
using SimpleBankApp.Infrastructure.Persistance.Repositories;

namespace SimpleBankApp.Infrastructure.Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.Configure<JWTSettings>(configuration.GetSection(JWTSettings.SectionName));

            services.AddSingleton<IJWTTokenGenerator, JWTTokenGenerator>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
