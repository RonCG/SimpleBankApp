using SimpleBankApp.Application.Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBankApp.Application.Authentication.Services
{
    public interface IAuthenticationService
    {
        public Task<RegisterResult> Register(string firstName, string lastName, string email, string password);
        public Task<LoginResult> Login(string email, string password);
    }
}
