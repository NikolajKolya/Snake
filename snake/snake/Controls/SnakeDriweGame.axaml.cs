using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;
using System.Collections.Generic;

namespace snake.Controls
{
    public partial class SnakeDriweGame : UserControl
    {
        #region const
        private readonly SolidColorBrush LinePen = new SolidColorBrush(new Color(100, 0, 0, 255));
        private const int Row = 10;
        #endregion

        private double _scaling;
        private int _width;
        private int _height;
        public SnakeDriweGame()
        {
            // Подписываемся на изменение свойств окна
            PropertyChanged += OnPropertyChangedListener;
            InitializeComponent();
        }
        public override void Render(DrawingContext context)
        {
            base.Render(context);
            DrawLines(context, LinePen);
        }

        private void DrawLines(DrawingContext context, SolidColorBrush LinePen)
        {
            for (double y = 0; y <= Row; y++)
            {
                context.DrawLine(new Pen(LinePen, 4), new Point(0, _height / Row * y), new Point(_width, _height / Row * y));
            }
            for (double x = 0; x <= Row; x++)
            {
                context.DrawLine(new Pen(LinePen, 4), new Point(_width / Row * x , 0), new Point(_width / Row * x, _height));
            }
        }

        /// <summary>
        /// Слушатель изменения свойств окна
        /// </summary>
        private void OnPropertyChangedListener(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property.Name.Equals("Bounds")) // Если меняется размер окна
            {
                // Обработать изменение размера
                OnResize((Rect)e.NewValue);
            }
        }

        /// <summary>
        /// Вызывается при измененеии размеров окна
        /// </summary>
        private void OnResize(Rect bounds)
        {
            _scaling = VisualRoot.RenderScaling;

            _width = (int)(bounds.Width * _scaling);
            _height = (int)(bounds.Height * _scaling);
        }
    }
}
