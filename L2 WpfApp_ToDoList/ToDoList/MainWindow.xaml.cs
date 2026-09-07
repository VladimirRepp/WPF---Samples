using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Коллекция задач (за которыми наблюдаем, для привязки к ListBox)
        // ObservableCollection<T> умеет уведомлять интерфейс об изменениях коллекции
        private readonly ObservableCollection<TodoItem> _tasks = new();

        // Что произойдёт, если мы изменим IsCompleted из C#?
        // Почему интерфейс узнаёт об изменении или не узнаёт?

        public MainWindow()
        {
            InitializeComponent();

            // Связываем список задач с ListBox
            TasksListBox.ItemsSource = _tasks;
            UpdateStatistics();
        }

        #region === Добавление задачи ===

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddTask();
        }

        private void AddTask()
        {
            string title = TaskTextBox.Text.Trim();

            // Проверяем, что задача не пустая
            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show(
                    "Введите текст задачи.",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                TaskTextBox.Focus();

                return;
            }

            TodoItem task = new TodoItem(title);
            _tasks.Add(task);

            TaskTextBox.Clear();
            TaskTextBox.Focus();

            UpdateStatistics();
        }

        #endregion

        #region === Добавление по Enter === 

        private void TaskTextBox_KeyDown(
            object sender,
            KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddTask();
            }
        }

        #endregion

        #region === Удаление задачи ===

        private void DeleteButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (TasksListBox.SelectedItem is not TodoItem selectedTask)
            {
                MessageBox.Show(
                    "Выберите задачу для удаления.",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            _tasks.Remove(selectedTask);

            UpdateStatistics();
        }

        #endregion

        #region === Очистка списка ===

        private void ClearButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (_tasks.Count == 0)
            {
                return;
            }


            MessageBoxResult result = MessageBox.Show(
                "Вы действительно хотите удалить все задачи?",
                "Очистка списка",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);


            if (result != MessageBoxResult.Yes)
            {
                return;
            }

            _tasks.Clear();

            UpdateStatistics();
        }

        #endregion

        #region === Изменение состояния задачи ===
       
        private void TaskCheckBox_Checked(
            object sender,
            RoutedEventArgs e)
        {
            UpdateStatistics();
        }

        private void TaskCheckBox_Unchecked(
            object sender,
            RoutedEventArgs e)
        {
            UpdateStatistics();
        }

        #endregion

        #region === Статистика === 
        private void UpdateStatistics()
        {
            int totalCount = _tasks.Count;

            int completedCount = 0;

            foreach (TodoItem task in _tasks)
            {
                if (task.IsCompleted)
                {
                    completedCount++;
                }
            }

            TasksCountTextBlock.Text =
                $"Задач: {totalCount}";

            CompletedCountTextBlock.Text =
                $"Выполнено: {completedCount}";
        }

        #endregion
    }
}