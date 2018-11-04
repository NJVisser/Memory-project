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

        public Endscreen()
        {
            InitializeComponent();
            Player1Name.Content = GridManager.Instance.LiveGame.Player1Name;
            Player2Name.Content = GridManager.Instance.LiveGame.Player2Name;
            scorep1.Content = "Score: " + GridManager.Instance.LiveGame.ScoreP1;
            scorep2.Content = "Score: " + GridManager.Instance.LiveGame.ScoreP2;
            var hsl = SaveGameManager.Instance.GetHighScoreList();
            hsl?.OrderByDescending(hsi => hsi.Score).ToList()
                .ForEach(e => HighScoreList.Items.Add($"{e.PlayerName}: {e.Score}"));
        }

    }
}
