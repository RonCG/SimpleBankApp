using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SimpleBankApp.Api.Common.Errors;
using SimpleBankApp.Api.Common.Mappings;
using System.Reflection;

namespace SimpleBankApp.Api.Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services)
        {
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddMappings();

            services.AddSingleton<ProblemDetailsFactory, SimpleBankAppProblemDetailsFactory>();

            return services;
        }
    }
}
