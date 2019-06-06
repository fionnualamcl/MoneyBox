using Moneybox.App.Domain.Services;
using Moneybox.App.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moneybox.App.Domain.Validation
{
   public class Validator
    {
    //    #region Withdrawing Money From Account
    //    public bool IsBalanceLessThanZero(Account account, decimal amount)
    //    {
    //        var newBalance = account.Balance - amount;
    //        var isBalanceLessThanZero = newBalance < TotalsHelper.ZeroBalance;

    //        return isBalanceLessThanZero;
    //    }

    //    public bool IsBelowMinimumBalance(Account account, decimal amount)
    //    {
    //        var newBalance = account.Balance - amount;
    //        var result = newBalance < TotalsHelper.MinimumBalance;
    //        return result;
    //    }
    //    #endregion

    //    #region Transfer Money To Account
    //    public bool IsPaidInGreaterThanPayInLimit(Account account, decimal amount)
    //    {
    //        var paidIn = account.PaidIn + amount;
    //        var result = paidIn > Account.PayInLimit;

    //        return result; 
    //    }

    //    public bool IsPaidInLessThanMinimumPayInLimit(Account account, decimal amount)
    //    {
    //        var paidIn = account.PaidIn + amount;
    //        var result = (Account.PayInLimit - account.PaidIn) < TotalsHelper.minPayInLimit;
    //        return result;
    //    }
    //    #endregion
   }
}
