using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using Moneybox.App.Domain.Validation;
using System;

namespace Moneybox.App.Features
{
    public class WithdrawMoney
    {
        private IAccountRepository _accountRepository;
        private INotificationService _notificationService;
        private IAccountService _accountService;

        private Validator _validator = new Validator();

        public WithdrawMoney(IAccountRepository accountRepository, INotificationService notificationService, IAccountService accountService)
        {
            this._accountRepository = accountRepository;
            this._notificationService = notificationService;
            this._accountService = accountService; 
        }

        public Account Execute(Guid fromAccountId, decimal amount)
        {
            var from = this._accountRepository.GetAccountById(fromAccountId);
            if (from == null)
            {
                throw new InvalidOperationException("Account:" + fromAccountId.ToString() + " was not found. Please contact your system administrator.");
            }

            if (amount < 0m)
            {
                throw new InvalidOperationException("The amount entered(" + amount + ") is a negative number");
            }

            var balanceIsExceeded = this._validator.IsBelowMinimumBalance(from, amount);
            if (balanceIsExceeded)
            {
                throw new InvalidOperationException("Insufficient funds to make withdraw");
            }

            var balanceIsZero = this._validator.IsBalanceLessThanZero(from, amount);
            if (balanceIsZero)
            {
                this._notificationService.NotifyFundsLow(from.User.Email);
            }

            from = this._accountService.CalculateWithdrawBalance(from, amount);

            // If any of the below fails then rollback both transactions
            // Ideally use transaction scope to put it into transaction and then let it handle rollback or committing 
            // of record into DB if it was connected to a database.
            try
            {
                this._accountRepository.Update(from);
            }
            catch (Exception)
            {
                this._accountRepository.RollBackTransaction(from);

                throw;
            }

            return from;
        }
    }
}
