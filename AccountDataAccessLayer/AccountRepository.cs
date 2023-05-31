using Models;

namespace AccountDataAccessLayer
{
    /// <summary>
    /// A class which connect between the database and the services/controllers.
    /// </summary>
    public class AccountRepository : IAccountRepository
    {
        readonly AccountDbContext db;

        /// <summary>
        /// Constructor for the account repository, the context is injected via Dependency Injection.
        /// </summary>
        /// <param name="db">The db context.</param>
        public AccountRepository(AccountDbContext db)
            => this.db = db;


        /// <summary>
        /// Get the balance of an account.
        /// </summary>
        /// <param name="id">The account's id.</param>
        /// <returns>The balance of the account.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if there is no account with the given id.</exception>
        public Task<int> GetBalance(int id)
        {
            return Task.Run(() =>
            {
                var account = db.Accounts.Single(a => a.AccountId == id) ?? throw new KeyNotFoundException();
                return account.Balance;
            });
        }
        /// <summary>
        /// Creates new account.
        /// </summary>
        /// <returns>The new account Id.</returns>
        public Task<int> CreateAccount()
        {
            return Task.Run(() =>
            {
                var account = new Account();
                db.Accounts.Add(account);
                db.SaveChanges();

                // load the account data from the db.
                db.Entry(account).GetDatabaseValues();
                return account.AccountId;
            });
        }
        /// <summary>
        /// Updates the Balance of an account.
        /// </summary>
        /// <param name="id">The account Id which his balnce is updated.</param>
        /// <param name="amount">The amount of money to update.</param>
        /// <returns>Task which in the end will update the balance.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if there is no account with the given id.</exception>
        public Task UpdateBalance(int id, int amount)
        {
            return Task.Run(() =>
            {
                var account = db.Accounts.SingleOrDefault(a => a.AccountId == id) ?? throw new KeyNotFoundException();
                account.Balance += amount;
                db.SaveChanges();
            });
        }
    }
}
