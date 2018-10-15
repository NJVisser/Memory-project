using System.Windows;

namespace MemoryProject
{
    public partial class MainMenuWindow : Window
    {
        public MainMenuWindow()
        {
            InitializeComponent();
        }

	    private void NewGame(object sender, RoutedEventArgs routedEventArgs)
	    {
		    var mainWindow = new MainGameWindow();
		    mainWindow.Show();
		    var w = new NewGameWindow(mainWindow);
		    w.Show();
		    Close();
	    }

	    private void Settings(object sender, RoutedEventArgs routedEventArgs)
		{
			// TODO: add settings tab
		}

	    private void Exit(object sender, RoutedEventArgs routedEventArgs)
		{
			Close();
		}
	}
}
