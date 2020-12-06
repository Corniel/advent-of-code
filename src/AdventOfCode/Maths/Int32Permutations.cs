using System.Collections.Generic;
using System.Linq;

namespace System
{
    public static class Int32Permutations
    {
        public static IEnumerable<int[]> Permutations(this int[] values)
            => values.Permutation(values.Length, 0);
        
        /// <remarks>Heap's algorithm.</remarks>
        private static IEnumerable<int[]> Permutation(this int[] array, int size, int n)
        {
            if (size == 1)
            {
                yield return array.ToArray();
            }
            for (int i = 0; i < size; i++)
            {
                foreach(var result in  Permutation(array, size - 1, n))
                {
                    yield return result;
                }
                if(size.IsEven())
                {
                    array.Swap(i, size - 1);
                }
                else
                {
                    array.Swap(0, size - 1);
                }
            }
        }

        private static void Swap(this int[] array, int index0, int index1)
        {
            var value = array[index0];
            array[index0] = array[index1];
            array[index1] = value;
        }
    }
}
