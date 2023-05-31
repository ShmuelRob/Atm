# ATM Project

This project represents an ATM. It has an options to make deposits and withdraws.
The ATM get his bills data from the DB (MS SQL Server), and get the accounts properties from the account microservice.
The account microservice is built in the project, and manages all the accounts and their balances.
The account microservice is injected to the transactions microservice via dependency Injection.

to run this project locally, you need:
- .NET version 6
- SQL Server 2019

Then, after downloading the project, to initialize the databases for the project
run these lines in the Package Manager Console or in the terminal:
`
cd ./AccountDataAccessLayer
dotnet ef database update -s ../AccountApi
cd ../TransactionDataAccessLayer
dotnet ef database update -s ../TransactionApi
`
Then
- if you are running in Visual Studio 2022, right click on the solution, 'configure startup project' and then choose AccountApi and TransactionApi
Then run the code.
- else, you can run these 2 projects in 2 seperated terminals, run `dotnet run`

