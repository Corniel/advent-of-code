// <copyright file = "Raster.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
using static System.FormattableString;

namespace SmartAss.Topology
{
    /// <summary>Represents a 2d raster map.</summary>
    public abstract class Raster<T> : Map<T>
        where T : RasterTile<T>
    {
        /// <summary>Initializes a new instance of the <see cref="Raster{T}"/> class.</summary>
        protected Raster(int cols, int rows)
            : base(rows * cols)
        {
            Cols = cols;
            Rows = rows;

#pragma warning disable S1699
            // Constructors should only call non-overridable methods
            // This is what we want, different initializations for
            // different overrides.
            Initialize(cols, rows);
#pragma warning restore S1699
        }

        /// <summary>Initializes a rows * cols sized raster.</summary>
        protected virtual void Initialize(int cols, int rows)
        {
            var index = 0;

            for (var row = 0; row < rows; row++)
            {
                for (var col = 0; col < cols; col++)
                {
                    var n = 2;
                    n += (col < 1 || col >= cols - 1) ? 0 : 1;
                    n += (row < 1 || row >= rows - 1) ? 0 : 1;
                    Tiles[index] = Create(index, col, row, n);
                    index++;
                }
            }

            foreach (var tile in Tiles)
            {
                var up = tile.Index - cols;
                var dw = tile.Index + cols;
                var le = tile.Index - 1;
                var ri = tile.Index + 1;

                if (up >= 0)
                {
                    tile.Neighbors.Add(Tiles[up]);
                }

                if (tile.Col < cols - 1)
                {
                    tile.Neighbors.Add(Tiles[ri]);
                }

                if (dw < Size)
                {
                    tile.Neighbors.Add(Tiles[dw]);
                }

                if (tile.Col > 0)
                {
                    tile.Neighbors.Add(Tiles[le]);
                }
            }
        }

        /// <summary>Create a single tile for this raster.</summary>
        protected abstract T Create(int index, int col, int row, int neighbors);

        /// <summary>Gets the tile based on its row and column.</summary>
        public T this[int col, int row] => Tiles[col + (row * Cols)];

        /// <summary>The number of rows (height).</summary>
        public int Rows { get; }

        /// <summary>The number of columns (width).</summary>
        public int Cols { get; }

        /// <inheritdoc />
        protected override string DebuggerDisplay
        {
            get => Invariant($"Size: {Rows:#,##0} (height)  x {Cols:#,##0} (width)");
        }
    }
}
