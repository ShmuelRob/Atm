namespace Models.Exceptions
{
    /// <summary>
    /// Exception which get thrown when there is not enaugh money in the ATM.
    /// </summary>
    public class NoMoneyException : Exception
    {
        /// <summary>
        /// The sum of all the money in the ATM.
        /// </summary>
        public int MoneyInAtm { get; }
        /// <summary>
        /// The amount of the money which the client requested.
        /// </summary>
        public int MoneyAsked { get; }


        /// <summary>
        /// constructor for the exception.
        /// </summary>
        /// <param name="moneyInAtm"> The sum of all the money in the ATM.</param>
        /// <param name="moneyAsked"> The amount of the money which the client requested.</param>
        public NoMoneyException(int moneyInAtm, int moneyAsked)
            : base($"sorry, this atm has only {moneyInAtm}, but you asked for {moneyAsked}")
        {
            MoneyInAtm = moneyInAtm;
            MoneyAsked = moneyAsked;
        }
    }
}
