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
        private ButtonState aORd = ButtonState.NaN;
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
            switch (aORd)
            {
                case ButtonState.NaN:
                    _snake.MoveForward();
                    return;

                case ButtonState.A:
                    _snake.MoveLeft();
                    aORd = ButtonState.NaN;
                    return;

                case ButtonState.D:
                    _snake.MoveRight();
                    aORd = ButtonState.NaN;
                    return;
            }
        }

        public void OnKeyPress(Key key)
        {
            if (key.ToString() == "A")
            {
                aORd = ButtonState.A;
            }
            else if (key.ToString() == "D")
            {
                aORd = ButtonState.D;
            }
            else
            {
                aORd = ButtonState.NaN;
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
