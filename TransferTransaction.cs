using System;


namespace bank
{
    class TransferTransaction : Transaction
    {
        private Account _fromAccount;
        private Account _toAccount;
        private DepositTransaction _deposit;
        private WithdrawTransaction _withdraw;


        //Constructor. Initializes a new instance of the TransferTransaction class. It sets the associated accounts and the amount to transfer
        public TransferTransaction(Account fromAccount, Account toAccount, decimal amount) : base(amount)
        {
            _deposit = new DepositTransaction(toAccount,amount);
            _withdraw = new WithdrawTransaction(fromAccount,amount);
            _fromAccount= fromAccount;
            _toAccount= toAccount;
            base._amount = amount;
        }

        //Prints the transfer transaction's details.
        public override void Print()
        {
            if (Executed)
            {
                Console.Write("Transaction ");
                if (Success && !Reversed)
                {
                    Console.WriteLine($"executed successfully, {_amount.ToString("C")} transfered from the account ({_fromAccount.Name()}) to account ({_toAccount.Name()})");
                }
                if (Reversed)
                {
                    Console.WriteLine($"reversed successfully, {_amount.ToString("C")} transfered from the account ({_toAccount.Name()}) to account ({_fromAccount.Name()})");
                }
            }
        }

        //Executes the transfer transaction and its underlying tasks.
        public override void Execute()
        {
            if (base.Executed)
            {
                throw new InvalidOperationException("Sorry the transaction has already been attempted");
            }
            base.Execute();
            
            if (!_deposit.Executed && !_withdraw.Executed)
            {
                _withdraw.Execute();

                _deposit.Execute();
                
                base._success = true;
                Print();
            }
        }

        //Performs a reversal of the preceding Transfer operation
        public override void RollBack()
        {
            if (!base._success)
            {
                throw new InvalidOperationException("The original withdraw operation has not been finalized");
            }
            else if (base.Reversed)
            {
                throw new InvalidOperationException("The transaction has already been reversed");
            }
            else
            {
                _deposit.RollBack();
                if (_deposit.Reversed)
                {
                    _withdraw.RollBack();
                    base.RollBack();
                    Print();
                }
                else
                {
                    throw new InvalidOperationException("Nahhhhhhh myte");
                }
            }

        }
    }
}
