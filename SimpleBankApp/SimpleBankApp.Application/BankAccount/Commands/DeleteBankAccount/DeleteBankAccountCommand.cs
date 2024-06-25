namespace SimpleBankApp.Application.BankAccount.Commands.DeleteBankAccount
{
    public class DeleteBankAccountCommand
    {
        public Guid UserId { get; set; }
        public Guid AccountId { get; set; }
    }
}
