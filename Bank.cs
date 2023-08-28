using System;
using read;

namespace bank
{
    class Bank
    {
        read.read Read = new read.read();
        List<Account> _accounts = new List<Account>();
        List<Transaction> _transactions = new List<Transaction>();

        public Bank() { }

        public void AddAccount(Account account)
        {
            _accounts.Add(account);
            Console.WriteLine("Account added successfully");
        }

        public Account GetAccount(String name)
        {
            foreach (Account account in _accounts)
            {
                if (account.Name() == name) return account;
            }
            Console.WriteLine($"\nOops: No account with name '{name}' found ");
            return null;
        }

        public void ExecuteTransaction(Transaction transaction)
        {
            transaction.Execute();
            _transactions.Add(transaction);
        }

        public void RollBackTransaction(Transaction transaction)
        {
            transaction.RollBack();
        }

        public void DoRollback(int idx)
        {
            RollBackTransaction(_transactions[idx-1]);
        }

        public void PrintTransactionHistory()
        {
            int i = 0;
            if (_transactions.Count == 0)
            {
                Console.WriteLine("N/A");
            }
            else
            { 
                foreach (Transaction transaction in _transactions)
                {
                    Console.Write($" {i+1}. [{transaction.DateStamp}] ");
                    transaction.Print();
                    i++;
                }
                if (Read.Boolean("\nRollback? "))
                {
                    DoRollback(Read.Integer("Transaction number: ", 1, _transactions.Count));
                }
            }
        }



    }
}
