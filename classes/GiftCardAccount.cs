using System;

namespace classes
{
    /// <summary>
    /// Gift card account type derived from BankAccount
    /// - Allows for monthly top up deposits
    /// </summary>
    public class GiftCardAccount : BankAccount
    {
        private decimal _monthlyDeposit = 0m;

        /// <summary>
        /// Creates a new instance of a gift card account type
        /// </summary>
        /// <param name="name">Account owner</param>
        /// <param name="initialBalance">Opening balance of pre-paid funds available</param>
        /// <param name="monthlyDeposit">Adds a monthly top up of the specified value</param>
        public GiftCardAccount(string name, decimal initialBalance, decimal monthlyDeposit = 0) : base(name, initialBalance) => _monthlyDeposit = monthlyDeposit;

        /// <summary>
        /// Performs gift card account month end transactions, adding a top up ammount as specified
        /// </summary>
        public override void PerformMonthEndTransactions()
        {
            if (_monthlyDeposit != 0)
            {
                MakeDeposit(_monthlyDeposit, DateTime.Now, "Add monthly deposit");
            }
        }
    }
}
