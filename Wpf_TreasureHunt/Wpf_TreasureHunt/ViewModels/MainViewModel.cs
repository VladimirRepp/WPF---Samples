using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Wpf_TreasureHunt.Commands;
using Wpf_TreasureHunt.Models;

namespace Wpf_TreasureHunt.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private const int GridSize = 5;

        private readonly Random _random = new();

        private int _treasureRow;
        private int _treasureColumn;

        private string _gameStatus = "Найдите сокровище";

        private int _movesCount;

        public ObservableCollection<Cell> Cells { get; }

        public ICommand OpenCellCommand { get; }

        public ICommand NewGameCommand { get; }

        public ICommand RoolsGameCommand { get; }

        public string GameStatus
        {
            get => _gameStatus;
            set
            {
                _gameStatus = value;
                OnPropertyChanged(nameof(GameStatus));
            }
        }

        public int MovesCount
        {
            get => _movesCount;
            set
            {
                _movesCount = value;
                OnPropertyChanged(nameof(MovesCount));
            }
        }

        public MainViewModel()
        {
            Cells = new ObservableCollection<Cell>();

            OpenCellCommand = new RelayCommand(OpenCell);

            NewGameCommand = new RelayCommand(_ => NewGame());

            RoolsGameCommand = new RelayCommand(_ => RoolsGame());

            NewGame();
        }

        private void NewGame()
        {
            Cells.Clear();

            MovesCount = 0;

            GameStatus = "Найдите сокровище";

            _treasureRow = _random.Next(GridSize);
            _treasureColumn = _random.Next(GridSize);

            for (int row = 0; row < GridSize; row++)
            {
                for (int column = 0; column < GridSize; column++)
                {
                    Cells.Add(new Cell
                    {
                        Row = row,
                        Column = column
                    });
                }
            }
        }

        private void RoolsGame()
        {
            MessageBox.Show(
                    "ОХОТА ЗА СОКРОВИЩЕМ\n\n" +
                    "Цель игры:\n" +
                    "Найдите спрятанное сокровище на игровом поле.\n\n" +

                    "Как играть:\n" +
                    "• Нажимайте на клетки поля.\n" +
                    "• После открытия клетки вы получите подсказку.\n\n" +

                    "Подсказки:\n" +
                    "❄️ Холодно - сокровище далеко.\n" +
                    "🌤️ Тепло - вы приближаетесь.\n" +
                    "🔥 Горячо - сокровище совсем рядом.\n" +
                    "💰 Сокровище найдено.\n\n" +

                    "Побеждает игрок, который найдет сокровище за минимальное количество ходов.",
                    "Правила игры",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
        }

        private void OpenCell(object? parameter)
        {
            if (parameter is not Cell cell)
                return;

            if (cell.IsOpened)
                return;

            cell.IsOpened = true;

            MovesCount++;

            int distance =
                Math.Abs(cell.Row - _treasureRow) +
                Math.Abs(cell.Column - _treasureColumn);

            if (distance == 0)
            {
                cell.Text = "💰";

                GameStatus =
                    $"Победа за {MovesCount} ходов!";
            }
            else if (distance == 1)
            {
                cell.Text = "🔥";
            }
            else if (distance <= 3)
            {
                cell.Text = "🌤️";
            }
            else
            {
                cell.Text = "❄️";
            }
        }

        public event PropertyChangedEventHandler?
            PropertyChanged;

        private void OnPropertyChanged(
            string propertyName)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}