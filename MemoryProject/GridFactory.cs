using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
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
        public readonly Theme PlaceholderTheme =
            JsonConvert.DeserializeObject<Theme>(
                File.ReadAllText($@"{AppDomain.CurrentDomain.BaseDirectory}\Themes\PlaceholderTheme.json"));


        /// <summary>
        ///     Generating/Summoning the game Grid
        /// </summary>
        /// <param name="cols">Amount of columns</param>
        /// <param name="rows">Amount of rows</param>
        internal GameGrid InitializeGameGrid(int cols, int rows)
        {
            var cardsNeeded = cols * rows;
            var tmpCardsList = new List<Card>();
            var cardsUsedList = PlaceholderTheme.cards.Take(cardsNeeded / 2).ToList();
            cardsUsedList.ForEach(c =>
            {
                c.ID = c.Name;
                tmpCardsList.Add(c);
            });
            cardsUsedList.ForEach(c =>
            {
                c.ID = $"{c.Name}Pair";
                tmpCardsList.Add(c);
            });

            var gameGrid = new GameGrid {cards = new Dictionary<string, Card>()};

            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < cols; column++)
                {
                    var RCard = tmpCardsList[rnd.Next(tmpCardsList.Count)];
                    tmpCardsList.Remove(RCard);

                    var backgroundImage = new Image
                    {
                        Source = new BitmapImage(new Uri($"Images/Placeholders/{PlaceholderTheme.BackImageName}.png",
                            UriKind.Relative)),
                        Cursor = Cursors.Hand,
                        Name = RCard.ID
                    };
                    backgroundImage.MouseDown += GridManager.Instance.ClickCard;
                    Grid.SetColumn(backgroundImage, column);
                    Grid.SetRow(backgroundImage, row);
                    GridManager.Instance.LiveGameGrid.Children.Add(backgroundImage);

                    RCard.Row = row;
                    RCard.Column = column;
                    gameGrid.cards.Add(RCard.ID, RCard);
                }
            }

            return gameGrid;
        }

        private static readonly Lazy<GridFactory> LazyGridFactory =
            new Lazy<GridFactory>(() => new GridFactory());

        public static GridFactory Instance => LazyGridFactory.Value;

        private GridFactory()
        {
        }
    }
}