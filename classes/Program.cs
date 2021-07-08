using System;

namespace classes
{
    class Program
    {
        /// <summary>
        /// This is the Entry Point of the program, used to initialize a bank account and test transactions
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var account = new BankAccount("David", 1000);
            Console.WriteLine($"Account {account.Number} was created for {account.Owner} with {account.Balance} initial balance.");

            account.MakeWithdrawal(500, DateTime.Now, "Rent payment");
            Console.WriteLine(account.Balance);
            account.MakeDeposit(100, DateTime.Now, "Friend paid me back");
            Console.WriteLine(account.Balance);

            Console.WriteLine("");
            Console.WriteLine(account.GetAccountHistory());

            // Test for invalid account
            BankAccount invalidAccount;
            try
            {
                invalidAccount = new BankAccount("invalid", -55);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Exception caught creating account with negative balance");
                Console.WriteLine(e.ToString());
                Console.WriteLine("");
            }

            // Test for negative balance
            try
            {
                account.MakeWithdrawal(750, DateTime.Now, "Attempt to withdraw");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Exception caught trying to overdraw");
                Console.WriteLine(e.ToString());
                Console.WriteLine("");
            }
        }
    }
}