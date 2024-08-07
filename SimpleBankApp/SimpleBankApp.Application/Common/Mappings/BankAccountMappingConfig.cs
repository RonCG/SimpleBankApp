﻿using Mapster;
using SimpleBankApp.Application.BankAccount.Commands.CreateBankAccount;
using SimpleBankApp.Application.BankAccount.Commands.DepositInBankAccount;
using SimpleBankApp.Application.BankAccount.Commands.WithdrawFromBankAccount;
using SimpleBankApp.Application.BankAccount.Queries.GetBankAccount;
using SimpleBankApp.Domain.Entities;

namespace SimpleBankApp.Application.Common.Mappings
{
    public class BankAccountMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<BankAccountEntity, CreateBankAccountCommandResponse>();
            config.NewConfig<BankAccountEntity, DepositInBankAccountCommandResponse>()
                .Map(dest => dest.AccountId, src => src.Id);
            config.NewConfig<BankAccountEntity, WithdrawFromBankAccountCommandResponse>()
                .Map(dest => dest.AccountId, src => src.Id);
            config.NewConfig<BankAccountEntity, GetBankAccountQueryResponse>()
                .Map(dest => dest.AccountId, src => src.Id);
        }
    }
}
