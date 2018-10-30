using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using AGC.Tools;
using MemoryProject.Data;

namespace MemoryProject
{
    public class GridManager
    {
        private Card _check;
        private readonly Image[] _clickedCards = new Image[2];
        private bool _isBusy;

        internal SingleGame LiveGame { get; set; } = new SingleGame {Grid = new Dictionary<string, Card>()};

        internal UniformGrid LiveGameGrid { get; set; }

        internal Label Player1Name { get; set; }
        internal Label Player2Name { get; set; }

        private readonly SolidColorBrush _green =
            new SolidColorBrush(Color.FromRgb(Byte.MinValue, Byte.MaxValue, Byte.MinValue));

        private readonly SolidColorBrush _red =
            new SolidColorBrush(Color.FromRgb(Byte.MaxValue, Byte.MinValue, Byte.MinValue));

        private Turn _playerTurn;

        private string _themeName;

        public void NewGrid(int size)
        {
            LiveGame.Grid = GridFactory.Instance.GenerateGameGrid(size, size);
            _playerTurn = Turn.Player1;
            Player1Name.Foreground = _green;
            Player2Name.Foreground = _red;
            _themeName = GridFactory.Instance.LiveTheme.ThemeName;
        }

        /// <summary>
        /// Card Clicker
        /// </summary>
        public void ClickCard(object sender, MouseButtonEventArgs e)
        {
            if (_isBusy) return;

            var img = (Image) sender;
            var card = LiveGame.Grid[img.Name];
            img.Source =
                new BitmapImage(
                    new Uri($"{AppDomain.CurrentDomain.BaseDirectory}/Images/{_themeName}/{card.Name}.png"));
            if (card.IsGone)
                return;

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
                SwitchPlayer(true);

                card.IsClicked = _check.IsClicked = false;
                card.IsGone = true;
                _check.IsGone = true;
            }

            else //deselect incorrect cards so they flip back adn Flip image back to card back
            {
                SwitchPlayer();

                Mouse.OverrideCursor = Cursors.Wait;
                _isBusy = true;
                card.IsClicked = false;
                _check.IsClicked = false;
                _clickedCards[1] = img;

                Task.Delay(TimeSpan.FromSeconds(1)).ContinueWith(t => FlipCard(_clickedCards));
            }

            _check = null;
        }

        private void SwitchPlayer(bool scored = false)
        {
            if (LiveGame.SinglePlayer)
            {
                if (!scored) return;
                LiveGame.ScoreP1++;
                Player1Name.Content = $"{LiveGame.Player1Name}: {LiveGame.ScoreP1}";
                return;
            }

            switch (_playerTurn)
            {
                case Turn.Player1:
                    if (scored)
                        LiveGame.ScoreP1++;
                    else
                    {
                        _playerTurn = Turn.Player2;
                        Player1Name.Foreground = _red;
                        Player2Name.Foreground = _green;
                    }

                    Player1Name.Content = $"{LiveGame.Player1Name}: {LiveGame.ScoreP1}";
                    break;
                case Turn.Player2:
                    if (scored)
                        LiveGame.ScoreP2++;
                    else
                    {
                        _playerTurn = Turn.Player1;
                        Player1Name.Foreground = _green;
                        Player2Name.Foreground = _red;
                    }

                    Player2Name.Content = $"{LiveGame.Player2Name}: {LiveGame.ScoreP2}";
                    break;
            }
        }


        /// <summary>
        /// Flip an array of cards back to the background image
        /// </summary>
        /// <param name="img">array of images to reset</param>
        private void FlipCard(Image[] img)
        {
            _isBusy = false;
            try
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    var bgimg = GridFactory.Instance.LiveTheme.BackImageName;
                    foreach (var i in img)
                    {
                        i.Source = new BitmapImage(
                            new Uri($"{AppDomain.CurrentDomain.BaseDirectory}/Images/{_themeName}/{bgimg}.png"));
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
            Player1Name.Content = $"{p1}: {LiveGame.ScoreP1}";
            LiveGame.Player1Name = p1;

            if (string.IsNullOrEmpty(p2))
            {
                Player2Name.Content = "";
                LiveGame.SinglePlayer = true;
            }
            else
            {
                Player2Name.Content = $"{p2}: {LiveGame.ScoreP2}";
                LiveGame.Player2Name = p2;
            }
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