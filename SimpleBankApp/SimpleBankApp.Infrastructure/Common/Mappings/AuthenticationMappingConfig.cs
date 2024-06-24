using Mapster;
using SimpleBankApp.Domain.Entities;
using SimpleBankApp.Infrastructure.Persistance.Linq2DB;

namespace SimpleBankApp.Infrastructure.Common.Mappings
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<UserEntity, User>();
            config.NewConfig<User, UserEntity>();
        }
    }
}
