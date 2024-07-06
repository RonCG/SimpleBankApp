using LinqToDB;
using MapsterMapper;
using SimpleBankApp.Application.Common.Interfaces.Persistance;
using SimpleBankApp.Domain.Entities;
using SimpleBankApp.Infrastructure.Persistance.Linq2DB;
using SimpleBankApp.Infrastructure.Persistance.Repositories.Common;

namespace SimpleBankApp.Infrastructure.Persistance.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly ICommonRepository _commonRepository;

        public UserRepository(
            IMapper mapper, 
            ICommonRepository commonRepository)
        {
            _mapper = mapper;
            _commonRepository = commonRepository;
        }

        public async Task<bool> AddAsync(UserEntity userEntity)
        {
            var user = _mapper.Map<User>(userEntity);
            var result = await _commonRepository.InsertAsync(user);
            return result > 0;
        }

        public async Task<UserEntity?> GetUserByEmailAsync(string email)
        {
            var user = _commonRepository.GetByPredicate<User>(x => x.Email == email);
            if (user == null)
            {
                return null;
            }

            return await Task.FromResult(_mapper.Map<UserEntity>(user));
        }
    }
}
