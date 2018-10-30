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

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            GridManager.Instance.SetPlayerNames(Player1NameInput.Text, Player2NameInput.Text);
            var size = int.Parse(((Button) sender).Tag.ToString());
            GridManager.Instance.NewGrid(size);
            Close();
        }
        
        private void NameUpdated(object sender, TextChangedEventArgs e)
        {
            var textBox = (TextBox) sender;
            //Veranderd de namen "player1" en "player2" naar de ingevulde namen
            if (textBox.Name == "Player1NameInput")
                Player1Name.Content = textBox.Text;
            else
                Player2Name.Content = textBox.Text;
        }

        private void SelectTheme(object sender, SelectionChangedEventArgs e)
        {
            var x = (ComboBoxItem)ThemeSelector.SelectedValue;
            GridFactory.Instance.SetTheme(x.Name);
        }
    }
}