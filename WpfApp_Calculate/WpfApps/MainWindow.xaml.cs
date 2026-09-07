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
using System.Xml.Serialization;

namespace WpfApps
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _firstNum;
        private int _secondNum;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonMinus_Click(object sender, RoutedEventArgs e)
        {
            Calculate((x, y) => x - y);
        }

        private void ButtonPlus_Click(object sender, RoutedEventArgs e)
        {
            Calculate((x, y) => x + y);
        }

        private void ButtonMult_Click(object sender, RoutedEventArgs e)
        {
            Calculate((x, y) => x * y);
        }

        private void ButtonDiv_Click(object sender, RoutedEventArgs e)
        {
            Calculate((x, y) => x / y);
        }
    
        private bool TryInput()
        {
            if (int.TryParse(FirstNumberTextBox.Text, out _firstNum) == false)
            {
                MessageBox.Show("Ошибка ввода первого числа!");
                return false;
            }

            if (int.TryParse(SecondNumberTextBox.Text, out _secondNum) == false)
            {
                MessageBox.Show("Ошибка ввода второго числа!");
                return false;
            }

            return true;
        }

        private void Output(float result)
        {
            ReultTextBlock.Text = result.ToString();
        }

        private void Calculate(Func<int, int, float> oper)
        {
            if (!TryInput())
                return;

            try
            {
                Output(oper(_firstNum, _secondNum));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}