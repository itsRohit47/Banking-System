using System;
using read;


namespace bank
{
    //implements the UI so that the user gets the control over the affiliated bank account.
    enum MenuOption
    {
        ADD,
        WITHDRAW,
        DEPOSIT,
        PRINT,
        TRANSFER,
        VIEW_TRANSACTION_HISTORY,
        QUIT
    }


    class BankSystem
    {
        static read.read Read = new read.read();

        //Shows the menu to the user and read in the user choice. 
        static MenuOption ReadUserOption()
        {
            read.read Read = new read.read();
            Console.WriteLine("\nSelection Menu\n\n" +
                "1. Add new account\n" +
                "2. Withdraw\n" +
                "3. Deposit\n" +
                "4. Get account details\n" +
                "5. Transfer amount\n" +
                "6. View Transaction history\n" +
                "7. Quit\n");
            return (MenuOption)Read.Integer("Enter your choice from the menu above:", 1, 7)-1;
        }

        static Account FindAccount(Bank bank)
        {
            return bank.GetAccount(Read.Str("Account holder's name: ? "));
        }

        //Performs a deposit operation on the specified account. The method requests for the amount that the user wants to deposit, then forwards the request to the account.
        static void DoDeposit(Bank bank)
        {
            Account acc = FindAccount(bank);
            if (acc != null)
            {
                decimal amount = Read.Decimal("Please enter the amount to be deposited: ");
                DepositTransaction deposit = new DepositTransaction(acc, amount);
                bank.ExecuteTransaction(deposit);
            } 
        }

        //Performs a withdraw operation on the specified account. The method requests for the amount that the user wants to withdraw, then forwards the request to the account.
        static void DoWithdraw(Bank bank)
        {
            Account acc = FindAccount(bank);
            if (acc != null)
            {
                decimal amount = Read.Decimal("Please enter the amount to Withdraw: ");
                WithdrawTransaction withdraw = new WithdrawTransaction(acc, amount);
                bank.ExecuteTransaction(withdraw);
            }
        }
            
        
        //Calls Print on the specified account.
        static void DoPrint(Bank bank)
        {
            Account acc = FindAccount(bank);
            if (acc != null)
            {
                acc.Print();
            }
        }


        static void Main(string[] args)
        {
            Bank bank = new Bank();
            MenuOption userChoice;
            bool quit = false;
            do
            {
                userChoice = ReadUserOption();
                Console.WriteLine();
                try
                {
                    switch (userChoice)
                    {
                        case MenuOption.ADD:
                            bank.AddAccount(new Account(Read.Str("Name? "),Read.Integer("Opening Balance")));
                            break;
                        case MenuOption.WITHDRAW:
                            DoWithdraw(bank);
                            break;
                        case MenuOption.DEPOSIT:
                            DoDeposit(bank);
                            break;
                        case MenuOption.PRINT:
                            DoPrint(bank);
                            break;
                        case MenuOption.TRANSFER:
                            Account withdraw = bank.GetAccount(Read.Str("Enter withdraw account name: "));
                            if (withdraw == null)
                            {
                                break;
                            }
                           
                            Account deposit = bank.GetAccount(Read.Str("Enter deposit account name: "));
                            if (deposit == null)
                            {
                                break;
                            }

                            if (withdraw.Name == deposit.Name)
                            {
                                Console.WriteLine("Sorry choose a different account, can't transfer funds in same account");
                            }
                            else
                            {
                                TransferTransaction transfer = new TransferTransaction(withdraw, deposit, Read.Decimal("Transfer amount: "));
                                bank.ExecuteTransaction(transfer);
                            }
                            break;
                        case MenuOption.VIEW_TRANSACTION_HISTORY:
                            bank.PrintTransactionHistory();
                            break;
                        case MenuOption.QUIT:
                            if (Read.Boolean("Confirm quit? "))
                            {
                                quit = true;
                                Console.Clear();
                                Console.WriteLine("Quit action triggred");
                                Console.ReadLine();
                            }
                            break;
                        default:
                            Console.WriteLine("\nInvalid choice, select from the given menu please");
                            break;
                    }
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine(e.Message);
                }
            } while (!quit);
        }
    }
}