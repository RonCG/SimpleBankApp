using ErrorOr;

namespace SimpleBankApp.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class BankAccount
        {
            public static Error BankAccountNotCreated => Error.Failure(code: "BankAccount.NotCreated", description: "There was an error while creating the bank account");
            public static Error BankAccountNotUpdated => Error.Failure(code: "BankAccount.NotUpdated", description: "There was an error while updating the bank account");
            public static Error BankAccountNotFound => Error.NotFound(code: "BankAccount.NotFound", description: "Bank account does not exist");
            public static Error BankAccountInsufficientFunds => Error.Forbidden(code: "BankAccount.InsufficientFunds", description: "Can not withdraw money from bank account. Insufficient funds");
        }
    }
}
