using System.ComponentModel;

namespace Wpf_Counter.Models
{
    /// <summary>
    /// Модель - бизнес-логика
    /// </summary>
    public class Counter : INotifyPropertyChanged
    {
        private int _number;

        public event PropertyChangedEventHandler? PropertyChanged;

        public int Count
        {
            get => _number;
            set
            {
                _number = value;

                PropertyChanged?.Invoke(
                    this,
                    new PropertyChangedEventArgs(
                        nameof(Count)));
            }
        }

        public Counter()
        {
            _number = 0;
        }

        public Counter(int number)
        {
            _number = number;
        }

        public void Increment() => Count = Count + 1;
        public void Decrement() => Count = Count - 1;

        public void ChangeTo(int number) => Count = Count + number;
    }
}
