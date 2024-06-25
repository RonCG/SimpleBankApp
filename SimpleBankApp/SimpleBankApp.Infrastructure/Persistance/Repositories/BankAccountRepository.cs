using LinqToDB;
using MapsterMapper;
using SimpleBankApp.Application.Common.Interfaces.Persistance;
using SimpleBankApp.Domain.Entities;
using SimpleBankApp.Infrastructure.Persistance.Linq2DB;
using SimpleBankApp.Infrastructure.Persistance.Repositories.Common;

namespace SimpleBankApp.Infrastructure.Persistance.Repositories
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly IMapper _mapper;
        private readonly ICommonRepository _commonRepository;

        public BankAccountRepository(
            IMapper mapper, 
            ICommonRepository commonRepository)
        {
            _mapper = mapper;
            _commonRepository = commonRepository;
        }

        public async Task<BankAccountEntity?> AddAsync(BankAccountEntity bankAccountEntity)
        {
            var bankAccount = _mapper.Map<BankAccount>(bankAccountEntity);
            var result = await _commonRepository.InsertAsync(bankAccount);
            return result > 0 ? _mapper.Map<BankAccountEntity>(bankAccount) : null;
        }

        public async Task<BankAccountEntity?> UpdateAsync(BankAccountEntity bankAccountEntity)
        {
            var bankAccount = _mapper.Map<BankAccount>(bankAccountEntity);
            var result = await _commonRepository.UpdateAsync(bankAccount);
            return result > 0 ? _mapper.Map<BankAccountEntity>(bankAccount) : null;
        }

        public async Task<BankAccountEntity?> GetBankAccount(Guid accountId, Guid userId)
        {
            var bankAccount = await _commonRepository.GetSimpleBankDb().BankAccounts
                                    .Where(x => x.Id == accountId && x.UserId == userId)
                                    .FirstOrDefaultAsync();

            return _mapper.Map<BankAccountEntity>(bankAccount);
        }

        public async Task<bool> DeleteAsync(BankAccountEntity bankAccountEntity)
        {
            var bankAccount = _mapper.Map<BankAccount>(bankAccountEntity);
            var result = await _commonRepository.DeleteAsync(bankAccount);
            return result > 0;
        }
    }
}
