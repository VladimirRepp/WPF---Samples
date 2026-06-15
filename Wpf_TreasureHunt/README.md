# 🗺️ Охота за сокровищем — WPF + MVVM игровой проект

Этот проект создан в **учебных целях** как третий шаг в освоении **WPF и MVVM** после «Счётчика» и «Менеджера студентов».  

Здесь мы отходим от классических бизнес-приложений и делаем **игру с простой, но наглядной логикой**. Проект показывает, как с помощью WPF + MVVM можно реализовать интерактивное приложение с динамическим интерфейсом, игровым полем и подсказками.

В папке Img находятся примеры работы. 

---

## 🎯 Цель игры

> Найдите спрятанное сокровище на игровом поле 5×5.

- Нажимайте на клетки — в них появляются **подсказки** (❄️, 🌤️, 🔥, 💰).
- Чем ближе к сокровищу, тем «теплее» подсказка.
- Побеждает тот, кто найдёт сокровище за **минимальное количество ходов**.

---

## 🧠 Что нового студенты узнают на этом проекте

По сравнению с предыдущими проектами, здесь вводятся:

| Элемент / концепция | Зачем нужна |
|---------------------|-------------|
| `ItemsControl` | Контейнер для динамического набора элементов (игровое поле) |
| `UniformGrid` | Сетка с одинаковыми ячейками (удобно для поля 5×5) |
| `DataTemplate` | Шаблон отображения каждой клетки (Button с подсказкой) |
| `CommandParameter` | Передача параметра в команду (какую клетку открыли) |
| `RelativeSource` | Привязка к DataContext окна изнутри DataTemplate |
| Игровая логика | Random, манхэттенское расстояние, состояние игры |

---

## 🧱 Структура проекта (MVVM)

```
Wpf_TreasureHunt/
│
├── Models/
│   └── Cell.cs                 # Модель одной клетки (координаты, текст, открыта/закрыта)
│
├── Commands/
│   └── RelayCommand.cs         # Реализация ICommand с параметром object?
│
├── ViewModels/
│   └── MainViewModel.cs        # Логика игры: поле, сокровище, ходы, команды
│
├── Resources/
│   └── Styles.xaml             # Стили кнопок, окна, игровых клеток
│
├── MainWindow.xaml             # Интерфейс: заголовок, статистика, поле
├── MainWindow.xaml.cs          # Только DataContext = new MainViewModel()
└── App.xaml
```

---

## 🔁 Как работает MVVM в этой игре

### 1. **Model** (`Cell.cs`)
Хранит данные о клетке:
- `Row`, `Column` — позиция
- `Text` — что отображать (?, ❄️, 🌤️, 🔥, 💰)
- `IsOpened` — открыта ли уже клетка
- Реализует `INotifyPropertyChanged` для обновления UI.

### 2. **ViewModel** (`MainViewModel.cs`)
- Содержит `ObservableCollection<Cell>` — всё игровое поле.
- Управляет случайным расположением сокровища.
- Считает количество ходов (`MovesCount`).
- Имеет команды:
  - `OpenCellCommand` — открыть клетку, вычислить расстояние, показать подсказку.
  - `NewGameCommand` — сбросить игру, перемешать сокровище.
- Меняет `GameStatus` при победе.

### 3. **View** (`MainWindow.xaml`)
- Привязан к `MainViewModel` через `DataContext`.
- Использует `ItemsControl` + `UniformGrid` для отображения поля.
- Каждая клетка рисуется через `DataTemplate` (кнопка с текстом).
- Команде `OpenCellCommand` через `CommandParameter` передаётся сама клетка.

---

## 🧪 Ключевые фрагменты кода (шпаргалка)

### Привязка игрового поля

```xml
<ItemsControl ItemsSource="{Binding Cells}">
    <ItemsControl.ItemsPanel>
        <ItemsPanelTemplate>
            <UniformGrid Rows="5" Columns="5"/>
        </ItemsPanelTemplate>
    </ItemsControl.ItemsPanel>

    <ItemsControl.ItemTemplate>
        <DataTemplate>
            <Button Content="{Binding Text}"
                    Command="{Binding DataContext.OpenCellCommand,
                              RelativeSource={RelativeSource AncestorType=Window}}"
                    CommandParameter="{Binding}"/>
        </DataTemplate>
    </ItemsControl.ItemTemplate>
</ItemsControl>
```

### Передача параметра в команду

```csharp
private void OpenCell(object? parameter)
{
    if (parameter is not Cell cell) return;
    if (cell.IsOpened) return;

    cell.IsOpened = true;
    MovesCount++;

    int distance = Math.Abs(cell.Row - _treasureRow) +
                   Math.Abs(cell.Column - _treasureColumn);

    if (distance == 0) { cell.Text = "💰"; GameStatus = $"Победа за {MovesCount} ходов!"; }
    else if (distance == 1) cell.Text = "🔥";
    else if (distance <= 3) cell.Text = "🌤️";
    else cell.Text = "❄️";
}
```

### Расстояние (манхэттенское)

```csharp
int distance = Math.Abs(cell.Row - treasureRow) + 
               Math.Abs(cell.Column - treasureColumn);
```

---

## 🧩 Дополнительные задания для самостоятельной работы

После того как базовая версия работает, студентам можно предложить:

### 🔹 Лёгкий уровень
- Добавить отображение **лучшего результата** (минимальное количество ходов).

### 🔹 Средний уровень
- Добавить выбор **размера поля** (5×5, 7×7, 10×10).
- Добавить **таймер** (сколько времени идёт поиск).

### 🔹 Сложный уровень
- После победы **открывать всё поле** (показать все подсказки).
- Добавить **несколько сокровищ** (нужно найти всё).

### 🔹 Очень сложный уровень
- Добавить **систему очков** (чем быстрее и меньше ходов, тем больше очков).

---

## 🆚 Сравнение с предыдущими проектами

| Проект | Основная цель | Что нового |
|--------|---------------|-------------|
| Счётчик | Первое знакомство с Binding, Command | Минимальный MVVM |
| Менеджер студентов | Работа со списками, DataGrid | ObservableCollection, редактирование |
| **Охота за сокровищем** | **Игровая логика, динамическое поле, параметры команд** | `ItemsControl`, `DataTemplate`, `CommandParameter`, `UniformGrid` |

---

## 🚀 Запуск проекта

1. Установите **Visual Studio 2022** (или новее) с .NET Desktop Development.
2. Склонируйте репозиторий или скачайте исходники.
3. Откройте `.csproj` или папку как проект.
4. Нажмите `F5` — игра запустится.

---

## 📎 Полезные ссылки

- [Исходный код на GitHub](https://github.com/VladimirRepp/WPF---Samples/tree/main/Wpf_TreasureHunt)
- [Официальная документация Microsoft по WPF](https://learn.microsoft.com/ru-ru/dotnet/desktop/wpf/)
- [MVVM Pattern](https://learn.microsoft.com/ru-ru/dotnet/architecture/maui/mvvm)

---

## 🎓 Резюме для студента

> Этот проект — мост между «деловыми» приложениями (CRUD, списки) и **игровыми механиками**.  
> Вы научитесь динамически создавать интерфейс, передавать данные в команды и реализовывать простой AI подсказок.  
> После него вы без труда сможете сделать «Сапёра», «Крестики-нолики» или «Морской бой» на WPF + MVVM.

**Удачи в поиске сокровищ!** 🏆💰