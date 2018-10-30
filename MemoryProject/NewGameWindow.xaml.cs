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


			switch (((Button)sender).Name.ToString())
			{
				case "X4Y4":
					GridManager.Instance.NewGrid(4, 4);
					break;
				case "X4Y5":
					GridManager.Instance.NewGrid(4, 5);
					break;
				case "X6Y6":
					GridManager.Instance.NewGrid(6, 6);
					break;
				case "X5Y8":
					GridManager.Instance.NewGrid(5, 8);
					break;
			}
           
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