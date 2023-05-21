using BankingApplication.Enum;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.Entities
{
    public class Account
    {
        
        public AccountType AccountType { get; private set; }
        public double Balance { get; private set; }
        public double Credit { get; private set; }
        public string Holder { get; private set; }
        public int Number { get; private set; }

        public Account(AccountType accountType, double balance, double credit, string holder, int number)
        {
            AccountType = accountType;
            Balance = balance;
            Credit = credit;
            Holder = holder;
            Number = number;
        }
        public bool Withdrawal(double amount)
        {
            if (amount <= 0 || amount > Balance) return false;

            Balance -= amount;
            return true;
        }

        public bool Deposit(double amount)
        {
            if (amount <= 0) return false;

            Balance += amount;
            return true;

        }

        public void Transfer(double amount, Account destinationAccount)
        {
            if (amount <= Balance)
            {
                Withdrawal(amount);
                destinationAccount.Deposit(amount);
            }
        }

        public override string ToString()
        {
            return $"Holder: {Holder}\nNumber: {Number}\nAccount Type: {AccountType}\nBalance: {Balance.ToString("F2", CultureInfo.InvariantCulture)}";
        }
    }
}
