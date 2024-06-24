using Microsoft.Extensions.DependencyInjection;
using SimpleBankApp.Application.Common.Interfaces.Authentication;
using SimpleBankApp.Infrastructure.Authentication;

namespace SimpleBankApp.Infrastructure.Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<IJWTTokenGenerator, JWTTokenGenerator>();

            return services;
        }
    }
}
