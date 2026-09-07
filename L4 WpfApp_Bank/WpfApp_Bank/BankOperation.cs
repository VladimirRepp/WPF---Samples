using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp_Bank
{
    public enum OperationType
    {
        Deposit,
        Withdrawal
    }

    /// <summary>
    /// Модель банковской операции: один объект описывает одну операцию
    /// </summary>
    public class BankOperation
    {
        public OperationType Type { get; }
        public decimal Amount { get; set; }
        public DateTime Date { get; }

        public string TypeText
        {
            get
            {
                return Type switch
                {
                    OperationType.Deposit => "Пополнение",
                    OperationType.Withdrawal => "Снятие",
                    _ => "Неизвестно"
                };
            }
        }

        public string Sign
        {
            get
            {
                return Type == OperationType.Deposit
                    ? "+"
                    : "-";
            }
        }

        public BankOperation(OperationType type, decimal amount)
        {
            Type = type;
            Amount = amount;
            Date = DateTime.Now;
        }
    }
}
