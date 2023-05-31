using Models;
using Models.Exceptions;

namespace TransactionDataAccessLayer
{
    /// <summary>
    /// An internal static class which implements some services
    /// </summary>
    internal static class DbInternalServices
    {
        /// <summary>
        /// Calculates all the money in the ATM.
        /// </summary>
        /// <param name="context">the ATM db context.</param>
        /// <returns>The Value of the money in the ATM.</returns>
        internal static int GetAllMoney(TransactionsDbContext context) => CalculateMoney(context.Bills.ToList());
        /// <summary>
        /// Calculates the value of a collection of bills.
        /// </summary>
        /// <param name="bills">Collection of bills to calculate.</param>
        /// <returns>The Value of the money in the collection.</returns>
        internal static int CalculateMoney(ICollection<Bill> bills) =>
        bills.Aggregate(0, (current, bill) => current += bill.Amount * bill.Value);
        /// <summary>
        /// Makes a withdraw from db context.
        /// </summary>
        /// <param name="context">The ATM db context</param>
        /// <param name="amount">The amount of money requested for the withdraw.</param>
        /// <returns>If the money was withdrawn.</returns>
        /// <exception cref="NoMoneyException">Thrown if there is not enaugh money in the ATM for the request.</exception>
        /// <exception cref="MoneyNotFitException">Thrown if the ATM cannot make a combination of his bills for the amount.</exception>
        internal static bool WithdrawFromAtm(TransactionsDbContext context, int amount)
        {
            // start new transaction.
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var moneyInAtm = GetAllMoney(context);
                if (moneyInAtm < amount)
                {
                    throw new NoMoneyException(moneyInAtm, amount);
                }


                var atmBillsSorted = context.Bills.OrderBy(b => b.Value).ToList();
                var billsToWithdraw = new int[atmBillsSorted.Count];

                // check for cases where the smallest bill cannot fit in the ATM, like when the client ask for 80,
                // giving him 50 wont fit (you don't have bill of 30), so he should get 4 of 20's
                amount = HandleSmallestBill(amount, atmBillsSorted, billsToWithdraw);
                amount = CheckIfBillsfit(amount, atmBillsSorted, billsToWithdraw);
                if (amount != 0)
                {
                    transaction.Rollback();
                    throw new MoneyNotFitException();
                }
                // the money in the ATM fits.
                context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
        static int CheckIfBillsfit(int amount, List<Bill> atmBillsSorted, int[] billsToWithdraw)
        {
            atmBillsSorted.Reverse();
            int indexInList = 0;

            atmBillsSorted.ForEach(bill =>
            {
                // go over the bills and give the max valued bill until it's not fit and 
                while (bill.Amount > 0 && amount >= bill.Value)
                {
                    bill.Amount--;
                    amount -= bill.Value;
                    billsToWithdraw[atmBillsSorted.Count - indexInList - 1]++;
                }
                indexInList++;
            });
            return amount;
        }
        static int HandleSmallestBill(int amount, List<Bill> atmBillsSorted, int[] billsToWithdraw)
        {
            // check if the smalles bill can fit the amount
            if (amount % atmBillsSorted[0].Value == 0)
                return amount;

            // check if any bill can fit the amount
            var oddBillIndex = atmBillsSorted.FindIndex(b => b.Value % atmBillsSorted[0].Value != 0 && b.Amount > 0);
            if (oddBillIndex == -1)
            {
                throw new MoneyNotFitException();
            }
            amount -= atmBillsSorted[oddBillIndex].Value;
            atmBillsSorted[oddBillIndex].Amount--;
            billsToWithdraw[oddBillIndex]++;
            return amount;
        }
    }
}
