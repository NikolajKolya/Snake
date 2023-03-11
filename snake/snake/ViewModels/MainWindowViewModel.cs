using Avalonia;
using ReactiveUI;
using snake.Abstract;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace snake.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private Dictionary<Point, SquareState> _RowColumn = new Dictionary<Point, SquareState>();
        public Dictionary<Point, SquareState> RowColumn
        {
            get => _RowColumn;
            set => this.RaiseAndSetIfChanged(ref _RowColumn, value);
        }
        public MainWindowViewModel()
        {
            for(int y = 0; y < 10; y++)
            {
                for(int x = 0; x < 10; x++)
                {
                    _RowColumn[new Point(x, y)] = SquareState.Nothing;
                }
            }
            _RowColumn[new Point(5, 5)] = SquareState.Snake;
            _RowColumn[new Point(5, 6)] = SquareState.Snake;
            _RowColumn[new Point(5, 7)] = SquareState.Snake;
            _RowColumn[new Point(3, 2)] = SquareState.Aplle;
        }
    }
}
