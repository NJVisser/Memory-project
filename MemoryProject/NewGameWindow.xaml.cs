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
			var size = int.Parse(((Button)sender).Tag.ToString());
			_mainGameWindow._gridManager.NewGrid(size);
            Close();
        }
    }
}