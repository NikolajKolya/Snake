using Avalonia.Controls;
using Avalonia.Input;
using snake.ViewModels;
using System;

namespace snake.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(MainWindowViewModel dataContext)
        {
            InitializeComponent();

            DataContext = dataContext;
        }

        /// <summary>
        /// Обработчик нажатия на кнопки
        /// </summary>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            ((MainWindowViewModel)DataContext).OnKeyPress(e);
        }
    }
}
