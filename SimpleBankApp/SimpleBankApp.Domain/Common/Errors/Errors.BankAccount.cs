using ErrorOr;

namespace SimpleBankApp.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class BankAccount
        {
            public static Error BankAccountNotCreated => Error.Failure(code: "BankAccount.NotCreated", description: "There was an error while creating the bank account");
        }
    }
}
