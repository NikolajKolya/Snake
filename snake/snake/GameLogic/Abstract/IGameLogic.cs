using Avalonia.Input;
using snake.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake.GameLogic.Abstract
{
    /// <summary>
    /// Game logic interface
    /// </summary>
    internal interface IGameLogic
    {
        /// <summary>
        /// Вызовите меня когда нажата кнопка
        /// </summary>
        void OnKeyPress(Key key);

        /// <summary>
        /// Следующий "шаг" змейки
        /// </summary>
        void NextStep();

        /// <summary>
        /// Вызовите меня, чтобы получить змеиные квадраты
        /// </summary>
        Dictionary<int, SquareState> GetSnakeSquares();
    }
}
