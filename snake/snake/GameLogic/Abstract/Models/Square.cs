using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake.GameLogic.Abstract.Models
{
    /// <summary>
    /// Один квадрат
    /// </summary>
    public class Square
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int GetIndex()
        {
            return Y * Constants.Constants.GameFieldSize + X;
        }
    }
}
