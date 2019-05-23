using System;

namespace Moneybox.App
{
    public class Account
    {
        public const decimal PayInLimit = 4000m;

        public Guid Id { get; set; }

        public User User { get; set; }

        public decimal Balance { get; set; }

        public decimal Withdrawn { get; set; }

        public decimal PaidIn { get; set; }

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
    }
}
