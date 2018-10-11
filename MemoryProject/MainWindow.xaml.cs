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

        public MainWindow()
        {
            InitializeComponent();
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
                Clear();
                var unused = new GridSummoner(GameGrid, _rowCount, _colCount);
            };
        }

        private void Clear()
        {
            GameGrid.Children.Clear();
            GameGrid.RowDefinitions.Clear();
            GameGrid.ColumnDefinitions.Clear();
        }
    }
}