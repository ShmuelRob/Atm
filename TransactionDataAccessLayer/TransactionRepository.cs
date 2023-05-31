using Models;
using Models.Exceptions;

namespace TransactionDataAccessLayer
{
    /// <summary>
    /// A class which connect between the database and the services/controllers.
    /// </summary>
    public class TransactionRepository : ITransactionRepository
    {
        readonly TransactionsDbContext db;
        readonly IAccountClientService httpClient;


        /// <summary>
        /// Constructor for the transaction repository, the context is injected via Dependency Injection.
        /// </summary>
        /// <param name="db">The db contet.</param>
        /// <param name="httpClient">The account microservice.</param>
        public TransactionRepository(TransactionsDbContext db, IAccountClientService httpClient)
        {
            this.db = db;
            this.httpClient = httpClient;
        }


        /// <summary>
        /// Deposit money in the Atm.
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <param name="billsFromClient">An array of all the bills, their values and amount.</param>
        /// <returns>Task which will return the new balance after the deposit.</returns>
        public async Task<int> Deposit(int accountId, Bill[] billsFromClient)
        {
            // start new transaction
            using var transaction = db.Database.BeginTransaction();
            try
            {
                var amount = DbInternalServices.CalculateMoney(billsFromClient);

                // add the bills to the atm
                billsFromClient.ToList().ForEach(billFromClient =>
                {
                    var billInAtm = db.Bills.FirstOrDefault(b => b.Value == billFromClient.Value);
                    if (billInAtm is not null)
                    {
                        billInAtm.Amount += billFromClient.Amount;
                    }
                });

                // commit the changes
                db.SaveChanges();
                transaction.Commit();
                return await httpClient.UpdateBalance(accountId, amount);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Withdraw money to the Atm.
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <param name="amount">The amount of the money which the client take.</param>
        /// <returns>The balance after the withdraw.</returns>
        /// <exception cref="MoneyNotFitException">Thrown if there is money in the ATM, but cannot make a combination of the bills to the exact amount.</exception>
        public async Task<int> Withdraw(int accountId, int amount)
        {
            try
            {
                var isWithdrawHappened = DbInternalServices.WithdrawFromAtm(db, amount);

                if (isWithdrawHappened)
                {
                    // in withdraw the account take money and his balance goes down
                    int newAmount = 0 - amount;
                    return await httpClient.UpdateBalance(accountId, 0 - amount);
                }
                throw new MoneyNotFitException();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
