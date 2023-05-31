using Microsoft.EntityFrameworkCore;
using Models;

namespace AccountDataAccessLayer
{
    /// <summary>
    /// Db Context for the ATM, for the Account microservice.
    /// </summary>
    public class AccountDbContext: DbContext
    {
        // add the 'virtual' keyword to enable lazy loading
        /// <summary>
        /// Represents the Accounts table in the db.
        /// </summary>
        public virtual DbSet<Account> Accounts { get; set; }


        /// <summary>
        /// Initialize the Db context.
        /// </summary>
        /// <param name="options">Db context options, go as is to the bas class</param>
        public AccountDbContext(DbContextOptions<AccountDbContext> options)
            : base(options) { }
    }
}
