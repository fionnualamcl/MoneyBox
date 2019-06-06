using Moneybox.App.Domain;
using Moneybox.App.Infrastructure;
using System;

namespace Moneybox.App.DataAccess
{
    public class AccountRepository : IAccountRepository
    {
        public Account GetAccountById(Guid accountId)
        {
            var account = new Account { Id = accountId,
                                        Balance = 10000m,
                                        PaidIn = 0,
                                        Withdrawn = 0,
                                        IsFrozen =false,
                                        IsDisabled =false};

            account.AddUser(new User
            {
                Id = new System.Guid(),
                Email = "test@email.com",
                Name = "First Last Name",
                Address = new Address("Address Line1",
                                        "Address Line2",
                                        "Address Line3",
                                        "City",
                                        "Region",
                                        "Country",
                                        "ABC DEF"),
                IsDisabled = false
            });

            return account;
        }

        public void Update(Account account)
        {
             var accountToEdit = this.GetAccountById(account.Id);
            if (accountToEdit == null)
            {
                throw new InvalidOperationException("Invalid account");
            }

            accountToEdit.PaidIn = account.PaidIn;
            accountToEdit.Withdrawn = account.Withdrawn;
            accountToEdit.Balance = account.Balance;
            // Commit to database
        }
    }
}
