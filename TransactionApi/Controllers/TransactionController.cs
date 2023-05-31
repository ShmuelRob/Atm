using Microsoft.AspNetCore.Mvc;
using Models;
using TransactionDataAccessLayer;

namespace TransactionApi.Controllers
{
    /// <summary>
    /// Controller of the Transaction microservice.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        readonly ITransactionRepository transactionRepository;


        /// <summary>
        /// The constructor of the controller, implements Dependency Injection.
        /// </summary>
        /// <param name="transactionRepository"></param>
        public TransactionController(ITransactionRepository transactionRepository)
            => this.transactionRepository = transactionRepository;


        /// <summary>
        /// In this route you can make a deposit.
        /// </summary>
        /// <param name="id">The account's id.</param>
        /// <param name="bills">An array of the bills that the client inserted to the ATM.</param>
        /// <returns>The account's new balance.</returns>
        [HttpPut("deposit/{id}")]
        public async Task<IActionResult> Deposit(int id, [FromBody] Bill[] bills)
        {
            var isAnyAmointOfBillNegetive = bills.Any(b => b.Amount < 0);
            if (isAnyAmointOfBillNegetive)
            {
                return BadRequest("the bills must be positive integers in a deposit");
            }
            int balanceAfterAction = await transactionRepository.Deposit(id, bills);
            return Ok(balanceAfterAction);
        }
        /// <summary>
        /// In this route you can make a withdraw.
        /// </summary>
        /// <param name="id">The account's id.param>
        /// <param name="amount">The mount of the money which the client requested.</param>
        /// <returns>The account's new balance.</returns>
        [HttpPut("withdraw/{id}")]
        public async Task<IActionResult> Withdraw(int id, [FromBody] int amount)
        {
            int balanceAfterAction = await transactionRepository.Withdraw(id, amount);
            return Ok(balanceAfterAction);
        }
    }
}
