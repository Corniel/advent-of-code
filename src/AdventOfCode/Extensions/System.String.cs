namespace System;

internal static class StringExtensions
{
    extension(string str)
    {
        [Pure]
        public int Count(string sub)
        {
            var count = 0;
            var index = str.IndexOf(sub);
            while (index >= 0)
            {
                index = str.IndexOf(sub, index + 1);
                count++;
            }
            return count;
        }
    }
}
