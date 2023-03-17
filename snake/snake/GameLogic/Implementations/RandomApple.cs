using snake.GameLogic.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake.GameLogic.Implementations
{
    internal class RandomApple: IRandomApple
    {
        private readonly Random _random = new Random();
        public int GenreateRandomApple()
        {
            return _random.Next(0, Constants.Constants.GameFieldSize * Constants.Constants.GameFieldSize + 1);
        }
    }
}
