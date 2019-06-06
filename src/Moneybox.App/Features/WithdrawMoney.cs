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

        public WithdrawMoney(IAccountRepository accountRepository, INotificationService notificationService)
        {
            this._accountRepository = accountRepository;
            this._notificationService = notificationService;
        }

        public Account Execute(Guid fromAccountId, decimal amount)
        {
            var from = this._accountRepository.GetAccountById(fromAccountId);
            if (from == null)
            {
                throw new InvalidOperationException("Account:" + fromAccountId.ToString() + " was not found. Please contact your system administrator.");
            }

            if (!PerformValidation(amount, from)) {
                throw new InvalidOperationException("Insuffient funds.");
            }

            from.CalculateWithdrawBalance(amount);

            _accountRepository.Update(from);

            return from;
        }

        private Boolean PerformValidation(decimal amount, Account from)
        {
            if (amount < 0m)
            {
                throw new InvalidOperationException("The amount entered(" + amount + ") is a negative number");
            }

            var balanceIsExceeded = from.IsBelowMinimumBalance(amount);
            if (balanceIsExceeded)
            {
                throw new InvalidOperationException("Insufficient funds to make withdraw");
            }

            var balanceIsZero = from.IsBalanceLessThanZero(amount);
            if (balanceIsZero)
            {
                foreach (var user in from.Users)
                {
                    this._notificationService.NotifyFundsLow(user.Email);
                }
                return false;
            }

            if (from.IsFrozen)
            {
                throw new InvalidOperationException("Account is frozen");
            }

            return true;
        }
    }
}
