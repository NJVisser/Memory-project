using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using MemoryProject.Data;

namespace MemoryProject
{
    public class GridManager
    {
        private UniformGrid _uniformGrid;
        private GameGrid _gameGrid;
        int score = 0;
        Card check = new Card();

	    

        public void NewGrid(int size)
        {
            Clear();
            _gameGrid = GridFactory.Instance.InitializeGameGrid(size, size);
        }

		/// <summary>
		/// Card Clicker
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void ClickCard(object sender, MouseButtonEventArgs e)
        {
            //((Image) sender).Source = new BitmapImage(new Uri("Images/Placeholders/CSharp.jpg", UriKind.Relative));

            var Card = ((Image)sender);
			var x = _gameGrid.cards[Card.Name];
			Card.Source = new BitmapImage(new Uri($"Images/Placeholders/{x.Name}.png", UriKind.Relative));

            //x.IsClicked = true;

            //more arguements need to be added for a more precise check
            if (check.Name == x.Name)
            {
                score++;
                check.Name = null;
                //x.IsClicked = false;
                return;
            }
            else if (check.Name == null || check.Name == "") //'check.Name == ""' *because in data the value is set to it by default
            {
                check.Name = x.Name;
                check.IsClicked = true;
            }

            //trying to deselect incorrectcards so they flip back
            if(check.Name != x.Name && x.IsClicked == true && check.IsClicked == true)
            {
                x.IsClicked = false;
                check.IsClicked = false;
            }
        }

        /// <summary>
        ///     Clears the game gird 
        /// </summary>
        internal void Clear()
        {
            _uniformGrid?.Children.Clear();
        }
	    
	    
        public UniformGrid LiveGameGrid
        {
            get => _uniformGrid;
            set => _uniformGrid = value;
        }
	    
        private static readonly Lazy<GridManager> LazyGridManager =
            new Lazy<GridManager>(() => new GridManager());
    
        public static GridManager Instance => LazyGridManager.Value;

        private GridManager()
        {
        }
    }
}