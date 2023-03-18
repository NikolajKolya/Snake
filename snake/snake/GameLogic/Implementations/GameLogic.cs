using Avalonia.Input;
using snake.Abstract;
using snake.GameLogic.Abstract;
using snake.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake.GameLogic.Implementations
{
    public class GameLogic : IGameLogic
    {
        private ButtonState AorD = ButtonState.NaN;
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
            switch (AorD)
            {
                case ButtonState.NaN:
                    _snake.MoveForward();
                    return;

                case ButtonState.A:
                    _snake.MoveLeft();
                    AorD = ButtonState.NaN;
                    return;

                case ButtonState.D:
                    _snake.MoveRight();
                    AorD = ButtonState.NaN;
                    return;
            }
        }

        public void OnKeyPress(Key key)
        {
            if (key.ToString() == "A")
            {
                AorD = ButtonState.A;
            }
            else if (key.ToString() == "D")
            {
                AorD = ButtonState.D;
            }
            else
            {
                AorD = ButtonState.NaN;
            }
        }

        public void Restart()
        {
            _snake.RestartPosition();
        }

        public void AppleIsEaten()
        {
            _snake.SnakeBecomeBiger();
        }
    }
}
