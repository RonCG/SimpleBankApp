using Mapster;
using SimpleBankApp.Application.BankAccount.Commands.CreateBankAccount;
using SimpleBankApp.Domain.Entities;

namespace SimpleBankApp.Application.Common.Mappings
{
    public class BankAccountMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<BankAccountEntity, CreateBankAccountCommandResponse>();
        }
    }
}
