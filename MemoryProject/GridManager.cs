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

        public GridManager(UniformGrid gameGrid)
        {
			_uniformGrid = gameGrid;
        }

        public void NewGrid(int size)
		{
			Clear();
			_gameGrid = GridFactory.InitializeGameGrid(this, _uniformGrid, size, size);
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

		}

        /// <summary>
        ///     Clears the game gird 
        /// </summary>
        internal void Clear()
        {
            if(_uniformGrid != null)
				_uniformGrid.Children.Clear();
        }
    }
}