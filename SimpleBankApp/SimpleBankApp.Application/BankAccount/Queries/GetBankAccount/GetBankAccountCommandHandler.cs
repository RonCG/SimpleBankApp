﻿using ErrorOr;
using MapsterMapper;
using SimpleBankApp.Application.BankAccount.Commands.WithdrawFromBankAccount;
using SimpleBankApp.Application.Common.Interfaces.Persistance;
using SimpleBankApp.Domain.Common.Errors;
using SimpleBankApp.Domain.Entities;

namespace SimpleBankApp.Application.BankAccount.Queries.GetBankAccount
{
    public class GetBankAccountCommandHandler : IGetBankAccountCommandHandler
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IMapper _mapper;

        public GetBankAccountCommandHandler(
            IBankAccountRepository bankAccountRepository, 
            IMapper mapper)
        {
            _bankAccountRepository = bankAccountRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<GetBankAccountCommandResponse>> Handle(GetBankAccountCommand command)
        {
            var bankAccount = await _bankAccountRepository.GetBankAccount(command.AccountId, command.UserId);
            if (bankAccount == null)
            {
                return Errors.BankAccount.BankAccountNotFound;
            }

            return _mapper.Map<GetBankAccountCommandResponse>(bankAccount);
        }
    }

    public interface IGetBankAccountCommandHandler
    {
        Task<ErrorOr<GetBankAccountCommandResponse>> Handle(GetBankAccountCommand command);
    }
}