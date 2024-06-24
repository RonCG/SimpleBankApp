using SimpleBankApp.Application.Authentication.Models;
using SimpleBankApp.Application.Common.Interfaces.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBankApp.Application.Authentication.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJWTTokenGenerator _jWTTokenGenerator;

        public AuthenticationService(IJWTTokenGenerator jWTTokenGenerator)
        {
            _jWTTokenGenerator = jWTTokenGenerator;
        }

        public RegisterResult Register(string firstName, string lastName, string email, string password)
        {

            Guid userId = Guid.NewGuid();
            var token = _jWTTokenGenerator.GenerateToken(userId, firstName, lastName);

            return new RegisterResult();
        }

        public LoginResult Login(string email, string password)
        {
            throw new NotImplementedException();
        }

    }
}
