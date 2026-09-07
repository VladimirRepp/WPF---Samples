using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace WpfApp_Bank
{
    /// <summary>
    /// Code-behind - основная логика окна
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BankAccount _account;

        public MainWindow()
        {
            InitializeComponent();

            _account = new BankAccount(15500);

            // Связываем список операций с коллекцией операций банковского счёта
            OperationsListBox.ItemsSource =
                _account.Operations;

            UpdateBalance();
        }

        private void DepositButton_Click(object sender, RoutedEventArgs e)
        {
            if (!TryGetAmount(out decimal amount))
            {
                return;
            }

            bool success =  _account.Deposit(amount);

            if (!success)
            {
                MessageBox.Show(
                    "Не удалось пополнить счёт.",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            // Почему список операций обновляется автоматически, а баланс приходится обновлять вручную?
            // ObservableCollection умеет сообщать об изменении коллекции.
            // Но не сообщает UI, что значение изменилось.
            // Для этого нужен:
            // INotifyPropertyChanged
            // См. -> BankAccountNotify.cs

            UpdateBalance();
            ClearAmountField();
        }

        private void WithdrawButton_Click(object sender, RoutedEventArgs e)
        {
            if (!TryGetAmount(out decimal amount))
            {
                return;
            }

            if (amount > _account.Balance)
            {
                MessageBox.Show(
                    "Недостаточно средств на счёте.",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            bool success = _account.Withdraw(amount);

            if (!success)
            {
                MessageBox.Show(
                    "Не удалось выполнить операцию.",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            UpdateBalance();
            ClearAmountField();
        }

        private bool TryGetAmount(out decimal amount)
        {
            amount = 0;
            string text = AmountTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(text))
            {
                MessageBox.Show(
                    "Введите сумму.",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                AmountTextBox.Focus();
                return false;
            }

            if (!decimal.TryParse(text, out amount))
            {
                MessageBox.Show(
                    "Введите корректное число.",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                AmountTextBox.Focus();
                return false;
            }


            if (amount <= 0)
            {
                MessageBox.Show(
                    "Сумма должна быть больше нуля.",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                AmountTextBox.Focus();
                return false;
            }

            return true;
        }

        private void ClearAmountField()
        {
            AmountTextBox.Clear();
            AmountTextBox.Focus();
        }

        private void UpdateBalance()
        {
            BalanceTextBlock.Text =  $"{_account.Balance:N2} ₽";
        }

        private void ClearHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            if (_account.Operations.Count == 0)
            {
                return;
            }

            MessageBoxResult result =
                MessageBox.Show(
                    "Очистить историю операций?",
                    "Подтверждение",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
            {
                return;
            }

            _account.ClearOperations();
        }

        private void AmountTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DepositButton_Click(
                    sender,
                    new RoutedEventArgs());
            }
        }
    }
}