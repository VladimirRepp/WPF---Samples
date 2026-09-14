using CounterApp.ViewModels;
using System.Windows;
using Wpf_Counter.Models;

namespace Wpf_Counter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var vm = new MainViewModel(new Counter());
            new MainWindow { DataContext = vm }.Show();
        }
    }
}
