using System;

namespace classes
{
    /// <summary>
    /// Defines Transaction properties and creates a new instance of a Transaction
    /// </summary>
    public class Transaction
    {
        public decimal Amount { get; }
        public DateTime Date { get; }
        public string Notes { get; }

        /// <summary>
        /// Creates a new instance of Transaction
        /// </summary>
        /// <param name="amount">Value of transaction</param>
        /// <param name="date">Date of transaction</param>
        /// <param name="note">Reason for transaction</param>
        public Transaction(decimal amount, DateTime date, string note)
        {
            this.Amount = amount;
            this.Date = date;
            this.Notes = note;
        }
    }
}
