using System.Windows;
using AGC.Tools;

namespace MemoryProject
{
    public partial class MainMenuWindow : Window
    {
        public MainMenuWindow()
        {
            InitializeComponent();
            AGCTools.SetupLogger();
        }

        private void NewGame(object sender, RoutedEventArgs routedEventArgs)
        {
            var mainWindow = new MainGameWindow();
            mainWindow.Show();
            new NewGameWindow().Show();
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