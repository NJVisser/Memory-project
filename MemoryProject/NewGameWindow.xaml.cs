using System.Windows;
using System.Windows.Controls;

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
        
        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            _mainWindow.NewGrid(int.Parse(((Button)sender).Tag.ToString()));
            Close();
        }
    }
}