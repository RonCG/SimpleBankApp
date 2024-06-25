using Mapster;
using SimpleBankApp.Api.Contracts.Authentication.Request;
using SimpleBankApp.Api.Contracts.Authentication.Response;
using SimpleBankApp.Api.Contracts.BankAccount.CreateBankAccount;
using SimpleBankApp.Api.Contracts.BankAccount.DeleteBankAccount;
using SimpleBankApp.Api.Contracts.BankAccount.DepositInBankAccount;
using SimpleBankApp.Api.Contracts.BankAccount.WithdrawFromBankAccount;
using SimpleBankApp.Application.Authentication.Models.Inputs;
using SimpleBankApp.Application.Authentication.Models.Results;
using SimpleBankApp.Application.BankAccount.Commands.CreateBankAccount;
using SimpleBankApp.Application.BankAccount.Commands.DeleteBankAccount;
using SimpleBankApp.Application.BankAccount.Commands.DepositInBankAccount;
using SimpleBankApp.Application.BankAccount.Commands.WithdrawFromBankAccount;

namespace SimpleBankApp.Api.Common.Mappings
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, RegisterInput>();
            config.NewConfig<LoginRequest, LoginInput>();
            config.NewConfig<RegisterResult, RegisterResponse>();
            config.NewConfig<LoginResult, LoginResponse>();
            config.NewConfig<CreateBankAccountCommandResponse, CreateBankAccountResponse>()
                .Map(dest => dest.AccountId, src => src.Id);
            config.NewConfig<DepositInBankAccountCommandResponse, DepositInBankAccountResponse>();
            config.NewConfig<WithdrawFromBankAccountCommandResponse, WithdrawFromBankAccountResponse>();
            config.NewConfig<DeleteBankAccountCommandResponse, DeleteBankAccountResponse>();
        }
    }
}
