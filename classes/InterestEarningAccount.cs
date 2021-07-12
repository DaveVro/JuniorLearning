                                                                         using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace classes
{
    public class InterestEarningAccount : BankAccount
    {
        /// <summary>
        /// Creates a new account instance of an interest earning account
        /// </summary>
        /// <param name="name"></param>
        /// <param name="initialBalance"></param>
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
