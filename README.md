# Moneybox Money Withdrawal


The solution contains a .NET core library (Moneybox.App) which is structured into the following 3 folders:


* Domain - this contains the domain models for a user and an account, and a notification service.

* Features - this contains two operations, one which is implemented (transfer money) and another which isn't (withdraw money)

* DataAccess - this contains a repository for retrieving and saving an account (and the nested user it belongs to)



## The task


The task is to implement a money withdrawal in the WithdrawMoney.Execute(...) method in the features folder. 
For consistency, the logic should be the same as the TransferMoney.Execute(...) method i.e. 
notifications for low funds 
and exceptions where the operation is not possible. 



As part of this process however, you should look to refactor some of the code in the TransferMoney.Execute(...) method into the domain models, 
and make these models less susceptible to misuse. 

We're looking to make our domain models rich in behaviour 
and much more than just plain old objects, however we don't want any data persistance operations (i.e. data access repositories) to bleed into our domain. 

This should simplify the task of implementing WithdrawMoney.Execute(...).



## Guidelines


* You should spend no more than 1 hour on this task, although there is no time limit

* You should fork or copy this repository into your own public repository (Github, BitBucket etc.) before you do your work

* Your solution must compile and run first time

* You should not alter the notification service or the the account repository interfaces

* You may add unit/integration tests using a test framework (and/or mocking framework) of your choice

* You may edit this README.md if you want to give more details around your work (e.g. why you have done something a particular way, 
or anything else you would look to do but didn't have time)


Once you have completed your work, send us a link to your public repository.

Good luck!


//santizing input
//namespace
//protected class
//iaccountservice or imoneyboxservice: withdraw(from) and transfer money(from,to), in exec u call service methods: Balance()
//add comments
//update readme

martin fowler: domain modal : never put business logic into domain model, put it into service layer or reposit

this is the order:
data
domain
service layer will have relationship with respos. and put bal, logic in here
service layer mediates comms between controller and repository layer. service layer contains business logic, and validation logic

controllers respon for app flow control logic


public class MyEntity
{
    private int someProperty = 0;

    public int SomeProperty
    {
        get { return this.someProperty; }
        set
        {
            string message;
            if(!TryValidateSomeProperty(value, out message)) 
            {
                throw new ArgumentOutOfRangeException("value", message);
            }
            this.someProperty = value;
        }
    }

    public static bool TryValidateSomeProperty(int value, out string message)
    {
        if(value < 0)
        {
            message = "Some Property cannot be negative.";
            return false;
        }
        message = string.Empty;
        return true;
    }
}
https://www.martinfowler.com/bliki/AnemicDomainModel.html
https://softwareengineering.stackexchange.com/questions/218011/how-accurate-is-business-logic-should-be-in-a-service-not-in-a-model
https://softwareengineering.stackexchange.com/questions/218011/how-accurate-is-business-logic-should-be-in-a-service-not-in-a-model/218394#218394