// <copyright file = "Map.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using SmartAss.Collections;
using SmartAss.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static System.FormattableString;

namespace SmartAss.Topology
{
    /// <summary>Represents a map.</summary>
    [DebuggerDisplay("{DebuggerDisplay}")]
    [DebuggerTypeProxy(typeof(CollectionDebugView))]
    public abstract class Map<T> : IEnumerable<T>
        where T : ITile<T>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected T[] Tiles { get; }

        /// <summary>Initializes a new instance of the <see cref="Map{T}"/> class.</summary>
        protected Map(int size) => Tiles = new T[size];

        /// <summary>Gets the number of tiles on the map.</summary>
        public int Size => Tiles.Length;

        /// <summary>Gets a tile with the specified index.</summary>
        public T this[int index] => Tiles[index];

        public void Remove(T tile)
        {
            foreach(var neigbor in tile.Neighbors)
            {
                neigbor.Neighbors.Remove(tile);
            }
            Tiles[tile.Index] = default;
        }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator() => Tiles.Where(tile => !Equals(tile, null)).GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>Represents the map as a DEBUG <see cref="string"/>.</summary>
        protected virtual string DebuggerDisplay => Invariant($"Size: {Size:#,##0}");
    }
}
