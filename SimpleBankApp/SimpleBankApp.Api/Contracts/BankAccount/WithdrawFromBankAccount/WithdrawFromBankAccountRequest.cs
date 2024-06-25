namespace SimpleBankApp.Api.Contracts.BankAccount.WithdrawFromBankAccount
{
    public class WithdrawFromBankAccountRequest
    {
        public Guid AccountId { get; set; }
        public decimal AmountToWithdraw{ get; set; } = 0;
    }
}
