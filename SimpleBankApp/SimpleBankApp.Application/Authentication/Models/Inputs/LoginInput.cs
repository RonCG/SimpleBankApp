using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBankApp.Application.Authentication.Models.Inputs
{
    public class LoginInput
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
