﻿namespace SimpleBankApp.Api.Contracts.Authentication.Request
{
    public class RegisterRequest
    {
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
    }
}
