// <copyright file = "TileDistances.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using SmartAss.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static System.FormattableString;

namespace SmartAss.Topology
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    [DebuggerTypeProxy(typeof(CollectionDebugView))]
    public class TileDistances : IEnumerable<object>
    {
        private const int Mask = int.MaxValue;
        private const int Unknown = 0;
        private const int Infinite = (int.MaxValue - 1) ^ Mask;

        private readonly int[] distances;

        public TileDistances(int size)
        {
            distances = new int[size];
        }

        public int Known => distances.Count(d => d != Unknown);

        public int Size => distances.Length;

        public int this[int index]
        {
            get => distances[index] ^ Mask;
            set => distances[index] = value ^ Mask;
        }

        public bool IsKnown(int index) => distances[index] != Unknown;

        public bool IsUnknown(int index) => distances[index] == Unknown;

        public void SetInfinite(int index) => distances[index] = Infinite;

        public void Clear() => Array.Clear(distances, 0, distances.Length);

        public IEnumerator<object> GetEnumerator() => distances.Select(d => Debug(d)).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private static object Debug(int n)
        {
            if (n == Unknown) { return "?"; }
            if (n == Infinite) { return "oo"; }
            return n ^ Mask;
        }

        /// <summary>Represents the map as a DEBUG <see cref="string"/>.</summary>
        protected virtual string DebuggerDisplay => Invariant($"Size: {Size:#,##0}, Known: {Known:#,##0}");
    }
}
