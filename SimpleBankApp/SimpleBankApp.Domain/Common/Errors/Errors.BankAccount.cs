﻿using ErrorOr;

namespace SimpleBankApp.Domain.Common.Errors
{
    public static partial class Errors
    {
        public static class BankAccount
        {
            public static Error BankAccountNotCreated => Error.Failure(code: "BankAccount.NotCreated", description: "There was an error while creating the bank account");
            public static Error BankAccountNotUpdated => Error.Failure(code: "BankAccount.NotUpdated", description: "There was an error while updating the bank account");
            public static Error BankAccountNotDeleted => Error.Failure(code: "BankAccount.NotDeleted", description: "There was an error while deleting the bank account");
            public static Error BankAccountNotFound => Error.NotFound(code: "BankAccount.NotFound", description: "Bank account does not exist");
            public static Error InsufficientFundsForWithdraw => Error.Forbidden(code: "BankAccount.InsufficientFundsForWithdraw", description: "Can not withdraw money from bank account. Insufficient funds");
            public static Error CannotDeleteWithAvailableFunds => Error.Forbidden(code: "BankAccount.CannotDeleteWithAvailableFunds", description: "Can not delete the account since it still has available funds. Please withdraw the funds first");
        }
    }
}
