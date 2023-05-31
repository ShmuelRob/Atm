using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    /// <summary>
    /// A model of bill, every bills group has value and amount.
    /// </summary>
    public class Bill
    {
        /// <summary>
        /// The value of the bill.
        /// </summary>
        [Key]
        public int Value { get; }
        /// <summary>
        /// The amount of the bills int the atm
        /// </summary>
        public int Amount { get; set; }


        /// <summary>
        /// Constructor for bill, initial the default amount to be 5.
        /// </summary>
        /// <param name="value">The value of the bill.</param>
        public Bill(int value) : this(value, 5) { }
        /// <summary>
        /// Constructor for bill, initial the amount according to the amount param.
        /// </summary>
        /// <param name="value">The value of the bill.</param>
        /// <param name="amount">How many bills in the ATM in the beginning.</param>
        [JsonConstructor]
        public Bill(int value, int amount)
        {
            Value = value;
            Amount = amount;
        }
    }
}
