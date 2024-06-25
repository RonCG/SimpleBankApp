using SimpleBankApp.Domain.Entities;

namespace SimpleBankApp.Application.Common.Interfaces.Persistance
{
    public interface IBankAccountRepository
    {
        Task<BankAccountEntity?> AddAsync(BankAccountEntity user);
    }
}
