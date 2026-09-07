using System.ComponentModel;

namespace WpfApp_Bank
{
    // В данном примере показана реализацию INotifyPropertyChanged для уведомления UI об изменении значения свойства Balance
    // Подробнее рассмотрено в другом примере: https://github.com/VladimirRepp/WPF---Samples/tree/main/Wpf_Counter
    internal class BankAccountNotify : INotifyPropertyChanged
    {
        private decimal _balance;
        public event PropertyChangedEventHandler? PropertyChanged;

        public decimal Balance
        {
            get => _balance;

            private set
            {
                _balance = value;

                // Уведомляем UI об изменении значения свойства Balance
                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(nameof(Balance)));
            }
        }
    }

    // И тогда в XAML:
    /*
        <TextBlock Text="{Binding Balance,
                          StringFormat={}{0:N2} ₽}"/>
     */
}
