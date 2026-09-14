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
            // Нарушаем MVVM:
            // View знает о Model 
            // Решением в App.xaml.cs - создаём ViewModel и передаём её в DataContext окна

            //Counter counter = new();

            //// DataContext - откуда брать данные для Binding
            //DataContext = new MainViewModel(counter); // подключаем ViewModel к DataContext окна
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainViewModel mv = (MainViewModel)DataContext;
            mv.Dispose();
        }
    }
}