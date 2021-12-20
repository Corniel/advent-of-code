namespace Advent_of_Code_2015;

[Category(Category.Cryptography)]
public class Day_11
{
    [Puzzle(answer: "vzbxxyzz", input: "vzbxkghb")]
    public char[] part_one(string input) => new NextPass(input).First();

    [Puzzle(answer: "vzcaabcc", input: "vzbxkghb")]
    public char[] part_two(string input) => new NextPass(input).Skip(1).First();

    struct NextPass : Iterator<char[]>
    {
        public NextPass(string chars) => Current = chars.Replace('i', 'j').Replace('o', 'p').Replace('l', 'm').ToCharArray();

        public bool MoveNext()
        {
            while(PlusOne())
            {
                var twos = Current[0] == Current[1] || Current[1] == Current[2] ? 1 : 0;
                var three = false;
                for(var i = 2; i < Current.Length; i++)
                {
                    three |= Current[i - 2] + 1 == Current[i - 1] && Current[i - 1] + 1 == Current[i];
                    twos += Current[i - 1] == Current[i] && Current[i - 1] != Current[i - 2] ? 1 : 0;
                }
                if (twos >= 2 && three) return true;
            }
            throw new InfiniteLoop();
        }
        
        private bool PlusOne()
        {
            var pos = Current.Length - 1;
            do Current[pos] = Next[Current[pos]];
            while (Current[pos--] == 'a' && pos >= 0);
            return true;
        }

        public char[] Current { get; private set; }
        public void Dispose() => Do.Nothing();
        public void Reset() => Do.Nothing();
        private static readonly string Next = new string(' ', (int)'a') + "bcdefghjjkmmnppqrstuvwxyza";
    }
}
