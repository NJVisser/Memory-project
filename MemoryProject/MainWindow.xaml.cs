using System.Windows;

namespace MemoryProject
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly GridManager _gridManager;

        public MainWindow()
        {
            InitializeComponent();
            _gridManager = new GridManager(GameGrid);
        }

        public void NewGrid(int size)
        {
            _gridManager.Clear();
            _gridManager.InitializeGameGrid(size, size);
        }

        private void MainWindow_NewGame(object sender, RoutedEventArgs re)
        {
            var w = new NewGameWindow(this);
            w.Show();
        }
    }
}