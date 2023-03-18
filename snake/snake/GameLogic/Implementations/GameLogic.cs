using Avalonia.Input;
using snake.Abstract;
using snake.GameLogic.Abstract;
using snake.GameLogic.Abstract.Models;
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
        private readonly IRandomApple _randomApple;

        private Square _applePlace;

        public GameLogic(ISnake snake, IRandomApple randomApple)
        {
            _snake = snake;
            _randomApple = randomApple;

            _applePlace = _randomApple.GenеrаteRandomApple(_snake.GetSnakeSquares().Select(s => s.Key).ToList());
        }

        public Dictionary<Square, SquareState> GetSnakeSquares()
        {
            return _snake.GetSnakeSquares();
        }

        public void NextStep()
        {
            switch (AorD)
            {
                case ButtonState.NaN:
                    _snake.MoveForward();
                    break;

                case ButtonState.A:
                    _snake.MoveLeft();
                    AorD = ButtonState.NaN;
                    break;

                case ButtonState.D:
                    _snake.MoveRight();
                    AorD = ButtonState.NaN;
                    break;
            }

            // Находим голову змейки
            var head = _snake
                .GetSnakeSquares()
                .Last();

            if (head.Key.X < 0
                ||
                head.Key.X >= Constants.Constants.GameFieldSize
                ||
                head.Key.Y < 0
                ||
                head.Key.Y >= Constants.Constants.GameFieldSize)
            {
                Restart();
            }

            // Съедание яблока
            if (head.Key.X == _applePlace.X && head.Key.Y == _applePlace.Y)
            {
                AppleIsEaten();
            }
        }

        public void OnKeyPress(Key key)
        {
            if (key == Key.A)
            {
                AorD = ButtonState.A;
            }
            else if (key == Key.D)
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
            _snake.SnakeBecomeBigger();

            _applePlace = _randomApple.GenеrаteRandomApple(_snake.GetSnakeSquares().Select(s => s.Key).ToList());
        }

        public Square GetApplePlace()
        {
            return _applePlace;
        }
    }
}
