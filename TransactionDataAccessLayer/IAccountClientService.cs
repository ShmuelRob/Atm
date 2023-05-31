namespace TransactionDataAccessLayer
{
    /// <summary>
    /// An interface for the HTTP client service
    /// </summary>
    public interface IAccountClientService
    {
        /// <summary>
        /// Updates the balance of an account.
        /// </summary>
        /// <param name="accountId">The account's id.</param>
        /// <param name="amount">The amount of money to update.</param>
        /// <returns>Task which will return the new Balance of the account.</returns>
        Task<int> UpdateBalance(int accountId, int amount);
    }
}
