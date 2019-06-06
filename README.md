# Moneybox Money Withdrawal


The solution contains a .NET core library (Moneybox.App) which is structured into the following 3 folders:



* Domain - this contains the domain models for a user, address, bank, and an account, interfaces and concrete classes for a notification service.	  
* Features - this contains two operations, transfer money and withdraw money.
* DataAccess - this contains a repository for retrieving and saving an account.
* Infrastructure - this contains a list of defined total limits
* Program - this contains the main program to execute the console program.
* NUnit - contains some unit tests



## The Implementation
* Implemented a console app whereby a user can choose option 1 to transfer money, or option 2 to withdraw money.
* Refactored code that was in the features TransferMoney and WithdrawMoney classes.
* Injected objects into the features classes that are created as singletons.
* Added validation methods to check that amount being transfered or withdrawn is within ranges and valid.

### Recommendations for further development:
* Add Login UI, authentication, and security checks
* Add MVC pattern to have web screen with UI for user to enter their data, submit and display their balance.
* Add email engine
* Add database such as SQL Server.
