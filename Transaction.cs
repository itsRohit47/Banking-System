using System;


namespace bank
{
    abstract class Transaction
    {
        protected decimal _amount;
        protected bool _success;
        private bool _executed;
        private bool _reversed;
        private DateTime dateStamp;
        public bool Success { get { return _success; } }
        public bool Executed { get { return _executed; } }
        public bool Reversed { get { return _reversed; } }
        public DateTime DateStamp { get { return dateStamp; } }

        public Transaction(decimal amount) { }
        public virtual void Print() { }
        public virtual void Execute() 
        {
            _executed = true;
            dateStamp = DateTime.Now;
        }
        public virtual void RollBack() 
        {
            _reversed = true;
            dateStamp = DateTime.Now;
        }
    }
}
