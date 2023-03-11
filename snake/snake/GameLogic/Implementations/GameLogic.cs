using Avalonia.Input;
using snake.Abstract;
using snake.GameLogic.Abstract;
using snake.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake.GameLogic.Implementations
{
    public class GameLogic : IGameLogic
    {
        private readonly ISnake _snake;

        public GameLogic(ISnake snake)
        {
            _snake = snake;
        }

        public Dictionary<int, SquareState> GetSnakeSquares()
        {
            return _snake.GetSnakeSquares();
        }

        public void NextStep()
        {
            // Пока-что тупо вперед
            _snake.MoveForward();
        }

        public void OnKeyPress(Key key)
        {
        }
    }
}
