using snake.Abstract;
using snake.GameLogic.Abstract;
using snake.GameLogic.Abstract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake.GameLogic.Implementations
{
    public class Snake : ISnake
    {
        private int SnakeDirectionNow = 0;
        private SnakeDirection SnakeGo = SnakeDirection.Up;

        /// <summary>
        /// Квадраты змейки
        /// </summary>
        private List<Square> _snakeSquares = new List<Square>();

        public Snake()
        {
            int center = Constants.Constants.GameFieldSize / 2;

            _snakeSquares.Add(new Square { X = center, Y = center });
            _snakeSquares.Add(new Square { X = center, Y = center - 1 });
            _snakeSquares.Add(new Square { X = center, Y = center - 2 });
        }

        public Dictionary<int, SquareState> GetSnakeSquares()
        {
            // Потом почитай про LINQ
            return _snakeSquares
                .ToDictionary(sq => sq.GetIndex(), sq => SquareState.Snake);
        }

        public void MoveForward()
        {
            _snakeSquares.RemoveAt(0);

            // Старая голова
            var oldHead = _snakeSquares.Last();

            switch (SnakeGo)
            {
                case SnakeDirection.Up:
                    _snakeSquares.Add(new Square { X = oldHead.X, Y = oldHead.Y - 1 });
                    return;
                case SnakeDirection.Down:
                    _snakeSquares.Add(new Square { X = oldHead.X, Y = oldHead.Y + 1 });
                    return;
                case SnakeDirection.Left:
                    _snakeSquares.Add(new Square { X = oldHead.X - 1, Y = oldHead.Y });
                    return;
                case SnakeDirection.Right:
                    _snakeSquares.Add(new Square { X = oldHead.X + 1, Y = oldHead.Y });
                    return;
            }
        }

        public void MoveLeft()
        {
            ChangeSnakeDirection(-1);
        }

        public void MoveRight()
        {
            ChangeSnakeDirection(1);
        }
        private void ChangeSnakeDirection(int i)
        {
            SnakeDirectionNow = (i == 1 ?
                (SnakeDirectionNow + i <= 3 ? SnakeDirectionNow + i : 0):
                (SnakeDirectionNow + i >= 0 ? SnakeDirectionNow + i : 3));

            SnakeGo = (SnakeDirection)SnakeDirectionNow;
            MoveForward();
        }
    }
}
//