namespace SimpleBankApp.Api.Contracts.BankAccount.DepositInBankAccount
{
    public class DepositInBankAccountResponse
    {
        public Guid AccountId { get; set; }
        public decimal Balance { get; set; } = 0;
        public DateTime LastUpdatedOn { get; set; }
    }
}
