using SimpleBankApp.Application.Common.Interfaces.Persistance;
using SimpleBankApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBankApp.Infrastructure.Persistance.Repositories
{
    public class UserRepository : IUserRepository
    {
        public static List<User> users = new List<User>();

        public Task<bool> AddAsync(User user)
        {
            users.Add(user);

            return Task.FromResult(true);
        }

        public Task<User?> GetUserByEmailAsync(string email)
        {
            var user = users.Where(x => x.Email == email).FirstOrDefault();

            return Task.FromResult(user);
        }
    }
}
