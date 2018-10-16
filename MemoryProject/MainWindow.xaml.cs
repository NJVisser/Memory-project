using MemoryProject.Data;

namespace MemoryProject
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainGameWindow
    {
        public readonly GridManager _gridManager;

        public MainGameWindow()
        {
            InitializeComponent();
            _gridManager = new GridManager(LiveGameGrid);
        }
    }
}