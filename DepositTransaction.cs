using System;

namespace bank
{
    class DepositTransaction : Transaction
    {
        //instance variables
        private Account _account;


        //Constructor. Initializes a new instance of the WithdrawTransaction class. It sets the associated account and the amount to deposit
        public DepositTransaction(Account account, decimal amount) : base(amount)
        {
            _account = account;
            base._amount = amount;
        }

        //Prints the deposit transaction's details.
        public override void Print()
        {
            if (Executed)
            {
                Console.Write("Transaction ");
                if (Success && !Reversed)
                {
                    Console.WriteLine($"executed successfully, {_amount.ToString("C")} deposited to the account ({_account.Name()})");
                }
                if (Reversed)
                {
                    Console.WriteLine($"reversed successfully, {_amount.ToString("C")} withdrawn from the account ({_account.Name()})");
                }
            }
        }

        //Executes the deposit transaction and its underlying tasks.
        public override void Execute()
        {
            if (base.Executed)
            {
                throw new InvalidOperationException("Sorry the transaction has already been attempted");
            }
            base.Execute();
			
            if (_account.deposit(_amount))
            {
                base._success = true;
                Print();
            }
            else
			{
				throw new InvalidOperationException("Sorry cannot deposit a negative amount");
			}
        }

        //Performs a reversal of the preceding deposit operation
        public override void RollBack()
        {
            if (!_success)
            {
                throw new InvalidOperationException("The original deposit operation has not been finalized");
            }
			else if (base.Reversed)
			{
				throw new InvalidOperationException("The transaction has already been reversed.");
			}
            else
            {
                _account.Withdraw(_amount);
                base.RollBack();
                Print();

            }
        }
    }
}

