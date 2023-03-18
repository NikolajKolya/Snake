using snake.GameLogic.Abstract.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake.GameLogic.Abstract
{
    public interface IRandomApple
    {
        Square GenеrаteRandomApple(IReadOnlyCollection<Square> snakeSquares);
    }
}
