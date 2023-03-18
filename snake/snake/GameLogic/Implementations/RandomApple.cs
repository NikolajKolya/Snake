using snake.GameLogic.Abstract;
using snake.GameLogic.Abstract.Models;
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

        public Square GenеrаteRandomApple(IReadOnlyCollection<Square> snakeSquares)
        {
            while (true)
            {
                var potentialApple = new Square()
                {
                    X = _random.Next(0, Constants.Constants.GameFieldSize),
                    Y = _random.Next(0, Constants.Constants.GameFieldSize),
                };

                if (!snakeSquares
                    .Any(s => s.X == potentialApple.X && s.Y == potentialApple.Y))
                {
                    return potentialApple;
                }
            }
        }
    }
}
