namespace SimpleBankApp.Application.BankAccount.Commands.CreateBankAccount
{
    public class CreateBankAccountCommandResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal Balance { get; set; } = 0;
        public DateTime CreatedOn { get; set; }
    }
}
