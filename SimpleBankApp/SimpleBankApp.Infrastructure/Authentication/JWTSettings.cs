using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBankApp.Infrastructure.Authentication
{
    public class JWTSettings
    {
        public const string SectionName = "JWTSettings";
        public string? Secret {  get; init; }
        public int ExpirationTimeInMinutes { get; init; }
        public string? Issuer { get; init; }
        public string? Audience { get; init; }
    }
}
