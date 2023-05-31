namespace Models.Exceptions
{
    /// <summary>
    /// exception which get thrown when the money in the atm cannot fit the client request
    /// </summary>
    public class MoneyNotFitException : Exception
    {
        /// <summary>
        /// constructor for the execption
        /// </summary>
        public MoneyNotFitException()
            : base($"sorry, this atm cannot give you this amount of money")
        { }
    }
}
