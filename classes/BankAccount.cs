using System;
using System.Collections.Generic;

namespace classes
{
    /// <summary>
    /// Maintains BankAccount, with creating a new instance, making deposits and withdrawals, and viewing the list of transactions
    /// </summary>
    public class BankAccount
    {
        private List<Transaction> allTransactions = new List<Transaction>();

        private static int accountNumberSeed = 1234567890;
        public string Number { get; }
        public string Owner { get; set; }
        public decimal Balance
        {
            get
            {
                decimal balance = 0;
                foreach (var itm in allTransactions)
                {
                    balance += itm.Amount;
                }

                return balance;
            }
        }

        public readonly decimal minimumBalance;

        /// <summary>
        /// Creates a new instance of BankAccount for non-credit accounts
        /// </summary>
        /// <param name="name">Account owner</param>
        /// <param name="initialBalance">Starting balance</param>
        public BankAccount(string name, decimal initialBalance) : this(name, initialBalance, 0)
        {
        }

        /// <summary>
        /// This constructor is used to create a new instance of BankAccount
        /// </summary>
        /// <param name="name">Account owner</param>
        /// <param name="initialBalance">Starting balance</param>
        /// <param name="minimumBalance">Minimum balance for credit accounts</param>
        public BankAccount(string name, decimal initialBalance, decimal minimumBalance)
        {
            this.Number = accountNumberSeed.ToString();
            accountNumberSeed++;

            this.Owner = name;
            this.minimumBalance = minimumBalance;

            if (initialBalance > 0)
                MakeDeposit(initialBalance, DateTime.Now, "Initial balance");
        }

        /// <summary>
        /// This method is used to make bank deposits
        /// </summary>
        /// <param name="amount">Amount being deposited</param>
        /// <param name="date">Date of deposit</param>
        /// <param name="note">Any notes regarding the deposit</param>
        public void MakeDeposit(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive.");
            }

            var deposit = new Transaction(amount, date, note);
            allTransactions.Add(deposit);
        }

        /// <summary>
        /// This method is used to make bank withdrawals
        /// </summary>
        /// <param name="amount">Amount being withdrawn</param>
        /// <param name="date">Date of withdrawal</param>
        /// <param name="note">Any notes regarding the withdrawal</param>
        public void MakeWithdrawal(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive.");
            }

            var overdraftTransaction = CheckWithdrawalLimit(Balance - amount < minimumBalance);
            var withdrawal = new Transaction(-amount, date, note);
            allTransactions.Add(withdrawal);
            if (overdraftTransaction != null)
            {
                allTransactions.Add(overdraftTransaction);
            }
        }

        /// <summary>
        /// Checks over drawn status of the account during withdrawals
        /// </summary>
        /// <param name="isOverdrawn">Declines the transaction if overdrawn</param>
        /// <returns></returns>
        protected virtual Transaction? CheckWithdrawalLimit(bool isOverdrawn)
        {
            if (isOverdrawn)
            {
                throw new InvalidOperationException("Insufficient funds for this withdrawal");
            }
            else
            {
                return default;
            }
        }

        /// <summary>
        /// This method returns the transaction history
        /// </summary>
        /// <returns></returns>
        public string GetAccountHistory()
        {
            var report = new System.Text.StringBuilder();

            decimal balance = 0;

            report.AppendLine("Date\t\tAmount\tBalance\tNote");
            foreach (var itm in allTransactions)
            {
                balance += itm.Amount;
                report.AppendLine($"{itm.Date.ToShortDateString()}\t{itm.Amount}\t{balance}\t{itm.Notes}");
            }

            return report.ToString();
        }

        /// <summary>
        /// Base method for month end transactions
        /// </summary>
        public virtual void PerformMonthEndTransactions()
        {
        }
    }
}
