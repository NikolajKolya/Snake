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
        Square OldTail = new Square();

        Square OldHead = new Square();

        private int SnakeDirectionNow = 0;

        private SnakeDirection SnakeGo = SnakeDirection.Up;

        private const int OneStep = 1;

        /// <summary>
        /// Квадраты змейки
        /// </summary>
        private List<Square> _snakeSquares = new List<Square>();

        public Snake()
        {
            RestartPosition();
        }

        /// <summary>
        /// Начальная позиция змейки
        /// </summary>
        public void RestartPosition()
        {
            int center = Constants.Constants.GameFieldSize / 2;

            _snakeSquares.Clear();

            _snakeSquares.Add(new Square { X = center, Y = center });
            _snakeSquares.Add(new Square { X = center, Y = center - 1 });
            _snakeSquares.Add(new Square { X = center, Y = center - 2 });
            SnakeDirectionNow = 0;
            SnakeGo = SnakeDirection.Up;
        }
        public Dictionary<int, SquareState> GetSnakeSquares()
        {
            // Потом почитай про LINQ
            // С помощью try catch ловим Exception который вылетает когда змейка ударяет саму себя
            try
            {
                return
                    _snakeSquares
                    .ToDictionary(sq => sq.GetIndex(), sq => SquareState.Snake);
            }
            catch(Exception ex)
            {
                RestartPosition();
                return new Dictionary<int, SquareState>();
            }

        }

        public void MoveForward()
        {
            OldTail = _snakeSquares.First();

            _snakeSquares.RemoveAt(0);

            // Старая голова
            OldHead = _snakeSquares.Last();

            // Определяем куда поползет змейка
            switch (SnakeGo)
            {
                case SnakeDirection.Up:
                    _snakeSquares.Add(new Square { X = OldHead.X, Y = OldHead.Y - OneStep });
                    break;
                case SnakeDirection.Down:
                    _snakeSquares.Add(new Square { X = OldHead.X, Y = OldHead.Y + OneStep });
                    break;
                case SnakeDirection.Left:
                    _snakeSquares.Add(new Square { X = OldHead.X - OneStep, Y = OldHead.Y });
                    break;
                case SnakeDirection.Right:
                    _snakeSquares.Add(new Square { X = OldHead.X + OneStep, Y = OldHead.Y });
                    break;
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

        // Определяем куда поползет змейка с учетом поворота в лево или в право
        private void ChangeSnakeDirection(int i)
        {
            SnakeDirectionNow = (i == 1 ?
                (SnakeDirectionNow + i <= 3 ? SnakeDirectionNow + i : 0):
                (SnakeDirectionNow + i >= 0 ? SnakeDirectionNow + i : 3));

            SnakeGo = (SnakeDirection)SnakeDirectionNow;
            MoveForward();
        }
        public void SnakeBecomeBiger()
        {
            _snakeSquares.Insert(0, new Square { X = OldTail.X, Y = OldTail.Y });
        }
    }
}
//