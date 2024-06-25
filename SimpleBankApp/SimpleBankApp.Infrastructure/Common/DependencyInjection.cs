using LinqToDB.Common;
using LinqToDB.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SimpleBankApp.Application.Common.Interfaces.Authentication;
using SimpleBankApp.Application.Common.Interfaces.Persistance;
using SimpleBankApp.Infrastructure.Authentication;
using SimpleBankApp.Infrastructure.Common.Mappings;
using SimpleBankApp.Infrastructure.Persistance;
using SimpleBankApp.Infrastructure.Persistance.Repositories;
using SimpleBankApp.Infrastructure.Persistance.Repositories.Common;
using System.Text;

namespace SimpleBankApp.Infrastructure.Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddMappings();

            var dbSettings = new DBSettings();
            configuration.Bind(DBSettings.SectionName, dbSettings);
            DataConnection.DefaultDataProvider = dbSettings.Provider;
            DataConnection.DefaultConfiguration = "default";
            DataConnection.AddConfiguration("default", dbSettings.ConnectionString, null);
            
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

            services.AddScoped<ICommonRepository, CommonRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBankAccountRepository, BankAccountRepository>();

            return services;
        }
    }
}
