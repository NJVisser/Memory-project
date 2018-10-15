using System.Windows;

namespace MemoryProject
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainGameWindow
    {
        private readonly GridManager _gridManager;

        public MainGameWindow()
        {
            InitializeComponent();
            _gridManager = new GridManager(GameGrid);
        }

        public void NewGrid(int size)
        {
            _gridManager.Clear();
            _gridManager.InitializeGameGrid(size, size);
        }
    }
}