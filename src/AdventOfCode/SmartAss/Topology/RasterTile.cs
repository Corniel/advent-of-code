// <copyright file = "RasterTile.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using SmartAss.Collections;
using static System.FormattableString;

namespace SmartAss.Topology
{
    public class RasterTile<T> : ITile<T>
        where T : ITile<T>
    {
        public RasterTile(int index, int col, int row, int neighbors)
        {
            Index = index;
            Col = col;
            Row = row;
            Neighbors = new SimpleList<T>(neighbors);
        }

        /// <inheritdoc />
        public int Index { get; }

        /// <summary>The x coordinate (column) of the tile.</summary>
        public int Col { get; }

        /// <summary>The y coordinate (row) of the tile.</summary>
        public int Row { get; }

        /// <inheritdoc />
        public SimpleList<T> Neighbors { get; }

        /// <inheritdoc />
        public IEnumerable<ITile> GetNeighbors() => Neighbors.Cast<ITile>();

        /// <inheritdoc />
        public override string ToString()
        {
            return Invariant($"[{Index}] (x: {Col}, y: {Row}), Neighbors: {Neighbors.Count}");
        }
    }
}
