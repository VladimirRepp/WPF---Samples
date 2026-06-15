using System.Windows;
using Wpf_StudentManagment.ViewModels;

namespace Wpf_StudentManagment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel(); // подключаем ViewModel
        }
    }
}