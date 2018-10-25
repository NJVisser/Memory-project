using System.Windows;
using System.Windows.Controls;

namespace MemoryProject
{
    public partial class NewGameWindow : Window
    {
        
        public NewGameWindow()
        {
            InitializeComponent();
        }
        public void Click_Button(object sender, RoutedEventArgs e)
        {
            //Veranderd de namen "player1" en "player2" naar de ingevulde namen
            label1.Content = textbox1.Text;
            label2.Content = textbox2.Text;

        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            GridManager.Instance.PlayerName1.Content = textbox1.Text;
            GridManager.Instance.PlayerName2.Content = textbox2.Text;
            var size = int.Parse(((Button)sender).Tag.ToString());
            GridManager.Instance.NewGrid(size);
            Close();
        }


    }
}