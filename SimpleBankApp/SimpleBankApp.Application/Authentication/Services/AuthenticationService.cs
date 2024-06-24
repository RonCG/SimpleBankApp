using ErrorOr;
using SimpleBankApp.Application.Authentication.Models;
using SimpleBankApp.Application.Common.Interfaces.Authentication;
using SimpleBankApp.Application.Common.Interfaces.Persistance;
using SimpleBankApp.Domain.Common.Errors;
using SimpleBankApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBankApp.Application.Authentication.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJWTTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(
            IJWTTokenGenerator jWTTokenGenerator, 
            IUserRepository userRepository)
        {
            _jwtTokenGenerator = jWTTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<RegisterResult>> Register(string firstName, string lastName, string email, string password)
        {
            //check if user exists
            var existingUser = await _userRepository.GetUserByEmailAsync(email);
            if(existingUser != null)
            {
                return Errors.User.ExistingUser;
            }

            var newUser = new User()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = BCrypt.Net.BCrypt.HashPassword(password)
            };

            await _userRepository.AddAsync(newUser);

            var registerResult = new RegisterResult
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
            };

            return registerResult;
        }

        public async Task<ErrorOr<LoginResult>> Login(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return Errors.Authentication.InvalidCredentials;
            }

            var loginResult = new LoginResult
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = email,
                Token = _jwtTokenGenerator.GenerateToken(user.Id, user.FirstName, user.LastName)
            };

            return loginResult;
        }

    }
}
