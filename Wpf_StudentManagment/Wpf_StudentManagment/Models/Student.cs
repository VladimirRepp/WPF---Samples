using System.ComponentModel;

namespace Wpf_StudentManagment.Models
{
    /// <summary>
    /// Модель данных - студент
    /// Ответсвенность: хранение информации о студенте (имя, возраст) и уведомление об изменениях свойств.
    /// </summary>
    public class Student : INotifyPropertyChanged
    {
        private string _name;
        private int _age;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public int Age
        {
            get => _age;
            set
            {
                _age = value;
                OnPropertyChanged(nameof(Age));
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
