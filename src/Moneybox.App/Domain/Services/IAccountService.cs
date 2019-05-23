using System;
using System.Collections.Generic;
using System.Text;

namespace Moneybox.App.Domain.Services
{
    public interface IAccountService
    {
        Account CalculateWithdrawBalance(Account account, decimal amount);
        Account CalculatePayInBalance(Account account, decimal amount);
    }
}
