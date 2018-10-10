using System.Windows;
using System.Windows.Media;

namespace MemoryProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public static int RowCount = 4;
        public static int ColCount = 4;

        public MainWindow()
        {
            InitializeComponent();
            //grid = new GridSummoner(GameGrid, ColCount, RowCount);
        }

        private void MainWindow_NewGame(object sender, RoutedEventArgs re)
        {
            var w = new NewGameWindow();
            w.Show();
            w.Closing += (s, e) =>
            {
                Clear();
                var gridSummoner = new GridSummoner(GameGrid, ColCount, RowCount);
            };
        }

        void Clear()
        {
            GameGrid.Children.Clear();
            GameGrid.RowDefinitions.Clear();
            GameGrid.ColumnDefinitions.Clear();
        }
    }
}