using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SimpleBankApp.Api.Common.Errors;
using SimpleBankApp.Api.Common.Mappings;
using SimpleBankApp.Api.Contracts.Authentication.Validators;
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
            
            services.AddSingleton<ProblemDetailsFactory, SimpleBankAppProblemDetailsFactory>();
            services.AddMappings();
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
