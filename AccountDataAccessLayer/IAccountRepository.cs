namespace AccountDataAccessLayer
{
    /// <summary>
    /// An interface which connect between the database and the services/controllers.
    /// </summary>
    public interface IAccountRepository
    {
        /// <summary>
        /// Get the balance of an account.
        /// </summary>
        /// <param name="id">The account's id.</param>
        /// <returns>The balance of the account.</returns>
        Task<int> GetBalance(int id);
        /// <summary>
        /// Creates new account.
        /// </summary>
        /// <returns>The new account Id.</returns>
        Task<int> CreateAccount();
        /// <summary>
        /// Updates the Balance of an account.
        /// </summary>
        /// <param name="id">The account Id which his balnce is updated.</param>
        /// <param name="amount">The amount of money to update.</param>
        /// <returns>Task which in the end will update the balance.</returns>
        Task UpdateBalance(int id, int amount);
    }
}
