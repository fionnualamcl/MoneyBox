using Moneybox.App;
using Moneybox.App.DataAccess;
using Moneybox.App.Infrastructure;
using NUnit.Framework;
using System.Linq;

namespace AccountTests
{
    public class AccountTests
    {
        [Test]
        public void CreateUserAccount_Success()
        {
            User user = new User
            {
                Id = new System.Guid(),
                Email = "test1@email.com",
                IsDisabled = false
            };

            Account account = new Account();
            account.AddUser(user);

            Assert.AreNotEqual(0, user.Id);
        }

        public void CreateMultipleUsersWithOneAccount_Success()
        {
            User user = new User
            {
                Id = new System.Guid(),
                Name = "Anna Dunne",
                Email = "Anna@Email.com",
                IsDisabled = false
            };

            Account account = new Account()
            {
                Id = new System.Guid(),
                Balance = 1000,
                Withdrawn = 0,
                PaidIn = 0,
                IsFrozen = false,
                IsDisabled = false
            };
            account.AddUser(user);

            user = new User
            {
                Id = new System.Guid(),
                Name = "John Dunne",
                Email = "john@email.com",
                IsDisabled = false
            };

            Assert.GreaterOrEqual(account.Users.Count(), 2);
        }

        [Test]
        public void GetAccountById_Success()
        {
            var user = new User
            {
                Id = new System.Guid(),
                Name = "Anna Dunne",
                Email = "Anna@Email.com"
            };

            IAccountRepository _accountRepository = new AccountRepository();
            var from = _accountRepository.GetAccountById(user.Id);

            Assert.IsNotNull(from);
        }

        [Test]
        public void IsBelowMinimumBalance_True()
        {
            var user = new User
            {
                Id = new System.Guid(),
                Name = "Anna Dunne",
                Email = "Anna@Email.com"
            };

            Account account = new Account()
            {
                Id = new System.Guid(),
                Balance = 1000,
                Withdrawn = 0,
                PaidIn = 0,
                IsFrozen = false,
                IsDisabled = false
            };

            account.AddUser(user);

            var amount = 9000;

            var balanceIsExceeded = account.IsBelowMinimumBalance(amount);

            Assert.IsTrue(balanceIsExceeded);
        }

        [Test]
        public void IsBalanceLessThanZero_True()
        {
            var user = new User
            {
                Id = new System.Guid(),
                Name = "Anna Dunne",
                Email = "Anna@Email.com"
            };

            Account account = new Account()
            {
                Id = new System.Guid(),
                Balance = 1000,
                Withdrawn = 0,
                PaidIn = 0,
                IsFrozen = false,
                IsDisabled = false
            };

            account.AddUser(user);

            var amount = 6000;

            var balanceIsZero = account.IsBalanceLessThanZero(amount);

            Assert.IsTrue(balanceIsZero);
        }

        [Test]
        public void CalculateWithdrawBalance_Success()
        {
            var user = new User
            {
                Id = new System.Guid(),
                Name = "Anna Dunne",
                Email = "Anna@Email.com"
            };

            Account account = new Account()
            {
                Id = new System.Guid(),
                Balance = 1000,
                Withdrawn = 0,
                PaidIn = 0,
                IsFrozen = false,
                IsDisabled = false
            };

            account.AddUser(user);

            var amount = 100m;

            account.CalculateWithdrawBalance(amount);

            Assert.Less(account.Balance, (account.Balance + amount));
        }

        [Test]
        public void IsPaidInLessThanMinimumPayInLimit_False()
        {
            var user = new User
            {
                Id = new System.Guid(),
                Name = "Anna Dunne",
                Email = "Anna@Email.com"
            };

            Account account = new Account()
            {
                Id = new System.Guid(),
                Balance = 1000,
                Withdrawn = 0,
                PaidIn = 0,
                IsFrozen = false,
                IsDisabled = false
            };

            account.AddUser(user);

            var amount = 1000;

            var underPayInLimitReached = account.IsPaidInLessThanMinimumPayInLimit(amount);

            Assert.IsFalse(underPayInLimitReached);
        }

        [Test]
        public void IsPaidInGreaterThanPayInLimit_False()
        {
            var user = new User
            {
                Id = new System.Guid(),
                Name = "Anna Dunne",
                Email = "Anna@Email.com"
            };

            Account account = new Account()
            {
                Id = new System.Guid(),
                Balance = 1000,
                Withdrawn = 0,
                PaidIn = 0,
                IsFrozen = false,
                IsDisabled = false
            };

            account.AddUser(user);

            var amount = 10;

            var paidInLimitExceeded = account.IsPaidInGreaterThanPayInLimit(amount);

            Assert.IsFalse(paidInLimitExceeded);
        }

        [Test]
        public void CalculatePayInBalance_Success()
        {
            var user = new User
            {
                Id = new System.Guid(),
                Name = "Anna Dunne",
                Email = "Anna@Email.com"
            };

            Account account = new Account()
            {
                Id = new System.Guid(),
                Balance = 1000,
                Withdrawn = 0,
                PaidIn = 0,
                IsFrozen = false,
                IsDisabled = false
            };

            account.AddUser(user);

            var amount = 5000m;

            account.CalculatePayInBalance(amount);

            Assert.GreaterOrEqual(account.Balance, (account.PaidIn));
        }

    }
}