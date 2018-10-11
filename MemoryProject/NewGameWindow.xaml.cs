using System.Windows;

namespace MemoryProject
{
    public partial class NewGameWindow : Window
    {
        private readonly MainWindow _mainWindow;

        public NewGameWindow(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();
        }
        private void ColCount_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _mainWindow.SetColCount((int) e.NewValue);
        }

        private void RowCount_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _mainWindow.SetRowCount((int) e.NewValue);
        }
        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}