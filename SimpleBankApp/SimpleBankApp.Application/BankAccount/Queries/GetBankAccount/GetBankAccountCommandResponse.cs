namespace SimpleBankApp.Application.BankAccount.Queries.GetBankAccount
{
    public class GetBankAccountCommandResponse
    {
        public Guid Id { get; set; }
        public decimal Balance { get; set; } = 0;
        public DateTime LastUpdatedOn { get; set; }
    }
}
