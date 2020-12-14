using SmartAss.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Int = System.Numerics.BigInteger;

namespace Advent_of_Code_2019.IntComputing
{
    [DebuggerDisplay("{Answer}, Ouput: {Output.Count}")]
    [DebuggerTypeProxy(typeof(CollectionDebugView))]
    public class Results : IEnumerable<Int>
    {
        public Int Answer { get; internal set; }
        public ICollection<Int> Output { get; } = new List<Int>();

        public IEnumerator<Int> GetEnumerator() => Output.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
