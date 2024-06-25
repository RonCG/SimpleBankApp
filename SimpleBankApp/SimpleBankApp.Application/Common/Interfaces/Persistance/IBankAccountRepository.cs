using SimpleBankApp.Domain.Entities;

namespace SimpleBankApp.Application.Common.Interfaces.Persistance
{
    public interface IBankAccountRepository
    {
        Task<BankAccountEntity?> AddAsync(BankAccountEntity user);
        Task<BankAccountEntity?> UpdateAsync(BankAccountEntity user);
        Task<BankAccountEntity?> GetBankAccount(Guid accountId, Guid userId);
    }
}
