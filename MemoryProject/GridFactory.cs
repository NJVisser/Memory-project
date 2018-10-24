using System;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using MemoryProject.Data;
using Grid = System.Windows.Controls.Grid;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Linq.Expressions;

namespace MemoryProject
{
    public class GridFactory
    {
        static Random rnd = new Random();

        /// <summary>
        /// supports a game upto 20 Pairs a.k.a 40 Cards
        /// </summary>
        public readonly Theme PlaceholderTheme = new Theme
        {
            BackImageName = "Cardback",
            cards = new List<Card>
            {
                new Card
                {
                    Name = "blue"
                },
                new Card
                {
                    Name = "red"
                },
                new Card
                {
                    Name = "brown"
                },
                new Card
                {
                    Name = "green"
                },
                new Card
                {
                    Name = "orange"
                },
                new Card
                {
                    Name = "yellow"
                },
                new Card
                {
                    Name = "purple"
                },
                new Card
                {
                    Name = "pink"
                },
                new Card
                {
                    Name = "blueGrey"
                },
                new Card
                {
                    Name = "darkRed"
                },
                new Card
                {
                    Name = "gold"
                },
                new Card
                {
                    Name = "grey"
                },
                new Card
                {
                    Name = "lavender"
                },
                new Card
                {
                    Name = "lightGreen"
                },
                new Card
                {
                    Name = "lightGrey"
                },
                new Card
                {
                    Name = "lightTurquoise"
                },
                new Card
                {
                    Name = "lightYellow"
                },
                new Card
                {
                    Name = "lime"
                },
                new Card
                {
                    Name = "turquoise"
                },
                new Card
                {
                    Name = "white"
                }
            },
        };


        /// <summary>
        ///     Generating/Summoning the game Grid
        /// </summary>
        /// <param name="cols">Amount of columns</param>
        /// <param name="rows">Amount of rows</param>
        internal GameGrid InitializeGameGrid(int cols, int rows)
        {
            var TmpCardsList = new List<Card>();
            TmpCardsList.AddRange(PlaceholderTheme.cards);
            TmpCardsList.AddRange(PlaceholderTheme.cards);

            var gameGrid = new GameGrid {cards = new Dictionary<string, Card>()};

            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < cols; column++)
                {
                    var RCard = TmpCardsList[rnd.Next(TmpCardsList.Count)];
                    TmpCardsList.Remove(RCard);

                    var name = TmpCardsList.Exists(item => item.Name == RCard.Name)
                        ? RCard.Name
                        : $"{RCard.Name}Pair";

                    var backgroundImage = new Image
                    {
                        Source = new BitmapImage(new Uri($"Images/Placeholders/{PlaceholderTheme.BackImageName}.png",
                            UriKind.Relative)),
                        Cursor = Cursors.Hand,
                        Name = name
                    };
                    backgroundImage.MouseDown += GridManager.Instance.ClickCard;
                    Grid.SetColumn(backgroundImage, column);
                    Grid.SetRow(backgroundImage, row);
                    GridManager.Instance.LiveGameGrid.Children.Add(backgroundImage);

                    RCard.Row = row;
                    RCard.Column = column;
                    gameGrid.cards.Add(name, RCard);
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