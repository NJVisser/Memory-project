using System.Windows;

namespace MemoryProject
{
    public partial class NewGameWindow : Window
    {
        public NewGameWindow()
        {
            InitializeComponent();
        }

        private void ColCount_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MainWindow.ColCount = (int) e.NewValue;
        }

        private void RowCount_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MainWindow.RowCount = (int) e.NewValue;
        }
        
        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
