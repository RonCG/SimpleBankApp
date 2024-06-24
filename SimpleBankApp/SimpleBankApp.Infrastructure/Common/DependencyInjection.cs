using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SimpleBankApp.Application.Common.Interfaces.Authentication;
using SimpleBankApp.Application.Common.Interfaces.Persistance;
using SimpleBankApp.Infrastructure.Authentication;
using SimpleBankApp.Infrastructure.Persistance.Repositories;
using System.Text;

namespace SimpleBankApp.Infrastructure.Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, ConfigurationManager configuration)
        {
            var jwtSettings = new JWTSettings();
            configuration.Bind(JWTSettings.SectionName, jwtSettings);
            services.AddSingleton(Options.Create(jwtSettings));
            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                });


            services.AddSingleton<IJWTTokenGenerator, JWTTokenGenerator>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
