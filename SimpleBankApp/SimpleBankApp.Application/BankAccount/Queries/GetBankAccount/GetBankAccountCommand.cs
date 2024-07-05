namespace SimpleBankApp.Application.BankAccount.Queries.GetBankAccount
{
    public class GetBankAccountCommand
    {
        public Guid AccountId { get; set; }
        public Guid UserId { get; set; }
    }
}
