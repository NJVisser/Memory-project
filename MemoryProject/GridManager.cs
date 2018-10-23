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
        private int _score;
        private Label _scoreLabel;
        private Card _check;

	   
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

            x.IsClicked = true;

            //more arguments need to be added for a more precise check
            if (_check.Name == x.Name)
            {
                _score++;
                _scoreLabel.Content = $"Score: {_score}";
                _check.Name = null;
                x.IsClicked = false;
                _check.IsClicked = false;
                x.IsGone = true;
                _check.IsGone = true;
                return;
            }

            if (string.IsNullOrEmpty(_check.Name)) //'check.Name == ""' *because in data the value is set to it by default
            {
                _check.Name = x.Name;
                _check.IsClicked = true;
                return;
            }

            //deselect incorrect cards so they flip back adn Flip image back to card back
            if(_check.Name != x.Name && _check.IsClicked)
            {
                x.IsClicked = false;
                _check.IsClicked = false;
                //some method to change cards back to "Carback"
            }
        }

        /// <summary>
        ///     Clears the game gird 
        /// </summary>
        internal void Clear()
        {
            _uniformGrid?.Children.Clear();
        }
	    
        internal void SetScoreLabel(Label s)
        {
            _scoreLabel = s;
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