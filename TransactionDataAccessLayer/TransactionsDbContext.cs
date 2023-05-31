using Microsoft.EntityFrameworkCore;
using Models;

namespace TransactionDataAccessLayer
{
    /// <summary>
    /// Db Context for the ATM, for the Transaction microservice
    /// </summary>
    public class TransactionsDbContext : DbContext
    {
        // add the 'virtual' keyword to enable lazy loading
        /// <summary>
        /// represents the Bills table in the db
        /// </summary>
        public virtual DbSet<Bill> Bills { get; set; }


        /// <summary>
        /// initialize the Db context
        /// </summary>
        /// <param name="options"></param>
        public TransactionsDbContext(DbContextOptions<TransactionsDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // design the models for migrate to the db
            modelBuilder.Entity<Bill>().HasKey(k => k.Value);

            // seed the db with 5 bills of each value - you can delete 
            modelBuilder.Entity<Bill>().HasData(
                new Bill(20),
                new Bill(50),
                new Bill(100),
                new Bill(200));

            // let the db context class make his configuration
            base.OnModelCreating(modelBuilder);
        }
    }
}
