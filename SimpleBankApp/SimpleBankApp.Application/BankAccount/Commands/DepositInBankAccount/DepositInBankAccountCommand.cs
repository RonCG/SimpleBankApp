namespace SimpleBankApp.Application.BankAccount.Commands.DepositInBankAccount
{
    public class DepositInBankAccountCommand
    {
        public Guid UserId { get; set; }
        public Guid AccountId { get; set; }
        public decimal AmountToDeposit { get; set; } = 0;
    }
}
