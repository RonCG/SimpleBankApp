using Microsoft.Extensions.DependencyInjection;
using SimpleBankApp.Application.Authentication.Services;

namespace SimpleBankApp.Application.Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            return services;
        } 
    }
}
