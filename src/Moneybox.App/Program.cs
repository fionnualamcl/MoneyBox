using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Concrete;
using Moneybox.App.Domain.Services;
using Moneybox.App.Domain.Validation;
using Moneybox.App.Features;
using Moneybox.App.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moneybox.App
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                decimal amount = 0;
                Guid fromAccountID;

                Console.WriteLine("Welcome to MoneyBox.");
                Console.WriteLine("Type 1 to transfer money");
                Console.WriteLine("Type 2 to withdraw money");

                string optionPicked = Console.ReadLine();

                IAccountRepository accountRepository = new AccountRepository();
                INotificationService notificationService = new NotificationService();
                IAccountService accountService = new AccountService(accountRepository);

                if (optionPicked.Equals("1"))
                {
                    fromAccountID = GetFromAccount();

                    var toAccountID = GetToAccount();

                    amount = GetAmount();

                    var transferMoney = new TransferMoney(accountRepository, notificationService, accountService);

                    var updatedAccount = transferMoney.Execute(fromAccountID, toAccountID, amount);

                    DisplayBalance(updatedAccount);
                }
                else if (optionPicked.Equals("2"))
                {
                    fromAccountID = GetFromAccount();

                    amount = GetAmount();

                    var withdrawMoney = new WithdrawMoney(accountRepository, notificationService, accountService);

                    var updatedAccount = withdrawMoney.Execute(fromAccountID, amount);

                    DisplayBalance(updatedAccount);
                }
                else
                {
                    Console.WriteLine("Sorry we were unable to process your request. Please try again or contact your system administrator.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.Read();
        }

        private static void DisplayBalance(Account updatedAccount)
        {
            Console.WriteLine("Account updated");
            Console.WriteLine("Your new balance is:" + updatedAccount.Balance);
        }

        private static decimal GetAmount()
        {
            Console.WriteLine("Amount:");
            string amountToProcess = Console.ReadLine();
            Decimal.TryParse(amountToProcess, out decimal amount);

            if (amount <= TotalsHelper.ZeroBalance)
            {
                throw new InvalidOperationException("Sorry we were unable to process your request. Please try again or contact your system administrator.");
            }

            return amount;
        }

        private static Guid GetFromAccount()
        {
            Console.WriteLine("From account number:");
            string accountID = Console.ReadLine();
            Guid.TryParse(accountID, out Guid fromAccountID);
            if (fromAccountID == Guid.Empty)
            {
                throw new InvalidOperationException("Invalid Account Id");
            }

            return fromAccountID;
        }

        private static Guid GetToAccount()
        {
            Console.WriteLine("To account number:");
            string accountID = Console.ReadLine();
            Guid.TryParse(accountID, out Guid toAccountID);
            if (toAccountID == Guid.Empty)
            {
                throw new InvalidOperationException("Invalid Account Id");
            }

            return toAccountID;
        }

    }
}
