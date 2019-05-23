using Moneybox.App.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moneybox.App.Domain.Concrete
{
    public class NotificationService : INotificationService
    {
        public void NotifyApproachingPayInLimit(string emailAddress)
        {
            Console.WriteLine("Emailed customer");
        }

        public void NotifyFundsLow(string emailAddress)
        {
            Console.WriteLine("Emailed customer");
        }
    }
}
