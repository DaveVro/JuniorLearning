using System;

namespace classes
{
    /// <summary>
    /// Savings account type derived from BankAccount
    /// - Adds a monthly interest deposit on a positive balance of 500 or more
    /// </summary>
    public class InterestEarningAccount : BankAccount
    {
        /// <summary>
        /// Creates a new account instance of an interest earning account
        /// </summary>
        /// <param name="name">Account owner</param>
        /// <param name="initialBalance">Starting balance</param>
        public InterestEarningAccount(string name, decimal initialBalance) : base(name, initialBalance)
        {
        }

        /// <summary>
        /// Performs savings account month end transactions, adding interest where applicable
        /// </summary>
        public override void PerformMonthEndTransactions()
        {
            if (Balance > 500m)
            {
                var interest = Balance * 0.05m;
                MakeDeposit(interest, DateTime.Now, "Apply monthly interest");
            }
        }
    }
}
