using System;
using System.Collections.Generic;
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
        private Card _check;
        private readonly Image[] _clickedCards = new Image[2];
        private bool _isBusy;

        internal Label ScoreLabel { get; set; }
        internal SingleGame LiveGame { get; set; } = new SingleGame {Grid = new Dictionary<string, Card>()};
        internal UniformGrid LiveGameGrid { get; set; }
        internal Label Player1Name { get; set; }
        internal Label Player2Name { get; set; }

        public void NewGrid(int size)
        {
            LiveGame.Grid = GridFactory.Instance.GenerateGameGrid(size, size);
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
            var card = LiveGame.Grid[img.Name];
            img.Source = new BitmapImage(new Uri($"Images/Placeholders/{card.Name}.png", UriKind.Relative));

            card.IsClicked = true;

            if (_check == null)
            {
                _check = card;
                _check.IsClicked = true;
                _clickedCards[0] = img;
                return;
            }

            if (_check.Name == card.Name)
            {
                LiveGame.Score++;
                ScoreLabel.Content = $"Score: {LiveGame.Score}";
                card.IsClicked = _check.IsClicked = false;
                card.IsGone = _check.IsGone = true;
            }
            //deselect incorrect cards so they flip back adn Flip image back to card back
            else
            {
                Mouse.OverrideCursor = Cursors.Wait;
                _isBusy = true;
                card.IsClicked = _check.IsClicked = false;
                _clickedCards[1] = img;

                Task.Delay(TimeSpan.FromSeconds(1)).ContinueWith(t => FlipCard(_clickedCards));
            }

            _check = null;
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
            LiveGame = new SingleGame {Grid = new Dictionary<string, Card>()};
            LiveGameGrid?.Children.Clear();
        }

        internal void SetPlayerNames(string p1, string p2)
        {
            Player1Name.Content = p1;
            LiveGame.Player1Name = p1;
            Player2Name.Content = p2;
            LiveGame.Player2Name = p2;
        }

        #region Singleton

        private static readonly Lazy<GridManager> LazyGridManager =
            new Lazy<GridManager>(() => new GridManager());

        public static GridManager Instance => LazyGridManager.Value;

        private GridManager()
        {
        }

        #endregion
    }
}