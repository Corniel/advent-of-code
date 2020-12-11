// <copyright file = "SphereRaster.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SmartAss.Topology
{
    /// <summary>Represents a 2d raster map where both top and bottom and left
    /// and right are connected.
    /// </summary>
    public abstract class SphereRaster<T> : Raster<T>
        where T : RasterTile<T>
    {
        /// <summary>Initializes a new instance of the <see cref="SphereRaster{T}"/> class.</summary>
        protected SphereRaster(int cols, int rows)
            : base(cols, rows) => Do.Nothing();

        /// <summary>Initializes a rows * cols sized sphere raster.</summary>
        protected override void Initialize(int cols, int rows)
        {
            var index = 0;

            for (var row = 0; row < rows; row++)
            {
                for (var col = 0; col < cols; col++)
                {
                    Tiles[index] = Create(index, col, row, 4);
                    index++;
                }
            }

            foreach (var tile in Tiles)
            {
                var row = tile.Row;
                var col = tile.Col;

                var n = this[(col + 0) /*         */, (row - 1 + rows) % rows];
                var e = this[(col + 1) % cols /*  */, (row + 0)];
                var s = this[(col + 0) /*         */, (row + 1) % rows];
                var w = this[(col - 1 + cols) % cols, (row + 0)];

                tile.Neighbors.Add(n);
                tile.Neighbors.Add(w);
                tile.Neighbors.Add(e);
                tile.Neighbors.Add(s);
            }
        }
    }
}
