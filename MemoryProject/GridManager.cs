using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Automation;

namespace MemoryProject
{
    public class GridManager
    {
        private readonly UniformGrid _grid;

        public GridManager(UniformGrid grid)
        {
            _grid = grid;
        }

        /// <summary>
        ///     Generating/Summoning the game Grid
        /// </summary>
        /// <param name="cols">Amount of columns</param>
        /// <param name="rows">Amount of rows</param>
        internal void InitializeGameGrid(int cols, int rows)
        {
            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < cols; column++)
                {
                    var backgroundImage = new Image
                    {
                        Source = new BitmapImage(new Uri("Images/Placeholders/100x100.png", UriKind.Relative))
                    };

                    backgroundImage.Name = $"I{row}X{column}";
                    backgroundImage.MouseDown += ClickCard;
                    Grid.SetColumn(backgroundImage, column);
                    Grid.SetRow(backgroundImage, row);
                    _grid.Children.Add(backgroundImage);
                }
            }
        }

        private void ClickCard(object sender, MouseButtonEventArgs e)
        {
            ((Image) sender).Source = new BitmapImage(new Uri("Images/Placeholders/CSharp.jpg", UriKind.Relative));
        }


        /// <summary>
        ///     Clears the game gird 
        /// </summary>
        internal void Clear()
        {
            _grid.Children.Clear();
        }
    }
}