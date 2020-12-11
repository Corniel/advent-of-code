// <copyright file = "ArrayEnumerator.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

#pragma warning disable S3898 // Value types should implement "IEquatable<T>"
// No value for an IEnumerator

using System.Collections;
using System.Collections.Generic;

namespace SmartAss.Collections
{
    /// <summary>Enumerates through a (subset of) an array.</summary>
    /// <typeparam name="T">
    /// The type of the array to enumerate through.
    /// </typeparam>
    public struct ArrayEnumerator<T> : IEnumerator<T>, IEnumerable<T>
    {
        private readonly T[] array;
        private readonly int end;
        private int index;

        /// <summary>Initializes a new instance of the <see cref="ArrayEnumerator{T}"/> struct.</summary>
        public ArrayEnumerator(T[] array, int count)
            : this(array, 0, count) => Do.Nothing();

        /// <summary>Initializes a new instance of the <see cref="ArrayEnumerator{T}"/> struct.</summary>
        public ArrayEnumerator(T[] array, int startIndex, int count)
        {
            this.array = array;
            end = startIndex + count;
            index = startIndex - 1;
        }

        /// <inheritdoc />
        public T Current => array[index];

        /// <inheritdoc />
        object IEnumerator.Current => Current;

        /// <inheritdoc />
        public bool MoveNext() => ++index < end;

        /// <inheritdoc />
        public void Reset() => index = -1;

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator() => this;

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc />
        public void Dispose() => Do.Nothing();
    }
}
