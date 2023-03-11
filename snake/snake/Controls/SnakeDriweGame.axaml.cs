using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using snake.Abstract;
using System;
using System.Collections.Generic;

namespace snake.Controls
{
    public partial class SnakeDriweGame : UserControl
    {
        #region const

        #region Lines

        private readonly SolidColorBrush LineBrush = new SolidColorBrush(new Color(220, 25, 25, 25));
        private const double LinePenThickness = 2.0;

        #endregion

        #region Colors

        private readonly Color EmptyColor = new Color(6, 0, 25, 25);
        private readonly Color SnakeColor = Colors.Green;
        private readonly Color AppleColor = Colors.Blue;

        #endregion

        private const double GridSize = 10;

        #endregion

        private double _scaling;
        private int _width;
        private int _height;

        /// <summary>
        /// Свойство управления словарем
        /// </summary>
        public static readonly AttachedProperty<Dictionary<Point, SquareState>> RowColumnProperty
            = AvaloniaProperty.RegisterAttached<SnakeDriweGame, Interactive, Dictionary<Point, SquareState>>(nameof(RowColumn));

        public Dictionary<Point, SquareState> RowColumn
        {
            get { return GetValue(RowColumnProperty); }
            set { SetValue(RowColumnProperty, value); }
        }
        public SnakeDriweGame()
        {
            // Подписываемся на изменение словаря
            RowColumnProperty.Changed.Subscribe(x => HandleRowColumnChanged(x.Sender, x.NewValue.GetValueOrDefault<bool>()));
            // Подписываемся на изменение свойств окна
            PropertyChanged += OnPropertyChangedListener;
            InitializeComponent();
        }

        private void HandleRowColumnChanged(IAvaloniaObject sender, bool v)
        {
            InvalidateVisual();
        }

        public override void Render(DrawingContext context)
        {
            base.Render(context);
            DrawSquares(context);
            DrawLines(context);
        }

        private void DrawLines(DrawingContext context)
        {
            var pen = new Pen(LineBrush, LinePenThickness);


            // рисуем сетку
            for (double y = 0; y <= GridSize; y++)
            {
                context.DrawLine(pen, new Point(0, _height / GridSize * y), new Point(_width, _height / GridSize * y));
            }

            for (double x = 0; x <= GridSize; x++)
            {
                context.DrawLine(pen, new Point(_width / GridSize * x, 0), new Point(_width / GridSize * x, _height));
            }
        }

        private void DrawSquares(DrawingContext context)
        {
            var emptyBrush = new SolidColorBrush(EmptyColor);
            var snakeBrush = new SolidColorBrush(SnakeColor);
            var appleBrush = new SolidColorBrush(AppleColor);

            var emptyPen = new Pen(emptyBrush, 1);
            var snakePen = new Pen(snakeBrush, 1);
            var applePen = new Pen(appleBrush, 1);


            IBrush squareBrush;
            IPen squarePen;

            for (double y = 0; y < GridSize; y++)
            {
                for (double x = 0; x < GridSize; x++)
                {
                    switch(RowColumn[new Point(x, y)])
                    {
                        case SquareState.Nothing:
                            squareBrush = emptyBrush;
                            squarePen = emptyPen;
                            break;

                        case SquareState.Snake:
                            squareBrush = snakeBrush;
                            squarePen = snakePen;
                            break;

                        case SquareState.Aplle:
                            squareBrush = appleBrush;
                            squarePen = applePen;
                            break;

                        default:
                            throw new InvalidOperationException("Unknown square state!");
                    }

                    // рисуем квадраты
                    context.DrawRectangle(
                        brush: squareBrush,
                        pen: squarePen,
                        rect: new Avalonia.Rect(
                            new Point(x * _width / GridSize, y * _height / GridSize),
                            new Point((x + 1) * _width / GridSize, (y + 1) * _height / GridSize)),
                        radiusX: 0,
                        radiusY: 0,
                        boxShadows: default);
                }
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
