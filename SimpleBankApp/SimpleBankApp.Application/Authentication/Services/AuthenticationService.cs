using ErrorOr;
using SimpleBankApp.Application.Authentication.Models.Inputs;
using SimpleBankApp.Application.Authentication.Models.Results;
using SimpleBankApp.Application.Common.Interfaces.Authentication;
using SimpleBankApp.Application.Common.Interfaces.Persistance;
using SimpleBankApp.Domain.Common.Errors;
using SimpleBankApp.Domain.Entities;


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

        public async Task<ErrorOr<RegisterResult>> Register(RegisterInput registerInput)
        {
            //check if user exists
            var existingUser = await _userRepository.GetUserByEmailAsync(registerInput.Email);
            if(existingUser != null)
            {
                return Errors.User.ExistingUser;
            }

            var newUser = new UserEntity()
            {
                FirstName = registerInput.FirstName,
                LastName = registerInput.LastName,
                Email = registerInput.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerInput.Password)
            };

            await _userRepository.AddAsync(newUser);

            var registerResult = new RegisterResult
            {
                FirstName = registerInput.FirstName,
                LastName = registerInput.LastName,
                Email = registerInput.Email,
            };

            return registerResult;
        }

        public async Task<ErrorOr<LoginResult>> Login(LoginInput loginInput)
        {
            var user = await _userRepository.GetUserByEmailAsync(loginInput.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginInput.Password, user.Password))
            {
                return Errors.Authentication.InvalidCredentials;
            }

            var loginResult = new LoginResult
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Token = _jwtTokenGenerator.GenerateToken(user.Id, user.FirstName, user.LastName)
            };

            return loginResult;
        }

    }
}
