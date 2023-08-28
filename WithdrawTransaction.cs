using System;

namespace bank
{
    class WithdrawTransaction : Transaction
    {
        //instance variables
        private Account _account;

        //Constructor. Initializes a new instance of the WithdrawTransaction class. It sets the associated account and the amount to withdraw
        public WithdrawTransaction(Account acc, decimal amount) : base(amount)
        {
            _account = acc;
            base._amount = amount;
        }

        //Prints the withdraw transaction's details.
        public override void Print()
        {
            if (Executed)
            {
                Console.Write("Transaction ");
                if (Success && !Reversed)
                {
                    Console.WriteLine($"executed successfully, {_amount.ToString("C")} withdrawn from the account ({_account.Name()})");
                }
                if (Reversed)
                {
                    Console.WriteLine($"reversed successfully, {_amount.ToString("C")} deposited back to the account ({_account.Name()})");
                }
            }
        }

        //Executes the withdraw transaction and its underlying tasks.
        public override void Execute()
        {
            if (base.Executed)
            {
                throw new InvalidOperationException("Sorry the transaction has already been attempted");
            }
            base.Execute();
            if (_account.Withdraw(_amount))
            {
                base._success = true;
                Print();
            }
        }

        //Performs a reversal of the preceding withdraw operation
        public override void RollBack()
        {
            if (!_success)
            {
                throw new InvalidOperationException("the original withdraw operation has not been finalized");
            }
            else if (base.Reversed)
            {
                throw new InvalidOperationException("The transaction has already been reversed");
            }
            else
            {
                
                _account.deposit(_amount);
                base.RollBack();
            }
        }
    }
}
