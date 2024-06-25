namespace SimpleBankApp.Api.Contracts.BankAccount.Requests
{
    public class CreateBankAccountResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal Balance { get; set; } = 0;
        public DateTime CreatedOn { get; set; }
    }
}
