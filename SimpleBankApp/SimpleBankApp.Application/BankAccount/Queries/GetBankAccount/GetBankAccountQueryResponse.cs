﻿namespace SimpleBankApp.Application.BankAccount.Queries.GetBankAccount
{
    public class GetBankAccountQueryResponse
    {
        public Guid AccountId { get; set; }
        public decimal Balance { get; set; } = 0;
        public DateTime LastUpdatedOn { get; set; }
    }
}
