namespace SimpleBankApp.Api.Contracts.BankAccount.DepositInBankAccount
{
    public class DepositInBankAccountRequest
    {
        public Guid AccountId { get; set; }
        public decimal AmountToDeposit { get; set; } = 0;
    }
}
