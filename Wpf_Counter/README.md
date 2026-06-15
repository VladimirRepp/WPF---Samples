# 🔢 WPF Counter — первый проект на MVVM

Этот проект создан в **обучающих целях** как **самое простое приложение** для первого знакомства с паттерном **MVVM** в WPF.

Он идеально подходит для студентов, которые:
- уже знают WinForms,
- хотят понять, зачем нужны WPF, XAML и MVVM,
- ищут минимальный рабочий пример без лишней сложности.

Приложение делает всего одну вещь — **увеличивает и уменьшает счётчик** по нажатию кнопок.

---

## 🎯 Почему именно счётчик?

Этот пример обычно оказывается самым удачным для первой практической работы, потому что:

- ✅ **Минимум кода** — легко понять за одну пару
- ✅ **Показывает всю цепочку MVVM** от кнопки до обновления интерфейса
- ✅ **Нет лишнего** — никаких таблиц, баз данных или сложной логики
- ✅ **Студенты сразу видят отличие от WinForms**
- ✅ **Демонстрирует все фундаментальные механизмы WPF**:
  - `Binding`
  - `Commands`
  - `INotifyPropertyChanged`
  - `DataContext`
  - `ICommand`

---

## 🧱 Структура проекта

```
Wpf_Counter/
│
├── Commands/
│   └── RelayCommand.cs          # Реализация ICommand для кнопок
│
├── ViewModels/
│   └── MainViewModel.cs         # Логика счётчика и команды
│
├── Views/
│   └── MainWindow.xaml          # Интерфейс (окно с кнопками и числом)
│
├── MainWindow.xaml.cs           # Только DataContext = new MainViewModel()
│
└── App.xaml                     # Стандартный (можно без изменений)
```

---

## 🔄 Как это работает? Полная цепочка MVVM

Вот что происходит, когда пользователь нажимает кнопку **«+»**:

```
Пользователь
    ↓
Button («+»)
    ↓
Command="{Binding IncrementCommand}"
    ↓
IncrementCommand в MainViewModel
    ↓
Метод Increment() → Counter++
    ↓
Свойство Counter вызывает PropertyChanged
    ↓
Binding в XAML получает уведомление
    ↓
TextBlock с текстом {Binding Counter} обновляется
```

> 💡 **Ключевой момент:** Никакого `label1.Text = counter.ToString()` в коде!  
> Всё происходит автоматически через **Binding**.

---

## 📚 Что показывает этот пример (шпаргалка для студентов)

### 1. XAML-разметка

```xml
<TextBlock Text="{Binding Counter}" FontSize="48"/>
<Button Command="{Binding IncrementCommand}" Content="+"/>
```

### 2. Data Binding

```xml
Text="{Binding Counter}"
```
Связывает TextBlock со свойством `Counter` из ViewModel.

### 3. DataContext

```csharp
DataContext = new MainViewModel();
```
Указывает, откуда брать данные для Binding.

### 4. INotifyPropertyChanged

```csharp
public int Counter
{
    set
    {
        _counter = value;
        PropertyChanged?.Invoke(...);  // Оповещает интерфейс
    }
}
```

### 5. ICommand / RelayCommand

```csharp
public ICommand IncrementCommand { get; }
new RelayCommand(Increment)
```
Вместо `button1_Click` в WinForms.

### 6. MVVM (Model-View-ViewModel)

| Компонент | Где находится | Что делает |
|-----------|---------------|-------------|
| **View** | `MainWindow.xaml` | Интерфейс (кнопки, текст) |
| **ViewModel** | `MainViewModel.cs` | Логика (счётчик, команды) |
| **Model** | (в данном проекте нет) | Данные (появляется в более сложных примерах) |

---

## 🧪 Как использовать этот репозиторий как шпаргалку

### Вспомнить, как создать команду
Открой `Commands/RelayCommand.cs` — это шаблон, который можно копировать в любой проект.

### Вспомнить, как устроен ViewModel
Открой `ViewModels/MainViewModel.cs`:
- Свойство с вызовом `PropertyChanged`
- Команды, инициализируемые в конструкторе
- Приватные методы `Increment()` / `Decrement()`

### Вспомнить, как подключить DataContext
Открой `MainWindow.xaml.cs` — там всего одна строка в конструкторе.

### Вспомнить Binding в XAML
Посмотри, как `TextBlock` и `Button` привязаны к свойствам и командам.

---

## 🏠 Домашнее задание (уровни сложности)

После того как базовый счётчик заработал, можно развивать проект:

### 🔹 Уровень 1 — просто
Добавить кнопку **«Сбросить»** (Reset), которая устанавливает счётчик в 0.

### 🔹 Уровень 2 — средний
Добавить кнопки **«+10»** и **«-10»**.

### 🔹 Уровень 3 — продвинутый
Ограничить значение счётчика: **не меньше 0** (использовать `CanExecute()` в RelayCommand).

### 🔹 Уровень 4 — стилизация
Изменять цвет числа:
- **Зелёный** — положительное
- **Красный** — отрицательное
(через Binding + конвертер или триггер)

### 🔹 Уровень 5 — хранение данных
Сохранять значение счётчика в JSON-файл при закрытии и загружать при запуске.

---

## 🆚 Сравнение с WinForms

| WinForms | WPF в этом проекте |
|----------|---------------------|
| `button1.Click += Button1_Click` | `Command="{Binding IncrementCommand}"` |
| `label1.Text = counter.ToString();` | Автоматическое обновление через `PropertyChanged` |
| Код в `code-behind` (MainWindow.cs) | MVVM — код в `MainViewModel.cs` |
| Нужно вручную обновлять интерфейс | Интерфейс обновляется автоматически по Binding |

---

## 🚀 Запуск проекта

1. Установите **Visual Studio 2022** (или новее) с компонентом **"Разработка классических приложений .NET"**
2. Скачайте или склонируйте репозиторий
3. Откройте файл `.csproj` или папку как проект
4. Нажмите `F5` для запуска

---

## 📌 Что дальше?

После того как счётчик стал понятен, можно переходить к следующему обучающему проекту:

👉 **[Менеджер студентов (Student Manager)](https://github.com/VladimirRepp/WPF---Samples/tree/main/Wpf_StudentManagment)**

Там появляются:
- `ObservableCollection`
- `DataGrid`
- Работа со списками
- Более сложный интерфейс

---

## 📎 Ссылки

- [Исходный код проекта на GitHub](https://github.com/VladimirRepp/WPF---Samples/tree/main/Wpf_Counter)
- [Документация Microsoft по WPF](https://learn.microsoft.com/ru-ru/dotnet/desktop/wpf/)
- [Документация по MVVM](https://learn.microsoft.com/ru-ru/dotnet/architecture/maui/mvvm)

---

> *Этот проект — ваш первый шаг в мир WPF и MVVM. После него вы уже никогда не вернётесь к WinForms-стилю «всё в одном файле». Удачи в изучении!* 🚀