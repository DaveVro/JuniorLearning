using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace classes
{
    public class LineOfCreditAccount : BankAccount
    {
        /// <summary>
        /// Creates a new instance of a credit account type
        /// </summary>
        /// <param name="name"></param>
        /// <param name="initialBalance"></param>
        /// <param name="creditLimit"></param>
        public LineOfCreditAccount(string name, decimal initialBalance, decimal creditLimit) : base(name, initialBalance, -creditLimit)
        {
        }

        /// <summary>
        /// Overrides base method when checking over drawn status of credit accounts, charging a transaction fee instead
        /// </summary>
        /// <param name="isOverdrawn"></param>
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
