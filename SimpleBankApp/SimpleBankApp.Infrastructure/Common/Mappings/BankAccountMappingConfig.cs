using Mapster;
using SimpleBankApp.Domain.Entities;
using SimpleBankApp.Infrastructure.Persistance.Linq2DB;

namespace SimpleBankApp.Infrastructure.Common.Mappings
{
    public class BankAccountMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<BankAccountEntity, BankAccount>();
            config.NewConfig<BankAccount, BankAccountEntity>();
        }
    }
}
