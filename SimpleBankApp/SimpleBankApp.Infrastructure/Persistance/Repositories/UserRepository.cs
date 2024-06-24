using LinqToDB;
using MapsterMapper;
using SimpleBankApp.Application.Common.Interfaces.Persistance;
using SimpleBankApp.Domain.Entities;
using SimpleBankApp.Infrastructure.Persistance.Linq2DB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBankApp.Infrastructure.Persistance.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public static List<UserEntity> users = new List<UserEntity>();

        private readonly IMapper _mapper;

        public UserRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(UserEntity userEntity)
        {
            var user = _mapper.Map<User>(userEntity);
            SetCreateVars(user);
            var result = await Db.InsertAsync(user);
            return result > 0;
        }

        public async Task<UserEntity?> GetUserByEmailAsync(string email)
        {
            var user = await Db.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
            return _mapper.Map<UserEntity>(user);
        }
    }
}
