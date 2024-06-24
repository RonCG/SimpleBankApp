using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBankApp.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class User
        {
            public static Error ExistingUser => Error.Conflict(code: "User.ExistingUser", description: "User already exists");
        }
    }
}
