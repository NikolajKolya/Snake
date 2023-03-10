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
        private readonly SolidColorBrush LinePen = new SolidColorBrush(new Color(100, 0, 0, 255));
        private const double Row = 10;
        private IBrush brush = new SolidColorBrush(new Color(150, 0, 0, 255));
        #endregion

        private double _scaling;
        private int _width;
        private int _height;

        /// <summary>
        /// Свойство управления зеленым огнём
        /// </summary>
        public static readonly AttachedProperty<Dictionary<Point, SquareState>> RowColumnProperty
            = AvaloniaProperty.RegisterAttached<SnakeDriweGame, Interactive, Dictionary<Point, SquareState>>(nameof(RowColumn));

        /// <summary>
        /// Горит-ли зеленый огонь
        /// </summary>
        public Dictionary<Point, SquareState> RowColumn
        {
            get { return GetValue(RowColumnProperty); }
            set { SetValue(RowColumnProperty, value); }
        }
        public SnakeDriweGame()
        {
            RowColumnProperty.Changed.Subscribe(x => HandleRowColumnChanged(x.Sender, x.NewValue.GetValueOrDefault<bool>()));
            // Подписываемся на изменение свойств окна
            PropertyChanged += OnPropertyChangedListener;
            InitializeComponent();
        }

        private void HandleRowColumnChanged(IAvaloniaObject sender, bool v)
        {
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
            for(double y = 0; y < 10; y++)
            {
                for (double x = 0; x < 10; x++)
                {/*
                    switch (RowColumn[new Point(x, y)])
                    {
                        case SquareState.Nothing:
                            return;
                        case SquareState.Snake:
                            brush = new SolidColorBrush(Colors.Red);
                            return;
                        case SquareState.Aplle:
                            brush = new SolidColorBrush(Colors.Green);
                            return;
                    }*/
                    context.DrawRectangle(brush,
                        new Pen(new SolidColorBrush(new Color(0, 0, 0, 0)), 1),
                        new Avalonia.Rect(new Point(x * _width / Row, y * _height / Row),
                        new Point((x + 1) * _width / Row, (y + 1) * _height / Row)),
                        900,
                        default);
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
