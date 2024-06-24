using Mapster;
using SimpleBankApp.Api.Contracts.Authentication.Request;
using SimpleBankApp.Api.Contracts.Authentication.Response;
using SimpleBankApp.Application.Authentication.Models.Inputs;
using SimpleBankApp.Application.Authentication.Models.Results;
using SimpleBankApp.Domain.Entities;

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
        }
    }
}
