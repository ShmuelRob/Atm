# ATM Project

This project represents an ATM. It has an options to make deposits and withdraws.
The ATM get his bills data from the DB (MS SQL Server), and get the accounts properties from the account microservice.
The account microservice manages all the accounts and their balances. this microservice is standalone.
The account microservice is injected to the transactions microservice via dependency Injection and depends on him.

Routes:
in the account microservice you have these:
- GET api/account/{id} - return the balance of an account 
- POST api/account - allow to make new account, return his id
- PUT api/account/{id} - allow to update the balance of an account

int the transaction microservice you have these:
- PUT api/transaction/deposit/{id} - make a deposit to the ATM, updating the account balance
- PUT api/transaction/withdraw/{id} - make a withdraw from the ATM, updating the account balnce


to run this project locally, you need:
- .NET version 6
- SQL Server 2019

Then, after downloading the project, to initialize the databases for the project
run these lines in the Package Manager Console or in the terminal:

`cd ./AccountDataAccessLayer`

`dotnet ef database update -s ../AccountApi`

`cd ../TransactionDataAccessLayer`

`dotnet ef database update -s ../TransactionApi`

Then
- if you are running in Visual Studio 2022, right click on the solution, 'configure startup project' and then choose AccountApi and TransactionApi,
Then run the code.
- else, you can run these 2 projects in 2 seperated terminals, run `dotnet run`

NOTE: do NOT use docker-compose yet do start the app, bacuse it wasn't get all the configurations needed.
