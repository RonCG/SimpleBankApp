using ErrorOr;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SimpleBankApp.Application.Authentication.Services;
using SimpleBankApp.Application.BankAccount.Commands.DeleteBankAccount;
using SimpleBankApp.Application.BankAccount.Commands.DepositInBankAccount;
using SimpleBankApp.Application.BankAccount.Commands.WithdrawFromBankAccount;
using SimpleBankApp.Application.BankAccount.Queries.GetBankAccount;
using SimpleBankApp.Application.Common.Mappings;
using SimpleBankApp.Domain.Entities;
using System.Reflection;

namespace SimpleBankApp.Application.Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMappings();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddScoped<IGetBankAccountQueryHandler, GetBankAccountQueryHandler>();

            return services;
        } 
    }
}
