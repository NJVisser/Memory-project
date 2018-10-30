using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MemoryProject
{
    /// <summary>
    /// Interaction logic for Endscreen.xaml
    /// </summary>
    public partial class Endscreen : Window
    {
        private MainMenuWindow mainMenuWindow;

        public Endscreen(GridManager gridManager)
        {
            InitializeComponent();
            var hsl = SaveGameManager.Instance.GetHighScoreList();
            hsl?.OrderByDescending(hsi => hsi.Score).ToList()
                .ForEach(e => HighScoreList.Items.Add($"{e.PlayerName}: {e.Score}"));
        }

        public Endscreen(MainMenuWindow mainMenuWindow)
        {
            this.mainMenuWindow = mainMenuWindow;
        }
    }
}
