using System.Linq;
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

            var hsl = SaveGameManager.Instance.GetHighScoreList();
            hsl?.OrderByDescending(hsi => hsi.Score).ToList()
                .ForEach(e => HighScoreList.Items.Add($"{e.PlayerName}: {e.Score}"));
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
            Endscreen win = new Endscreen();
            win.Show();
        }

        private void Exit(object sender, RoutedEventArgs routedEventArgs)
        {
            Close();
        }
    }
}