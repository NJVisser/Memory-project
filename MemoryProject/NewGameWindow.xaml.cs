using System.Windows;
using System.Windows.Controls;

namespace MemoryProject
{
    public partial class NewGameWindow : Window
    {
        
        public NewGameWindow()
        {
            InitializeComponent();
        }
        
        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
			var size = int.Parse(((Button)sender).Tag.ToString());
            GridManager.Instance.NewGrid(size);
            Close();
        }
    }
}