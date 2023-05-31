using System.Text.Json;
using TransactionDataAccessLayer;

namespace TransactionApi.Services
{
    /// <summary>
    /// A class for the HTTP client service
    /// </summary>
    public class AccountClientService : IAccountClientService
    {
        readonly HttpClient httpClient;


        /// <summary>
        /// Constructor for the HTTP client service.
        /// </summary>
        /// <param name="clientFacroty">client factory, will be injected via Dependency Injection.</param>
        public AccountClientService(IHttpClientFactory clientFacroty)
        {
            httpClient = clientFacroty.CreateClient();
        }


        /// <summary>
        /// Updates the balance of an account.
        /// </summary>
        /// <param name="accountId">The account's id.</param>
        /// <param name="amount">The amount of money to update.</param>
        /// <returns>Task which will return the new Balance of the account.</returns>
        /// <exception cref="Exception">Thrown if there's a problem in the HTTP request.</exception>
        public async Task<int> UpdateBalance(int accountId, int amount)
        {
            return await Task.Run(async () =>
            {

                var response = await httpClient.PutAsync($"http://localhost:5195/api/account/{accountId}", JsonContent.Create(amount));
                if (response.IsSuccessStatusCode)
                {
                    // parse the response
                    var contentAsString = await response.Content.ReadAsStringAsync();
                    var contentAsObject = JsonSerializer.Deserialize<HasResult>(contentAsString);

                    // return the new balance or an exception.
                    return contentAsObject is null
                    ? throw new Exception(response.ReasonPhrase)
                    : contentAsObject.Result;
                }
                throw new Exception(response.ReasonPhrase);
            });
        }


        // helper class for the API response
        class HasResult
        {
            public int Result { get; set; }
        }
    }
}
