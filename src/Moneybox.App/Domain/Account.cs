using Moneybox.App.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Moneybox.App
{
    public class Account
    {
        // A user can have multiple accounts e.g: user can have savings account, current account, credit card, etc
        // An account can have multiple users, e.g. a savings account might belong to a couple. 
        // Account types: 1. Current, 2. Savings, 3. Credit card
        private readonly List<User> _users = new List<User>();

        public const decimal PayInLimit = 4000m;

        public Guid Id { get; set; }

        //public User User { get; set; }
        public IEnumerable<User> Users {get { return _users; } }

        public decimal Balance { get; set; }

        public decimal Withdrawn { get; set; }

        public decimal PaidIn { get; set; }

        public EnumAccountType AccountType { get; set; }

        public Boolean IsFrozen { get; set; }

        public Boolean IsDisabled { get; set; }

        public void AddUser(User user)
        {
            this._users.Add(user);
        }

        public Boolean DisableUser(User user)
        {
            var resultUser = this.Users.Where(u =>u.Id == user.Id).ToList();
            if (resultUser.Any())
            {
                resultUser.FirstOrDefault().IsDisabled = true;
                return true;
            }

            return false;
        }

        public List<User> GetUserList(User user)
        {
            return Users.Where(u => !u.IsDisabled).ToList();
        }

        public Boolean FreezeAccount()
        {
            return this.IsFrozen = true;
        }

        public Boolean IsAccountFrozen()
        {
            return this.IsFrozen;
        }

        public Boolean DisableAccount()
        {
              return this.IsDisabled = true;
        }
        public Boolean IsAccountDisabled()
        {
            return this.IsDisabled;
        }

        public void CalculatePayInBalance(decimal amount)
        {
            if (Decimal.MaxValue < amount || Decimal.MaxValue < this.Balance)
            {
                throw new OverflowException();
            }

            this.Balance = this.Balance + amount;
            this.PaidIn = this.PaidIn + amount;
        }

        public void CalculateWithdrawBalance(decimal amount)
        {
            if (Decimal.MaxValue < amount || Decimal.MaxValue < this.Balance)
            {
                throw new OverflowException();
            }

            this.Balance = this.Balance - amount;
            this.Withdrawn = this.Withdrawn - amount;
        }

        #region Withdrawing Money From Account
        public bool IsBalanceLessThanZero(decimal amount)
        {
            var newBalance = this.Balance - amount;
            var isBalanceLessThanZero = newBalance < TotalsHelper.ZeroBalance;

            return isBalanceLessThanZero;
        }

        public bool IsBelowMinimumBalance(decimal amount)
        {
            var newBalance = this.Balance - amount;
            var result = newBalance < TotalsHelper.MinimumBalance;
            return result;
        }
        #endregion

        #region Transfer Money To Account
        public bool IsPaidInGreaterThanPayInLimit(decimal amount)
        {
            var paidIn = this.PaidIn + amount;
            var result = paidIn > Account.PayInLimit;

            return result;
        }

        public bool IsPaidInLessThanMinimumPayInLimit(decimal amount)
        {
            var paidIn = this.PaidIn + amount;
            var result = (Account.PayInLimit - this.PaidIn) < TotalsHelper.minPayInLimit;
            return result;
        }
        #endregion
    }


}
