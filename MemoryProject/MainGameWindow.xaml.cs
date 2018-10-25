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

            GridManager.Instance.PlayerName1 = label1;
            GridManager.Instance.PlayerName2 = label2;
            GridManager.Instance.LiveGameGrid = LiveGameGrid;
            GridManager.Instance.ScoreLabel = Score1;
        }


        private void RestartGame(object sender, RoutedEventArgs e)
        {
            GridManager.Instance.Clear();
            GridManager.Instance.LiveGameGrid = null;
            GridManager.Instance.ScoreLabel = null;

            var mainWindow = new MainMenuWindow();
            mainWindow.Show();
            Close();

        }
    }
}