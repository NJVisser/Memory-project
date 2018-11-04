using System.Windows;


namespace MemoryProject
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainGameWindow
    {
        public MainGameWindow()
        {
            InitializeComponent();

            GridManager.Instance.Player1Name = Player1Name;
            GridManager.Instance.Player2Name = Player2Name;
            GridManager.Instance.LiveGameGrid = LiveGameGrid;
            GridManager.Instance.ClockBlock = clocktxtblock;
        }


        private void RestartGame(object sender, RoutedEventArgs e)
        {
            GridManager.Instance.Clear();
            GridManager.Instance.LiveGameGrid = null;
            

            var mainWindow = new MainMenuWindow();
            mainWindow.Show();
            Close();
        }

        private void SaveGame(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(SaveGameManager.Instance.SaveGame("Quick") ?  "Game saved!": "Failed to save game", "Game Save");
        }

        private void LoadGame(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(SaveGameManager.Instance.LoadGame("Quick") ? "Game loaded!": "Failed to load game", "Game Load" );
        }
    }
}