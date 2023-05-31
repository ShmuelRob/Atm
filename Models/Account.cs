namespace Models
{
    /// <summary>
    /// An account in the bank, has Id and his balance.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Uniqe id for every account.
        /// </summary>
        public int AccountId { get; set; }
        /// <summary>
        /// The balance of the client, can be either positive or negetive number.
        /// </summary>
        public int Balance { get; set; }
    }
}
