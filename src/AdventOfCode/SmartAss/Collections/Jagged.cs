namespace SmartAss.Collections
{
    /// <summary>Helper for creating jagged arrays.</summary>
    public static class Jagged
    {
        /// <summary>Creates a jagged array of m x n.</summary>
        /// <typeparam name="T">
        /// Type of the jagged array.
        /// </typeparam>
        public static T[][] Array<T>(int m, int n)
        {
            var array = new T[m][];
            for (var i = 0; i < m; i++)
            {
                array[i] = new T[n];
            }

            return array;
        }

        /// <summary>Creates a jagged array of m x n x o.</summary>
        /// <typeparam name="T">
        /// Type of the jagged array.
        /// </typeparam>
        public static T[][][] Array<T>(int m, int n, int o)
        {
            var array = new T[m][][];
            for (var i = 0; i < m; i++)
            {
                array[i] = Array<T>(n, o);
            }

            return array;
        }

        /// <summary>Creates a jagged array of m x n x o x p.</summary>
        /// <typeparam name="T">
        /// Type of the jagged array.
        /// </typeparam>
        public static T[][][][] Array<T>(int m, int n, int o, int p)
        {
            var array = new T[m][][][];
            for (var i = 0; i < m; i++)
            {
                array[i] = Array<T>(n, o, p);
            }

            return array;
        }
    }
}
