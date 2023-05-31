using Microsoft.AspNetCore.Mvc;

namespace AccountApi.Controllers
{
    /// <summary>
    /// Controller of the Account microservice.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        readonly IAccountRepository accountRepository;


        /// <summary>
        /// The constructor of the controller, implements Dependency Injection.
        /// </summary>
        /// <param name="accountRepository">The repository, will be ingected via DI</param>
        public AccountsController(IAccountRepository accountRepository)
            => this.accountRepository = accountRepository;


        /// <summary>
        /// In this route you can get the balance of an account.
        /// </summary>
        /// <param name="id">The id of the account.</param>
        /// <returns>A task with the balnace.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBalance(int id)
        {
            try
            {
                var balance = await accountRepository.GetBalance(id);
                return Ok(balance);
            }
            catch (KeyNotFoundException ex)
            {
                // in case where the id is not exist
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // in every other case
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// In this route you can update the balance of an account.
        /// </summary>
        /// <param name="id">The id of the account.</param>
        /// <param name="amount">The amount of money to give or take from the account. Negetive means take money.</param>
        /// <returns>Task with the new Balance.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBalance(int id, [FromBody] int amount)
        {
            try
            {
                await accountRepository.UpdateBalance(id, amount);
                var newBalance = accountRepository.GetBalance(id);
                return Ok(newBalance);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// In this route you can create new account.
        /// </summary>
        /// <returns>The new account id.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAccount()
        {
            try
            {
                var newAccountId = await accountRepository.CreateAccount();
                return Ok(newAccountId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
