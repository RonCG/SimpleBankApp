namespace SimpleBankApp.Api.Contracts.BankAccount.WithdrawFromBankAccount
{
    public class WithdrawFromBankAccountResponse
    {
        public Guid AccountId { get; set; }
        public decimal Balance { get; set; } = 0;
        public DateTime LastUpdatedOn { get; set; }
    }
}
