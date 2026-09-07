using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp_Entered
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void EnterLabel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(CityComboBox.SelectedValue is ComboBoxItem selectedItem)
            {
                CityInfoTextBlock.Text = $"Вы выбрали город: {selectedItem.Content.ToString()}";
            }
            else
            {
                CityInfoTextBlock.Text = string.Empty;
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text;
            string password = PasswordTextBox.Text;

            if(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Имя или не указан", "Ошибка", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool isBegore16 = ValueBefore16RadioButton.IsChecked == true;
            bool isBetween16_22 = Value16_22RadioButton.IsChecked == true;
            bool isAfter22 = ValueAfter22RadioButton.IsChecked == true;

            if (isBegore16 == false && isBetween16_22 == false && isAfter22 == false)
            {
                MessageBox.Show("Не выбран возраст", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool isMale = MaleRadioButton.IsChecked == true;
            bool isFemale = FemaleRadioButton.IsChecked == true;

            if (isMale == false && isFemale == false)
            {
                MessageBox.Show("Не выбран пол", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string city = CityInfoTextBlock.Text;
            if (CityComboBox.SelectedValue is ComboBoxItem selectedItem)
            {
                city = selectedItem.Content.ToString();
            }

            if (string.IsNullOrEmpty(city))
            {
                MessageBox.Show("Не выбран город", "Ошибка",
                   MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool isAgreement = AgreementCheckBox.IsChecked == true;

            if (!isAgreement)
            {
                MessageBox.Show("Нет согласия", "Ошибка",
                  MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            UserInfo.Name = name;
            UserInfo.Password = password;
            UserInfo.City = city;
            UserInfo.IsAgreement = isAgreement;

            if(isBegore16)
                UserInfo.Age = EAgeState.Before16;
            else if(isBetween16_22)
                UserInfo.Age = EAgeState.Between16_22;
            else if(isAfter22)
                UserInfo.Age = EAgeState.After22;

            if(isMale)
                UserInfo.Gender = EGenderState.Male;
            else if (isFemale)
                UserInfo.Gender = EGenderState.Female;

            MessageBox.Show($"Добро пожаловать, {UserInfo.Name}!",
                "Успех", 
                MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
    }
}