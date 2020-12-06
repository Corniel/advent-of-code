namespace System
{
    public static class Int32Extensions
    {
        public static bool IsEven(this int n) => (n & 1) == 0;
        public static bool IsEven(this long n) => (n & 1) == 0;
    }
}
