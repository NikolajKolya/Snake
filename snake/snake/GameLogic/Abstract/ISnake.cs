using snake.Abstract;
using snake.GameLogic.Abstract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake.GameLogic.Abstract
{
    public interface ISnake
    {
        Dictionary<Square, SquareState> GetSnakeSquares();

        void SnakeBecomeBigger();

        void RestartPosition();

        void MoveForward();

        void MoveLeft();

        void MoveRight();
    }
}
