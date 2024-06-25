namespace SimpleBankApp.Application.BankAccount.Commands.DepositInBankAccount
{
    public class DepositInBankAccountCommandResponse
    {
        public Guid AccountId { get; set; }
        public decimal Balance { get; set; } = 0;
        public DateTime LastUpdatedOn { get; set; }
    }
}
