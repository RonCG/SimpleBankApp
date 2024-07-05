namespace SimpleBankApp.Api.Contracts.BankAccount.GetBankAccount
{
    public class GetBankAccountResponse
    {
        public Guid AccountId { get; set; }
        public decimal Balance { get; set; } = 0;
        public DateTime LastUpdatedOn { get; set; }
    }
}
