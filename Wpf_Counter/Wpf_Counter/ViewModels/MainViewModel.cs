using System.ComponentModel;
using System.Windows.Input;
using CounterApp.Commands;
using Wpf_Counter.Models;

namespace CounterApp.ViewModels;

public class MainViewModel : INotifyPropertyChanged, IDisposable
{
    private readonly Counter _counter;
    private bool _disposed;

    public event PropertyChangedEventHandler? PropertyChanged;

    // Прокси-свойство — теперь Binding найдёт его
    public int Count => _counter.Count;

    public ICommand IncrementCommand { get; }
    public ICommand DecrementCommand { get; }

    public MainViewModel(Counter counter)
    {
        _counter = counter;
        _counter.Count = 0;

        // Подписываемся на модель и переиспускаем её уведомления
        _counter.PropertyChanged += OnCounterPropertyChanged;

        IncrementCommand = new RelayCommand(Increment);
        DecrementCommand = new RelayCommand(Decrement);
    }

    /// <summary>
    /// Вызвать при закрытие окна
    /// </summary>
    public void Dispose()
    {
        if (_disposed)
            return;

        _counter.PropertyChanged -= OnCounterPropertyChanged;
        _disposed = true;

        // Можно через деструктор, но лучше явно вызывать Dispose() при закрытии окна
        // так как он недетерминированный, и GC не гарантирует его вызов в нужный момент
        // В текущем случае Counter держит ссылку на ViewModel, и наоборот 
        // Это циклическая ссылка — но GC в .NET справляется с циклическими ссылками
        // По этому, утечни не должны быть, но лучше явно отписываться от событий,
        // чтобы GC мог освободить память быстрее
    }

    private void OnCounterPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        // Если изменилось Count — сообщаем об этом View
        if (e.PropertyName == nameof(Counter.Count))
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
    }

    private void Increment() => _counter.Increment();
    private void Decrement() => _counter.Decrement();
}