namespace System
{
    public static class NumberExtensions
    {
        public static int Mod(this int n, int mod)
        {
            var m = n % mod;
            return m < 0 ? m + mod : m;
        }
    }
}
