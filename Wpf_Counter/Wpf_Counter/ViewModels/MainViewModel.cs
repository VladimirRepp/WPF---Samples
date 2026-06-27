using System.ComponentModel;
using System.Windows.Input;
using CounterApp.Commands;

namespace CounterApp.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    // Model (лучше вынести в отдельный класс):
    private int _counter;

    public event PropertyChangedEventHandler? PropertyChanged;

    public int Counter
    {
        get => _counter;
        set
        {
            _counter = value;

            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(
                    nameof(Counter)));
        }
    }

    // ViewModel:
    public ICommand IncrementCommand { get; }

    public ICommand DecrementCommand { get; }

    public MainViewModel()
    {
        IncrementCommand = new RelayCommand(Increment);
        DecrementCommand = new RelayCommand(Decrement);
    }

    private void Increment()
    {
        Counter++;
    }

    private void Decrement()
    {
        Counter--;
    }
}
