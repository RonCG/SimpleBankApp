namespace SimpleBankApp.Api.Contracts.Authentication.Request
{
    public class LoginRequest
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
