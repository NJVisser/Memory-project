using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
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
        private readonly Image[] _clickedCards = new Image[2];
        private bool _isBusy;


        public void NewGrid(int size)
        {
            _gameGrid = GridFactory.Instance.InitializeGameGrid(size, size);
        }

        /// <summary>
        /// Card Clicker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ClickCard(object sender, MouseButtonEventArgs e)
        {
            if (_isBusy) return;

            var img = (Image) sender;
            var card = _gameGrid.cards[img.Name];
            img.Source = new BitmapImage(new Uri($"Images/Placeholders/{card.Name}.png", UriKind.Relative));

            card.IsClicked = true;

            //more arguments need to be added for a more precise check
            if (_check.Name == card.Name)
            {
                _score++;
                _scoreLabel.Content = $"Score: {_score}";
                _check.Name = null;
                card.IsClicked = false;
                _check.IsClicked = false;
                card.IsGone = true;
                _check.IsGone = true;
                return;
            }

            if (string.IsNullOrEmpty(_check.Name))
            {
                _check.Name = card.Name;
                _check.IsClicked = true;
                _clickedCards[0] = img;
                return;
            }

            //deselect incorrect cards so they flip back adn Flip image back to card back
            if (_check.Name != card.Name && _check.IsClicked)
            {
                Mouse.OverrideCursor = Cursors.Wait;
                _isBusy = true;
                _check.Name = null;
                card.IsClicked = false;
                _check.IsClicked = false;
                _clickedCards[1] = img;
                Task.Delay(TimeSpan.FromSeconds(1)).ContinueWith(t => FlipCard(_clickedCards));
            }
        }

        private void FlipCard(Image[] img)
        {
            _isBusy = false;
            try
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    var bgimg = GridFactory.Instance.PlaceholderTheme.BackImageName;
                    foreach (var i in img)
                    {
                        i.Source = new BitmapImage(new Uri($"Images/Placeholders/{bgimg}.png", UriKind.Relative));
                    }

                    Mouse.OverrideCursor = null;
                });
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        ///     Clears the game gird 
        /// </summary>
        internal void Clear()
        {
            _uniformGrid?.Children.Clear();
        }

        internal Label ScoreLabel
        {
            get => _scoreLabel;
            set => _scoreLabel = value;
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