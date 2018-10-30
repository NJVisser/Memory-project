using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using AGC.Tools;
using MemoryProject.Data;
using Newtonsoft.Json;

namespace MemoryProject
{
    public class GridFactory
    {
        static Random rnd = new Random();

        /// <summary>
        /// supports a game upto 20 Pairs a.k.a 40 Cards
        /// </summary>
        public Theme LiveTheme =
            JsonConvert.DeserializeObject<Theme>(
                File.ReadAllText($@"{AppDomain.CurrentDomain.BaseDirectory}/Themes/Placeholder.json"));


        /// <summary>
        ///     Generating/Summoning the game Grid
        /// </summary>
        /// <param name="cols">Amount of columns</param>
        /// <param name="rows">Amount of rows</param>
        internal Dictionary<string, Card> GenerateGameGrid(int cols, int rows)
        {
            var gameGrid = new Dictionary<string, Card>();
            var cardsNeeded = cols * rows;
            var tmpCardsList = new List<Card>();
            var cardsUsedList = LiveTheme.Cards.Take(cardsNeeded / 2).ToList();
            foreach (var c in cardsUsedList)
            {
                tmpCardsList.Add(new Card {Name = c.Name, ID = c.Name});
                tmpCardsList.Add(new Card {Name = c.Name, ID = $"{c.Name}Pair"});
            }

            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < cols; column++)
                {
                    var RCard = tmpCardsList[rnd.Next(tmpCardsList.Count)];
                    tmpCardsList.Remove(RCard);

                    var backgroundImage = new Image
                    {
                        Source = new BitmapImage(new Uri(
                            $"{AppDomain.CurrentDomain.BaseDirectory}/Images/{LiveTheme.ThemeName}/{LiveTheme.BackImageName}.png")),
                        Cursor = Cursors.Hand,
                        Name = RCard.ID
                    };
                    backgroundImage.MouseDown += GridManager.Instance.ClickCard;
                    Grid.SetColumn(backgroundImage, column);
                    Grid.SetRow(backgroundImage, row);
                    GridManager.Instance.LiveGameGrid.Children.Add(backgroundImage);

                    RCard.Row = row;
                    RCard.Column = column;
                    gameGrid.Add(RCard.ID, RCard);
                }
            }

            return gameGrid;
        }


        internal void SetTheme(string Name)
        {
            LiveTheme =
                JsonConvert.DeserializeObject<Theme>(
                    File.ReadAllText($@"{AppDomain.CurrentDomain.BaseDirectory}/Themes/{Name}.json"));
        }

        internal bool RestoreGameGrid(Dictionary<string, Card> savedGrid)
        {
            try
            {
                foreach (var card in savedGrid)
                {
                    var backgroundImage = new Image
                    {
                        Source = card.Value.IsGone
                            ? new BitmapImage(new Uri(
                                $"{AppDomain.CurrentDomain.BaseDirectory}/Images/{LiveTheme.ThemeName}/{card.Value.Name}.png"))
                            : new BitmapImage(new Uri(
                                $"{AppDomain.CurrentDomain.BaseDirectory}/Images/{LiveTheme.ThemeName}/{LiveTheme.BackImageName}.png")),
                        Cursor = Cursors.Hand,
                        Name = card.Key
                    };
                    if (!card.Value.IsGone)
                        backgroundImage.MouseDown += GridManager.Instance.ClickCard;

                    Grid.SetColumn(backgroundImage, card.Value.Column);
                    Grid.SetRow(backgroundImage, card.Value.Column);
                    GridManager.Instance.LiveGameGrid.Children.Add(backgroundImage);
                }


                return true;
            }
            catch (Exception e)
            {
                AGCTools.LogException(e);
                return false;
            }
        }

        #region Singleton

        private static readonly Lazy<GridFactory> LazyGridFactory =
            new Lazy<GridFactory>(() => new GridFactory());

        public static GridFactory Instance => LazyGridFactory.Value;

        private GridFactory()
        {
        }

        #endregion
    }
}