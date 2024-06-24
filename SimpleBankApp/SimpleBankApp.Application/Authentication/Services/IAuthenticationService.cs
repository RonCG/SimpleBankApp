using ErrorOr;
using SimpleBankApp.Application.Authentication.Models.Inputs;
using SimpleBankApp.Application.Authentication.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBankApp.Application.Authentication.Services
{
    public interface IAuthenticationService
    {
        public Task<ErrorOr<RegisterResult>> Register(RegisterInput registerInput);
        public Task<ErrorOr<LoginResult>> Login(LoginInput loginInput);
    }
}
