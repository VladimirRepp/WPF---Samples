using System.Collections.ObjectModel;
using System.Windows.Input;
using Wpf_StudentManagment.Commands;
using Wpf_StudentManagment.Models;

namespace Wpf_StudentManagment.ViewModels
{
    /// <summary>
    /// Модель представления для главного окна приложения.
    /// Ответственность: управление списком студентов, добавление новых студентов и связывание данных с представлением.
    /// </summary>
    public class MainViewModel
    {
        public ObservableCollection<Student> Students { get; set; }

        public string StudentName { get; set; }

        public int StudentAge { get; set; }

        public ICommand AddStudentCommand { get; }

        public MainViewModel()
        {
            Students = new ObservableCollection<Student>();

            Students.Add(new Student
            {
                Name = "Иван",
                Age = 20
            });

            Students.Add(new Student
            {
                Name = "Анна",
                Age = 22
            });

            AddStudentCommand =
                new RelayCommand(AddStudent);
        }

        private void AddStudent()
        {
            Students.Add(new Student
            {
                Name = StudentName,
                Age = StudentAge
            });
        }

    }
}
