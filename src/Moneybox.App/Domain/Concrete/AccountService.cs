using System;
using System.Collections.Generic;
using System.Text;
using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;

namespace Moneybox.App.Domain.Concrete
{
    public class AccountService:IAccountService
    {
        private IAccountRepository accountRepository = null;

        public AccountService(IAccountRepository repository)
        {
            this.accountRepository = repository;
        }

        public Account CalculateWithdrawBalance(Account account, decimal amount)
        {
            var accountToEdit = this.accountRepository.GetAccountById(account.Id);
            accountToEdit.CalculateWithdrawBalance(amount);
            return accountToEdit;
        }

        public Account CalculatePayInBalance(Account account, decimal amount)
        {
            var accountToEdit = this.accountRepository.GetAccountById(account.Id);
            accountToEdit.CalculatePayInBalance(amount);
            return accountToEdit;
        }
    }
}
