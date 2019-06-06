using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using System;
using System.Linq;

namespace Moneybox.App.Features
{
    public class TransferMoney
    {
        private IAccountRepository _accountRepository; 
        private INotificationService _notificationService;

        public TransferMoney(IAccountRepository accountRepository, INotificationService notificationService)
        {
            this._accountRepository = accountRepository;
            this._notificationService = notificationService;
        }

        public Account Execute(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            var from = this._accountRepository.GetAccountById(fromAccountId);
            if (from == null)
            {
                throw new InvalidOperationException("Account:" + fromAccountId.ToString() + " was not found. Please contact your system administrator.");
            }

            var to = this._accountRepository.GetAccountById(toAccountId);
            if (to == null)
            {
                throw new InvalidOperationException("Account:" + toAccountId.ToString() + " was not found. Please contact your system administrator.");
            }

            if (!PerformValidation(amount, from, to)) {
                throw new InvalidOperationException("Insuffient funds.");
            }

            from.CalculateWithdrawBalance(amount);

            to.CalculatePayInBalance(amount);

            _accountRepository.Update(from);

            _accountRepository.Update(to);

            return from;
        }

        private Boolean PerformValidation(decimal amount, Account from, Account to)
        {
            if (from.Users.Count() == 0)
            {
                throw new InvalidOperationException("No account holder found for this account.");
            }

            if (amount < 0m)
            {
                throw new InvalidOperationException("The amount entered(" + amount + ") is a negative number");
            }

            var balanceIsExceeded = from.IsBelowMinimumBalance(amount);
            if (balanceIsExceeded)
            {
                throw new InvalidOperationException("Insufficient funds to make transfer");
            }

            var balanceIsZero = from.IsBalanceLessThanZero(amount);
            if (balanceIsExceeded)
            {
                foreach (var user in from.Users)
                {
                    this._notificationService.NotifyFundsLow(user.Email);
                }
               
                return false;
            }

            var underPayInLimitReached = to.IsPaidInLessThanMinimumPayInLimit(amount);
            if (underPayInLimitReached)
            {
                foreach (var user in to.Users)
                {
                    this._notificationService.NotifyApproachingPayInLimit(user.Email);
                }
                return false;
            }

            var paidInLimitExceeded = to.IsPaidInGreaterThanPayInLimit(amount);
            if (paidInLimitExceeded)
            {
                throw new InvalidOperationException("Account pay in limit reached");
            }

            if (from.IsFrozen || to.IsFrozen)
            {
                throw new InvalidOperationException("Account is frozen");
            }

            return true;
        }
    }
}
