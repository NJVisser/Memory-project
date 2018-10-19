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

            var card = ((Image)sender);
            var x = _gameGrid.cards[card.Name];
            card.Source = new BitmapImage(new Uri($"Images/Placeholders/{x.Name}.png", UriKind.Relative));

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