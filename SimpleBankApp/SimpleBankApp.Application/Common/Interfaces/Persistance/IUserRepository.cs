using SimpleBankApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBankApp.Application.Common.Interfaces.Persistance
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<bool> AddAsync(User user);
    }
}
