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

            InvalidAccountTest();
            NegativeBalanceTest();
            GiftCardTest();
            SavingsAccountTest();
            LineOfCreditTest();

            // Test for invalid account
            void InvalidAccountTest()
            {
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
            }

            // Test for negative balance
            void NegativeBalanceTest()
            {
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

            // Test gift card account
            void GiftCardTest()
            {
                var giftCard = new GiftCardAccount("Gift card", 100, 50);
                giftCard.MakeWithdrawal(20, DateTime.Now, "Get expensive coffee");
                giftCard.MakeWithdrawal(50, DateTime.Now, "Buy groceries");
                giftCard.PerformMonthEndTransactions();
                // Can make additional deposits
                giftCard.MakeDeposit(27.50m, DateTime.Now, "Add some additional spending money");
                Console.WriteLine(giftCard.GetAccountHistory());
            }

            // Test savings account
            void SavingsAccountTest()
            {
                var savings = new InterestEarningAccount("Savings account", 10000);
                savings.MakeDeposit(750, DateTime.Now, "Save some money");
                savings.MakeDeposit(1250, DateTime.Now, "Add more savings");
                savings.MakeWithdrawal(1250, DateTime.Now, "Needed to pay monthly bills");
                savings.PerformMonthEndTransactions();
                Console.WriteLine(savings.GetAccountHistory());
            }


            // Test line of credit account
            void LineOfCreditTest()
            {
                var lineOfCredit = new LineOfCreditAccount("Line of credit", 0, 2000);
                // How much is too much to borrow?
                lineOfCredit.MakeWithdrawal(1000m, DateTime.Now, "Take out monthly advance");
                lineOfCredit.MakeDeposit(50m, DateTime.Now, "Pay back small amount");
                lineOfCredit.MakeWithdrawal(5000m, DateTime.Now, "Emergency funds for repairs");
                lineOfCredit.MakeDeposit(150m, DateTime.Now, "Partial restoration on repairs");
                lineOfCredit.PerformMonthEndTransactions();
                Console.WriteLine(lineOfCredit.GetAccountHistory());
            }
        }
    }
}