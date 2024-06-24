using SimpleBankApp.Application.Common.Interfaces.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBankApp.Infrastructure.Authentication
{
    public class JWTTokenGenerator : IJWTTokenGenerator
    {
        public string GenerateToken(Guid userId, string firstName, string lastName)
        {
            return "test";
        }
    }
}
