// <copyright file = "CollectionDebugView.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

#pragma warning disable S2365
// Properties should not make collection or array copies
// Required for debugging purposes.

using System.Collections;
using System.Diagnostics;
using System.Linq;

namespace SmartAss.Diagnostics
{
    /// <summary>Allows the debugger to display collections.</summary>
    public class CollectionDebugView
    {
        /// <summary>A reference to the enumeration to display.</summary>
        private readonly IEnumerable enumeration;

        /// <summary>Initializes a new instance of the <see cref="CollectionDebugView"/> class..</summary>
        public CollectionDebugView(IEnumerable enumeration) => this.enumeration = enumeration;

        /// <summary>The array that is shown by the debugger.</summary>
        /// <remarks>
        /// Every time the enumeration is shown in the debugger, a new array is created.
        /// By doing this, it is always in sync with the current state of the enumeration.
        /// </remarks>
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public object[] Items => enumeration.Cast<object>().ToArray();
    }
}
