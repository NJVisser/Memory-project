namespace MemoryProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private const int rowCount = 4;
        private const int colCount = 4;
        GridSummoner grid;

        public MainWindow()
        {
            InitializeComponent();
            grid = new GridSummoner(GameGrid, colCount, rowCount);

        }
    }
}