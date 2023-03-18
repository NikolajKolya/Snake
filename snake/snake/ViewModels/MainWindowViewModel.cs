using Avalonia;
using Avalonia.Input;
using ReactiveUI;
using snake.Abstract;
using snake.GameLogic.Abstract;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Timers;
using snake.GameLogic.Implementations;
using System.Reactive;
using snake.GameLogic.Abstract.Models;
using System.Linq;

namespace snake.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IGameLogic _gameLogic;

        private Dictionary<Square, SquareState> _rowColumn = new Dictionary<Square, SquareState>();

        private Timer _stepsTimer;

        private string _startAndStopButtonName;
        

        public ReactiveCommand<Unit, Unit> StartAndStopButton { get; }

        public string StartAndStopButtonName
        {
            get => _startAndStopButtonName;
            set => this.RaiseAndSetIfChanged(ref _startAndStopButtonName, value);
        }

        public Dictionary<Square, SquareState> RowColumn
        {
            get => _rowColumn;
            set => this.RaiseAndSetIfChanged(ref _rowColumn, value);
        }

        public MainWindowViewModel()
        {
            _gameLogic = Program.Di.GetService<IGameLogic>();

            StartAndStopButton = ReactiveCommand.Create(StartStopGame);

            StartAndStopButtonName = "Стaрт";

            ClearGameFiled();

            // Starting timer
            _stepsTimer = new Timer(300);
            _stepsTimer.Enabled = false;
            _stepsTimer.Elapsed += OnStepsTimer;
        }

        /// <summary>
        /// метод кнопки старт/стоп
        /// </summary>
        private void StartStopGame()
        {
            _gameLogic.Restart();

            _stepsTimer.Enabled = !_stepsTimer.Enabled;
            StartAndStopButtonName = (_stepsTimer.Enabled == true ? "Стоп ": "Стaрт");
        }

        private void ClearGameFiled()
        {
            RowColumn = new Dictionary<Square, SquareState>();

            for (int y = 0; y < Constants.Constants.GameFieldSize; y++)
            {
                for (int x = 0; x < Constants.Constants.GameFieldSize; x++)
                {
                    RowColumn[new Square() { X = x, Y = y }] = SquareState.Nothing;
                }
            }
        }

        private void OnStepsTimer(object? sender, ElapsedEventArgs e)
        {
            ClearGameFiled();

            _gameLogic.NextStep();

            // Рисуем яблоко
            var applePlace = _gameLogic.GetApplePlace();
            var appleSquare = RowColumn
                .Single(s => s.Key.X == applePlace.X && s.Key.Y == applePlace.Y);

            RowColumn.Remove(appleSquare.Key);
            RowColumn.Add(new Square() { X = applePlace.X, Y = applePlace.Y }, SquareState.Aplle);

            var snakeSquares = _gameLogic
                .GetSnakeSquares()
                .Select(s => s.Key);
            foreach (var snakeSquare in snakeSquares)
            {
                var currentSnakeSquare = RowColumn
                    .Single(s => s.Key.X == snakeSquare.X && s.Key.Y == snakeSquare.Y);

                RowColumn.Remove(currentSnakeSquare.Key);
                RowColumn.Add(new Square() { X = snakeSquare.X, Y = snakeSquare.Y }, SquareState.Snake);
            }

            RowColumn = new Dictionary<Square, SquareState>(RowColumn);

            _stepsTimer.Start();
        }

        public void OnKeyPress(KeyEventArgs keyEventArgs)
        {
            _gameLogic.OnKeyPress(keyEventArgs.Key);
        }
    }
}
