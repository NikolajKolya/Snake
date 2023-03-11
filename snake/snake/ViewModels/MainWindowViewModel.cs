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
        private Dictionary<int, SquareState> _rowColumn = new Dictionary<int, SquareState>();
        public Dictionary<int, SquareState> RowColumn
        {
            get => _rowColumn;
            set => this.RaiseAndSetIfChanged(ref _rowColumn, value);
        }
        public MainWindowViewModel()
        {
            for(int y = 0; y < Constants.Constants.GameFieldSize; y++)
            {
                for(int x = 0; x < Constants.Constants.GameFieldSize; x++)
                {
                    _rowColumn[y * Constants.Constants.GameFieldSize + x] = SquareState.Nothing;
                }
            }

            _rowColumn[5 * Constants.Constants.GameFieldSize + 5] = SquareState.Snake;
            _rowColumn[6 * Constants.Constants.GameFieldSize + 5] = SquareState.Snake;
            _rowColumn[7 * Constants.Constants.GameFieldSize + 5] = SquareState.Snake;
            _rowColumn[2 * Constants.Constants.GameFieldSize + 3] = SquareState.Aplle;
        }
    }
}
