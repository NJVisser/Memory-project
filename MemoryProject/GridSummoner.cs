using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MemoryProject
{
    public class GridSummoner
    {
        private readonly int _cols;
        private readonly Grid _grid;
        private readonly int _rows;

        public GridSummoner(Grid grid, int cols, int rows)
        {
            _grid = grid;
            _cols = cols;
            _rows = rows;
            InitializeGameGrid(cols, rows);
            AddImages();
        }

        /// <summary>
        ///     Generating/Summoning a Grid
        /// </summary>
        /// <param name="cols"></param>
        /// <param name="rows"></param>
        private void InitializeGameGrid(int cols, int rows)
        {
            for (var i = 0; i < rows; i++) _grid.RowDefinitions.Add(new RowDefinition());

            for (var i = 0; i < cols; i++) _grid.ColumnDefinitions.Add(new ColumnDefinition());
        }

        private void AddImages()
        {
            for (var row = 0; row < _rows; row++)
            {
                for (var column = 0; column < _cols; column++)
                {
                    var backgroundImage = new Image
                    {
                        Source = new BitmapImage(new Uri("Images/Placeholders/100x100.png", UriKind.Relative))
                    };
                    Grid.SetColumn(backgroundImage, column);
                    Grid.SetRow(backgroundImage, row);
                    _grid.Children.Add(backgroundImage);
                }
            }
        }
    }
}