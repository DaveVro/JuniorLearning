using System;

namespace classes
{
    /// <summary>
    /// Card account type derived from BankAccount
    /// - Allows for an over draft charge when withdrawing more than the credit limit
    /// - Adds monthly interest charges when applicable
    /// </summary>
    public class LineOfCreditAccount : BankAccount
    {
        /// <summary>
        /// Creates a new instance of a credit account type
        /// </summary>
        /// <param name="name">Account owner</param>
        /// <param name="initialBalance">Starting balance</param>
        /// <param name="creditLimit">Credit limit on overdrawn accounts, transaction fee's apply for any withdrawals over the limit</param>
        public LineOfCreditAccount(string name, decimal initialBalance, decimal creditLimit) : base(name, initialBalance, -creditLimit)
        {
        }

        /// <summary>
        /// Overrides base method when checking over drawn status of credit accounts, charging a transaction fee instead
        /// </summary>
        /// <param name="isOverdrawn">Adds an interest charge if overdrawn instead of blocking the transaction</param>
        /// <returns></returns>
        protected override Transaction CheckWithdrawalLimit(bool isOverdrawn) => isOverdrawn ? new Transaction(-20, DateTime.Now, "Apply overdraft fee") : default;

        /// <summary>
        /// Performs credit account month end transactions, charging interest where applicable
        /// </summary>
        public override void PerformMonthEndTransactions()
        {
            if (Balance < 0)
            {
                // Negate the balance to get a positive interest charge
                var interest = -Balance * 0.07m;
                MakeWithdrawal(interest, DateTime.Now, "Charge monthly interest");
            }
        }
    }
}
