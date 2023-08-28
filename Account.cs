using System;

namespace bank
{
    class Account
    {
        //Instance variables
        private string name;
        private decimal balance;

        //The object constructor : Initializes a new instance of the Account class and sets its initial _balance
        //and _name attributes to the respective values given in the input.
        public Account(string _name, decimal _balance)
        {
            this.name = _name;
            this.balance = _balance;
        }

        //Adds funds to the account increasing the _balance by the specified amount
        public bool deposit(decimal amount)
        {
            if (amount >= 0)
            {
                this.balance += amount;
                Console.WriteLine($"\nNew Balance ({this.name}): {(this.balance).ToString("C")}");
                return true;
            }
            else
            {
                throw new InvalidOperationException("can't do it ");
            }
        }

        //Withdraws funds from the account decreasing the _balance by the specified amount.
        public bool Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new InvalidOperationException("Cannot withdraw a negative amount");
            }
            else if (amount > balance)
            {
                throw new InvalidOperationException("Insuffecient funds");
            }
            else
            {
                this.balance -= amount;
                Console.WriteLine($"\nNew Balance ({this.name}): {(this.balance).ToString("C")}");
                return true;
            }
        }

        //Prints the contents of _balance and _name of the account to the terminal
        public void Print()
        {
            Console.WriteLine($"\nAccount Holder: {name}\n" +
                $"Current Balance: {balance.ToString("C")}\n");
        }

        //Gets the _name of the account.
        public string Name()
        {
            return name;
        }

        public decimal Balance()
        {
            return balance;
        }
    }
}
