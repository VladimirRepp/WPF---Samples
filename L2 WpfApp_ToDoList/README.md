# L2 WpfApp_ToDoList — Список задач с привязкой данных (Binding)

**Третий шаг** в изучении WPF. Этот проект знаменует собой переход от простых форм к работе с данными. Мы создадим полноценный список задач, используя модель, коллекцию `ObservableCollection<T>`, элемент `ListBox` и, самое главное, механизм **Binding** (привязка данных).

**Ключевая идея:** Мы продолжаем использовать простую архитектуру Code-behind, но теперь данные (список задач) отделены от интерфейса и связаны с ним через мощный механизм WPF.

---

## 🎯 Цель проекта

Научиться работать с коллекциями данных, отображать их в интерфейсе и управлять ими, используя один из главных китов WPF — **привязку данных (Data Binding)** . Вы поймете, как UI-элементы могут автоматически отражать изменения в данных и наоборот.

---

## 📚 Ключевые концепции (шпаргалка для студентов)

### 1. Модель данных (`TodoItem`)

Это класс, описывающий одну задачу. Он не зависит от интерфейса.

```csharp
public class TodoItem
{
    public string Title { get; set; }      // Название задачи
    public bool IsCompleted { get; set; }   // Статус выполнения

    public TodoItem(string title)
    {
        Title = title;
        IsCompleted = false;
    }
}
```

### 2. Наблюдаемая коллекция (`ObservableCollection<T>`)

Это специальная коллекция, которая **уведомляет интерфейс** о своих изменениях: добавлении, удалении, очистке. Если вы используете обычный `List<T>`, изменения не отобразятся в UI.

```csharp
private readonly ObservableCollection<TodoItem> _tasks = new();
TasksListBox.ItemsSource = _tasks; // Связываем коллекцию с ListBox
```

Теперь при добавлении или удалении задачи из `_tasks`, `ListBox` на форме обновится автоматически.

### 3. Привязка данных (Data Binding)

**Binding** — это механизм автоматической связи свойства объекта (данных) со свойством UI-элемента.

#### Как это работает (схема):

1.  **Источник:** Объект с данными (например, `TodoItem`).
2.  **Цель:** UI-элемент (например, `CheckBox`).
3.  **Посредник:** Механизм Binding в WPF.

```xml
<CheckBox Content="{Binding Title}" 
          IsChecked="{Binding IsCompleted}" />
```

В этом примере:
*   `CheckBox.Content` автоматически получает значение из `TodoItem.Title`.
*   `CheckBox.IsChecked` автоматически синхронизируется с `TodoItem.IsCompleted`.

Когда пользователь ставит или снимает галочку, свойство `IsCompleted` у объекта меняется автоматически.

#### Важное ограничение (на этом этапе):

*   `ObservableCollection<T>` уведомляет об изменениях **коллекции** (добавление/удаление).
*   Однако сам объект `TodoItem` **НЕ уведомляет** UI об изменении своих свойств (например, `IsCompleted`).
*   Поэтому, если вы измените `task.IsCompleted = true;` в коде, интерфейс может не обновиться. Для этого существует интерфейс `INotifyPropertyChanged`, который будет рассмотрен в следующих проектах.

---

## 🧩 Функциональность приложения

Вы реализуете полноценное приложение со следующим функционалом:

| Функция | Как реализовано |
| :--- | :--- |
| **Добавление задачи** | Ввод текста в `TextBox` и нажатие кнопки. Новая задача добавляется в `_tasks`. |
| **Удаление задачи** | Выбор задачи в `ListBox` и нажатие кнопки "Удалить". |
| **Отметка о выполнении** | Установка/снятие галочки в `CheckBox` внутри шаблона `ListBox`. |
| **Очистка списка** | Кнопка "Очистить всё" вызывает `_tasks.Clear()`. |
| **Количество задач** | `TextBlock` с привязкой к `_tasks.Count`. |
| **Количество выполненных** | Вычисляется через LINQ: `_tasks.Count(t => t.IsCompleted)`. |

---

## 💻 Пример кода в Code-Behind (`MainWindow.xaml.cs`)

```csharp
public partial class MainWindow : Window
{
    private readonly ObservableCollection<TodoItem> _tasks = new();

    public MainWindow()
    {
        InitializeComponent();
        TasksListBox.ItemsSource = _tasks; // Устанавливаем источник данных
        UpdateTaskCounts(); // Обновляем счетчики
    }

    // Добавление задачи
    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(TaskInputTextBox.Text))
        {
            _tasks.Add(new TodoItem(TaskInputTextBox.Text));
            TaskInputTextBox.Clear();
            UpdateTaskCounts();
        }
    }

    // Удаление выбранной задачи
    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        if (TasksListBox.SelectedItem is TodoItem selectedTask)
        {
            _tasks.Remove(selectedTask);
            UpdateTaskCounts();
        }
    }

    // Обновление счетчиков
    private void UpdateTaskCounts()
    {
        TotalTasksTextBlock.Text = $"Всего: {_tasks.Count}";
        CompletedTasksTextBlock.Text = $"Выполнено: {_tasks.Count(t => t.IsCompleted)}";
    }
}
```

---

## 💡 Ключевые выводы для студентов

1.  **Binding** — это "клей" между UI и данными в WPF. Он делает интерфейс реактивным без лишнего кода.
2.  **`ObservableCollection<T>`** — лучший выбор для работы со списками в UI, так как она автоматически обновляет интерфейс.
3.  Разделение данных (Model) и интерфейса (View) — это первый шаг к правильной архитектуре.
4.  В следующем проекте вы узнаете, как заставить объекты (`TodoItem`) самим сообщать об изменениях свойств с помощью `INotifyPropertyChanged`.

---

⭐ Если проект помог вам понять Binding, поставьте звезду репозиторию! Удачи в изучении WPF!