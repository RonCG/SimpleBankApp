using Mapster;
using SimpleBankApp.Api.Contracts.BankAccount.CreateBankAccount;
using SimpleBankApp.Api.Contracts.BankAccount.DeleteBankAccount;
using SimpleBankApp.Api.Contracts.BankAccount.DepositInBankAccount;
using SimpleBankApp.Api.Contracts.BankAccount.GetBankAccount;
using SimpleBankApp.Api.Contracts.BankAccount.WithdrawFromBankAccount;
using SimpleBankApp.Application.BankAccount.Commands.CreateBankAccount;
using SimpleBankApp.Application.BankAccount.Commands.DeleteBankAccount;
using SimpleBankApp.Application.BankAccount.Commands.DepositInBankAccount;
using SimpleBankApp.Application.BankAccount.Commands.WithdrawFromBankAccount;
using SimpleBankApp.Application.BankAccount.Queries.GetBankAccount;

namespace SimpleBankApp.Api.Common.Mappings
{
    public class BankMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateBankAccountCommandResponse, CreateBankAccountResponse>()
                .Map(dest => dest.AccountId, src => src.Id);
            config.NewConfig<GetBankAccountQueryResponse, GetBankAccountResponse>();
            config.NewConfig<DepositInBankAccountCommandResponse, DepositInBankAccountResponse>();
            config.NewConfig<WithdrawFromBankAccountCommandResponse, WithdrawFromBankAccountResponse>();
            config.NewConfig<DeleteBankAccountCommandResponse, DeleteBankAccountResponse>();
        }
    }
}
