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
            GridManager.Instance.LiveGameGrid = LiveGameGrid;
            GridManager.Instance.SetScoreLabel(Score1);
        }
    }
}