using System.Collections.ObjectModel;

namespace WpfApp_Bank
{
    /// <summary>
    /// Модель банковского счёта
    /// </summary>
    public class BankAccount
    {
        // double — это тип с плавающей точкой, предназначенный прежде всего для вычислений с приближёнными значениями
        // Например, при финансовых расчётах ошибки округления могут быть нежелательны
        // По этому используется decimal — это тип с фиксированной точкой, предназначенный для финансовых и денежных расчётов

        public decimal Balance { get; private set; }

        public ObservableCollection<BankOperation> Operations
        {
            get;
        }

        public BankAccount(decimal initialBalance = 0)
        {
            Balance = initialBalance;

            Operations =
                new ObservableCollection<BankOperation>();
        }

        public bool Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                return false;
            }

            Balance += amount;
            Operations.Add(
                new BankOperation(
                    OperationType.Deposit,
                    amount));

            return true;
        }


        public bool Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                return false;
            }

            if (amount > Balance)
            {
                return false;
            }

            Balance -= amount;
            Operations.Add(
                new BankOperation(
                    OperationType.Withdrawal,
                    amount));

            return true;
        }

        public void ClearOperations()
        {
            Operations.Clear();
        }
    }
}
