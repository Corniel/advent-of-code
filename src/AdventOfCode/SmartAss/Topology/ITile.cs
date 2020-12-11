// <copyright file = "ITile.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using SmartAss.Collections;
using System.Collections.Generic;

namespace SmartAss.Topology
{
    public interface ITile
    {
        /// <summary>The index of the tile.</summary>
        int Index { get; }

        /// <summary>The neighbors of the tile.</summary>
        IEnumerable<ITile> GetNeighbors();
    }

    /// <summary>Represents a tile in a map.</summary>
    /// <typeparam name="T">
    /// The actual implementation of <see cref="ITile"/>.
    /// </typeparam>
    public interface ITile<T> : ITile where T : ITile
    {
        /// <summary>The neighbors of the tile.</summary>
        SimpleList<T> Neighbors { get; }
    }
}
