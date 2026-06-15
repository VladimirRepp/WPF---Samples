using System.ComponentModel;

namespace Wpf_TreasureHunt.Models
{
    public class Cell : INotifyPropertyChanged
    {
        private string _text = "?";
        private bool _isOpened;

        public int Row { get; set; }

        public int Column { get; set; }

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        public bool IsOpened
        {
            get => _isOpened;
            set
            {
                _isOpened = value;
                OnPropertyChanged(nameof(IsOpened));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}