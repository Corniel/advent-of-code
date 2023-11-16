namespace Advent_of_Code_2015;

[Category(Category.Cryptography)]
public class Day_11
{
    [Puzzle(answer: "vzbxxyzz", "vzbxkghb", O.ms)]
    public char[] part_one(string input) => new NextPass(input).First();

    [Puzzle(answer: "vzcaabcc", "vzbxkghb", O.ms)]
    public char[] part_two(string input) => new NextPass(input).Skip(1).First();

    struct NextPass(string chars) : Iterator<char[]>
    {
        public char[] Current { get; private set; } = chars.Replace('i', 'j').Replace('o', 'p').Replace('l', 'm').ToCharArray();

        public readonly bool MoveNext()
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
        
        readonly bool PlusOne()
        {
            var pos = Current.Length - 1;
            do Current[pos] = Next[Current[pos]];
            while (Current[pos--] == 'a' && pos >= 0);
            return true;
        }

        public readonly void Dispose() => Do.Nothing();
        public readonly void Reset() => Do.Nothing();
        
        static readonly string Next = new string(' ', (int)'a') + "bcdefghjjkmmnppqrstuvwxyza";
    }
}
