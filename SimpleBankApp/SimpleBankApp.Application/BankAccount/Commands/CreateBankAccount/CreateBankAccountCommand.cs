namespace SimpleBankApp.Application.BankAccount.Commands.CreateBankAccount
{
    public class CreateBankAccountCommand
    {
        public Guid UserId { get; set; }
        public decimal Balance { get; set; } = 0;
    }
}
