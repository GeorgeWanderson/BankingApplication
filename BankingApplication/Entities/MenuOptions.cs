using BankingApplication.Entities;
using BankingApplication.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace BankingApplication.Entities
{
    public class MenuOptions
    {
        static List<Account> accountsList = new List<Account>();
        public static void ShowMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("************************************************");
            Console.WriteLine();
            Console.WriteLine("\tWelcome to your favorite Banking!\t");
            Console.WriteLine();
            Console.WriteLine("************************************************");
            Console.WriteLine();
            Console.WriteLine("Please wait 5 seconds, we will start our system soon!");
            Console.ResetColor();
            Thread.Sleep(5000);
            BankingOptions();
        }

        public static void BankingOptions()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("What do you want do to?\n");
            Console.WriteLine("1 - List Accounts");
            Console.WriteLine("2 - Create a new Account");
            Console.WriteLine("3 - Deposit");
            Console.WriteLine("4 - Withdrawal");
            Console.WriteLine("5 - Transfer");
            Console.WriteLine("6 - Clear");
            Console.WriteLine("7 - Leave");
            Console.ResetColor();
            Console.WriteLine();

            string option;

            do
            {
                option = Console.ReadLine().ToUpper();
                switch (option)
                {
                    case "1":
                        ListAccounts();
                        break;
                    case "2":
                        InsertNewAccount();
                        break;
                    case "3":
                        Deposit();
                        break;
                    case "4":
                        Withdrawal();
                        break;
                    case "5":
                        Transfer();
                        break;
                    case "6":
                        Console.Clear();
                        break;
                    case "7":
                        Environment.Exit(0);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();

                }
            } while (option != "7");
        }

        private static void Deposit()
        {
            Console.Write("Enter number account:");
            int number = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("How much do you want to deposit?");
            double amount = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);

            var account = accountsList.Find(a => a.Number == number);
            if (account != null)
            {
                if (account.Deposit(amount))
                {
                    Console.WriteLine();
                    Console.WriteLine("Successful Deposit!");
                    Console.WriteLine(account);
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Sorry, invalid value. You can only type positive numbers");

                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Account not found.");
            }

        }

        private static void Withdrawal()
        {
            Console.WriteLine("Enter number account:");
            int number = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("How much do you need?");
            double amount = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);

            var account = accountsList.Find(a => a.Number == number);
            if (account != null)
            {
                if (account.Withdrawal(amount))
                {
                    Console.WriteLine();
                    Console.WriteLine("Successful withdrawal!");
                    Console.WriteLine(account);
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Insufficiente balance.");
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Account not found.");
            }

        }

        //private static void Transfer()
        //{
        //    Console.Write("Enter your number account:");
        //    int sourceNumber = Convert.ToInt32(Console.ReadLine());

        //    Console.WriteLine("Enter destination number account:");
        //    int destinationNumber = Convert.ToInt32(Console.ReadLine());

        //    Console.WriteLine("How much do you want to transfer?");
        //    double amount = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);

        //    var sourceAccount = accountsList.Find(a => a.Number == sourceNumber);
        //    var destinationAccount = accountsList.Find(a => a.Number == destinationNumber);

        //    if (sourceAccount != null && destinationAccount != null)
        //    {
        //        if (sourceAccount.Transfer(amount, destinationAccount))
        //        {
        //            Console.WriteLine();
        //            Console.WriteLine("Successful transfer!");
        //            Console.WriteLine("Source Account:");
        //            Console.WriteLine(sourceAccount);
        //            Console.WriteLine();
        //            Console.WriteLine("Destination Account:");
        //            Console.WriteLine(destinationAccount);
        //        }
        //        else
        //        {
        //            Console.WriteLine();
        //            Console.WriteLine("Insufficient balance in the source account.");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine();
        //        Console.WriteLine("One or both accounts not found.");
        //    }
        //}

        private static void Transfer()
        {
            Console.Write("Enter your account number: ");
            int sourceNumber = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter destination account number: ");
            int destinationNumber = Convert.ToInt32(Console.ReadLine());

            Console.Write("How much do you want to transfer? ");
            double amount = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);

            var sourceAccount = accountsList.Find(a => a.Number == sourceNumber);
            var destinationAccount = accountsList.Find(a => a.Number == destinationNumber);

            if (sourceAccount != null && destinationAccount != null)
            {
                if (sourceAccount.Withdrawal(amount))
                {
                    if (destinationAccount.Deposit(amount))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Successful transfer!");
                        Console.WriteLine("Source Account:");
                        Console.WriteLine(sourceAccount);
                        Console.WriteLine();
                        Console.WriteLine("Destination Account:");
                        Console.WriteLine(destinationAccount);
                    }
                    else
                    {
                        // Reverse the withdrawal if deposit fails
                        sourceAccount.Deposit(amount);

                        Console.WriteLine();
                        Console.WriteLine("Transfer failed. Please try again later.");
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Insufficient balance in the source account.");
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("One or both accounts not found.");
            }
        }


        private static void InsertNewAccount()
        {
            Console.Clear();
            Console.WriteLine("Insert new Account: \n");

            Console.WriteLine("Enter account number: ");
            int number = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Choose account type: ");
            Console.Write("\t1- Natural Person");
            Console.WriteLine("\t2 - Legal Person");
            int typePerson = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the account holder's name: ");
            string holder = Console.ReadLine();

            Console.Write("Enter the initial balance: ");
            double initialBalance = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);

            Console.Write("Enter the initial credit: ");
            double initialCredit = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);

            Account account = new Account((AccountType)typePerson, initialBalance, initialCredit, holder, number);
            accountsList.Add(account);
        }

        private static void ListAccounts()
        {

            if (accountsList.Count <= 0)
            {
                Console.WriteLine("There are no accounts");
            }
            else
            {
                Console.WriteLine("List of Accounts: \n");
                foreach (var account in accountsList)
                {
                    Console.WriteLine(account);
                    Console.WriteLine();
                }
            }
        }
    }
}

