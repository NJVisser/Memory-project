using System.Windows;

namespace MemoryProject
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private int _colCount = 4;
        private int _rowCount = 4;
        private readonly GridManager _gridManager;

        public MainWindow()
        {
            InitializeComponent();
            _gridManager = new GridManager(GameGrid);
        }

        public void SetRowCount(int value)
        {
            _rowCount = value;
        }

        public void SetColCount(int value)
        {
            _colCount = value;
        }

        private void MainWindow_NewGame(object sender, RoutedEventArgs re)
        {
            var w = new NewGameWindow(this);
            w.Show();
            w.Closing += (s, e) =>
            {
                _gridManager.Clear();
                _gridManager.InitializeGameGrid(_colCount, _rowCount);
            };
        }
    }
}