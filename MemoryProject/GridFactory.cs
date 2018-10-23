using System;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using MemoryProject.Data;
using Grid = System.Windows.Controls.Grid;
using System.Collections.Generic;

namespace MemoryProject
{
    public class GridFactory
    {

        static Random rnd = new Random();
        /// <summary>
        /// Placeholder theme only works for 4x4
        /// </summary>
        private static Theme placeholderTheme = new Theme {
            BackImageName = "Cardback",
            cards = new List<Card>{
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
            TmpCardsList.AddRange(placeholderTheme.cards);
            TmpCardsList.AddRange(placeholderTheme.cards);

            var gameGrid = new GameGrid {cards = new Dictionary<string, Card>()};

            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < cols; column++)
                {

                    //var RCard = TmpCardsList[rnd.Next(TmpCardsList.Count)];
                    var RCard = TmpCardsList[0]; // *remove this line after tests
                    TmpCardsList.Remove(RCard);

					var backgroundImage = new Image
                    {
                        Source = new BitmapImage(new Uri($"Images/Placeholders/{placeholderTheme.BackImageName}.png", UriKind.Relative))
                    };
                    backgroundImage.Name = $"I{row}X{column}";
                    backgroundImage.MouseDown += GridManager.Instance.ClickCard;
                    Grid.SetColumn(backgroundImage, column);
                    Grid.SetRow(backgroundImage, row);
                    GridManager.Instance.LiveGameGrid.Children.Add(backgroundImage);

                    RCard.Row = row;
                    RCard.Column = column;
                    gameGrid.cards.Add(backgroundImage.Name, RCard);

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