using System.ComponentModel;
using System.Windows.Input;
using CounterApp.Commands;
using Wpf_Counter.Models;

namespace CounterApp.ViewModels;

public class MainViewModel : INotifyPropertyChanged, IDisposable
{
    private readonly Counter _counter;
    private bool _disposed;

    // Событие - уведомление об изменение свойства 
    public event PropertyChangedEventHandler? PropertyChanged;

    // Прокси-свойство — теперь Binding берет его
     public int Count
     {
         get => _counter.Count;
         set => _counter.Count = value;
     }

    public ICommand IncrementCommand { get; }
    public ICommand DecrementCommand { get; }

    public MainViewModel(Counter counter)
    {
        _counter = counter;

        IncrementCommand = new RelayCommand(Increment);
        DecrementCommand = new RelayCommand(Decrement);
    }

    /// <summary>
    /// Вызвать при закрытие окна, при необходимости очистить ресурсы до GC
    /// </summary>
    public void Dispose()
    {
        if (_disposed)
            return;

        _disposed = true;
    }

    private void Increment()
     {
         _counter.Increment();
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Count"));
     }
    
     private void Decrement() 
     {
         _counter.Decrement();
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Count"));
     }
}
