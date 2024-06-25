using SimpleBankApp.Domain.Entities;

namespace SimpleBankApp.Application.Common.Interfaces.Persistance
{
    public interface IBankAccountRepository
    {
        Task<BankAccountEntity?> AddAsync(BankAccountEntity bankAccount);
        Task<BankAccountEntity?> UpdateAsync(BankAccountEntity bankAccount);
        Task<BankAccountEntity?> GetBankAccount(Guid accountId, Guid userId);
        Task<bool> DeleteAsync(BankAccountEntity bankAccount);
    }
}
