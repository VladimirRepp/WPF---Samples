using System.ComponentModel;
using System.Windows.Input;
using CounterApp.Commands;

namespace CounterApp.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private int _counter;

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

    public ICommand IncrementCommand { get; }

    public ICommand DecrementCommand { get; }

    public MainViewModel()
    {
        IncrementCommand =
            new RelayCommand(Increment);

        DecrementCommand =
            new RelayCommand(Decrement);
    }

    private void Increment()
    {
        Counter++;
    }

    private void Decrement()
    {
        Counter--;
    }

    public event PropertyChangedEventHandler?
        PropertyChanged;
}