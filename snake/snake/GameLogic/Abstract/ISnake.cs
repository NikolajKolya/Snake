using snake.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake.GameLogic.Abstract
{
    public interface ISnake
    {
        Dictionary<int, SquareState> GetSnakeSquares();

        void SnakeBecomeBigger();

        void RestartPosition();

        void MoveForward();

        void MoveLeft();

        void MoveRight();
    }
}
