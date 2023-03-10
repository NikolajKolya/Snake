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
        private Dictionary<Point, SquareState> _RowColumn;
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
                    //RowColumn[new Point(x, y)] = SquareState.Nothing;
                }
            }
        }
    }
}
