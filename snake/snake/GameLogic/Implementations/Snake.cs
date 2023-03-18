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
        private Square _oldTail = new Square();

        private Square _oldHead = new Square();

        private SnakeDirection _snakeGo = SnakeDirection.Up;

        private const int _oneStep = 1;

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
            _snakeGo = SnakeDirection.Up;
        }
        public Dictionary<int, SquareState> GetSnakeSquares()
        {
            var isHitHerself = _snakeSquares
                .Select(sq => sq.GetIndex()) // Select проецирует один объект в другой. Мы берём список квадратов и получем список индексов
                .GroupBy(i => i) // Мы сортируем индексы по группам, типа 1 - столько-то раз встречается, 2 - столько-то раз и т.п.
                .Where(g => g.Count() > 1) // Оставляем только те группы, где больше одного индекса
                .Any(); // Если есть хоть одна такая группа - то мы ударили себя

            if (isHitHerself) // Hit herself
            {
                RestartPosition();
                return new Dictionary<int, SquareState>();
            }

            return _snakeSquares
                    .ToDictionary(sq => sq.GetIndex(), sq => SquareState.Snake);
        }

        public void MoveForward()
        {
            _oldTail = _snakeSquares.First();

            _snakeSquares.RemoveAt(0);

            // Старая голова
            _oldHead = _snakeSquares.Last();

            // Определяем куда поползет змейка
            switch (_snakeGo)
            {
                case SnakeDirection.Up:
                    _snakeSquares.Add(new Square { X = _oldHead.X, Y = _oldHead.Y - _oneStep });
                    break;
                case SnakeDirection.Down:
                    _snakeSquares.Add(new Square { X = _oldHead.X, Y = _oldHead.Y + _oneStep });
                    break;
                case SnakeDirection.Left:
                    _snakeSquares.Add(new Square { X = _oldHead.X - _oneStep, Y = _oldHead.Y });
                    break;
                case SnakeDirection.Right:
                    _snakeSquares.Add(new Square { X = _oldHead.X + _oneStep, Y = _oldHead.Y });
                    break;
            }
        }

        public void MoveLeft()
        {
            ChangeSnakeDirection(false);
        }

        public void MoveRight()
        {
            ChangeSnakeDirection(true);
        }

        // Определяем куда поползет змейка с учетом поворота в лево или в право
        private void ChangeSnakeDirection(bool isTurnRight)
        {
            if (isTurnRight)
            {
                switch (_snakeGo)
                {
                    case SnakeDirection.Up:
                        _snakeGo = SnakeDirection.Right;
                        break;

                    case SnakeDirection.Right:
                        _snakeGo = SnakeDirection.Down;
                        break;

                    case SnakeDirection.Down:
                        _snakeGo = SnakeDirection.Left;
                        break;

                    case SnakeDirection.Left:
                        _snakeGo = SnakeDirection.Up;
                        break;

                    default:
                        throw new InvalidOperationException();
                }
            }
            else
            {
                switch (_snakeGo)
                {
                    case SnakeDirection.Up:
                        _snakeGo = SnakeDirection.Left;
                        break;

                    case SnakeDirection.Left:
                        _snakeGo = SnakeDirection.Down;
                        break;

                    case SnakeDirection.Down:
                        _snakeGo = SnakeDirection.Right;
                        break;

                    case SnakeDirection.Right:
                        _snakeGo = SnakeDirection.Up;
                        break;

                    default:
                        throw new InvalidOperationException();
                }
            }

            MoveForward();
        }

        public void SnakeBecomeBigger()
        {
            _snakeSquares.Insert(0, new Square { X = _oldTail.X, Y = _oldTail.Y });
        }
    }
}
//