using LinqToDB;
using MapsterMapper;
using Microsoft.AspNetCore.Razor.TagHelpers;
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

        public Task<BankAccountEntity?> UpdateAsync(BankAccountEntity user)
        {
            return Task.FromResult(new BankAccountEntity());
        }

        public Task<BankAccountEntity?> GetBankAccount(Guid accountId, Guid userId)
        {
            return Task.FromResult(new BankAccountEntity());
        }
    }
}
