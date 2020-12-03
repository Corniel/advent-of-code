using SmartAss;
using SmartAss.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode._2020
{
    [DebuggerTypeProxy(typeof(CollectionDebugView))]
    [DebuggerDisplay("Count: {Count}, Min: {Minimum}, Max: {Maximum}")]
    public class UniqueNumbers : IEnumerable<int>
    {
        private readonly byte[] contains = new byte[short.MaxValue];
        private const byte True = 255;

        public UniqueNumbers(IEnumerable<int> numbers)
        {
            var minimum = int.MaxValue;
            var maximum = int.MinValue;
            var count = 0;

            foreach(var item in numbers)
            {
                contains[item] = True;
                count++;
                if(item > maximum)
                {
                    maximum = item;
                }
                else if(item < minimum)
                {
                    minimum = item;
                }
            }
            Minimum = minimum;
            Maximum = maximum;
            Count = count;
        }

        public int Minimum { get; }
        public int Maximum { get; }
        public int Count { get; }

        public bool Contains(int number) => contains[number] == True;

        public IEnumerable<int> TakeToMax(int max) => new Enumerator(contains, Minimum, max);

        public IEnumerable<int> Enumerate(int min, int max) => new Enumerator(contains, min, max);

        public IEnumerator<int> GetEnumerator() => new Enumerator(contains, Minimum, Maximum);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public static UniqueNumbers Parse(string str) => new UniqueNumbers(Parser.Ints(str));

        private class Enumerator : IEnumerator<int>, IEnumerable<int>
        {
            private readonly byte[] contains;
            private readonly int max;
            private int index = -1;

            public Enumerator(byte[] contains, int min, int max)
            {
                this.contains = contains;
                this.index = min - 1;
                this.max = max;
            }

            public int Current => index;

            object IEnumerator.Current => Current;

            public void Dispose() => Do.Nothing();

            public bool MoveNext()
            {
                do { index++; }
                while (contains[index] == 0 && index <= max);
                return index < max;
            }

            public void Reset() => throw new NotSupportedException();

            public IEnumerator<int> GetEnumerator() => this;

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
