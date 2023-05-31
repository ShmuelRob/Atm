using Models;

namespace TransactionDataAccessLayer
{
    /// <summary>
    /// An interface which connect between the database and the services/controllers.
    /// </summary>
    public interface ITransactionRepository
    {
        /// <summary>
        /// Deposit money in the Atm.
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <param name="bills">An array of all the bills, their values and amount.</param>
        /// <returns>Task which will return the new balance after the deposit.</returns>
        Task<int> Deposit(int accountId, Bill[] bills);
        /// <summary>
        /// Withdraw money to the Atm.
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <param name="amount">The amount of the money which the client take.</param>
        /// <returns>The balance after the withdraw.</returns>
        Task<int> Withdraw(int accountId, int amount);
    }
}
