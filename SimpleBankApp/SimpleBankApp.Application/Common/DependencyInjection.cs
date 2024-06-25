using ErrorOr;
using Microsoft.Extensions.DependencyInjection;
using SimpleBankApp.Application.Authentication.Services;
using SimpleBankApp.Application.BankAccount.Commands.CreateBankAccount;
using SimpleBankApp.Application.BankAccount.Commands.DeleteBankAccount;
using SimpleBankApp.Application.BankAccount.Commands.DepositInBankAccount;
using SimpleBankApp.Application.BankAccount.Commands.WithdrawFromBankAccount;
using SimpleBankApp.Application.Common.Mappings;
using SimpleBankApp.Domain.Entities;

namespace SimpleBankApp.Application.Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMappings();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<ICreateBankAccountCommandHandler, CreateBankAccountCommandHandler>();
            services.AddScoped<IDepositInBankAccountCommandHandler, DepositInBankAccountCommandHandler>();
            services.AddScoped<IWithdrawFromBankAccountCommandHandler, WithdrawFromBankAccountCommandHandler>();
            services.AddScoped<IDeleteBankAccountCommandHandler, DeleteBankAccountCommandHandler>();

            return services;
        } 
    }
}
