using System.Windows;
using System.Windows.Controls;

namespace MemoryProject
{
    public partial class NewGameWindow : Window
    {
        private readonly MainGameWindow _mainGameWindow;

        public NewGameWindow(MainGameWindow mainGameWindow)
        {
            _mainGameWindow = mainGameWindow;
            InitializeComponent();
        }
        
        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            _mainGameWindow.NewGrid(int.Parse(((Button)sender).Tag.ToString()));
            Close();
        }
    }
}