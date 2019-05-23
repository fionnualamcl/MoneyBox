using System;
using System.Collections.Generic;
using System.Text;

namespace Moneybox.App.DataAccess
{
    public class AccountRepository : IAccountRepository
    {
        public Account GetAccountById(Guid accountId)
        {
            return new Account { Id = accountId, Balance = 1000000m };
        }

        public void RollBackTransaction(Account account)
        {
            Console.WriteLine("Rollback performed");
        }

        public void Update(Account account)
        {
            // If there was a database, then use transaction scope to put it into a transaction and save to DB.
            var accountToEdit = this.GetAccountById(account.Id);
            accountToEdit.PaidIn = account.PaidIn;
            accountToEdit.Withdrawn = account.Withdrawn;
            accountToEdit.Balance = account.Balance;
        }
    }
}
