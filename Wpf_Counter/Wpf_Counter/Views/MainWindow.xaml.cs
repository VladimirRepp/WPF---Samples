using CounterApp.ViewModels;
using System.Windows;

namespace Wpf_Counter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // DataContext - откуда брать данные для Binding
            DataContext = new MainViewModel(); // подключаем ViewModel к DataContext окна
        }
    }
}