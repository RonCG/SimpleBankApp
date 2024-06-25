namespace SimpleBankApp.Application.BankAccount.Commands.WithdrawFromBankAccount
{
    public class WithdrawFromBankAccountCommandResponse
    {
        public Guid AccountId { get; set; }
        public decimal Balance { get; set; } = 0;
        public DateTime LastUpdatedOn { get; set; }
    }
}
