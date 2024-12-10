namespace Advent_of_Code_2016;

[Category(Category.Cryptography)]
public class Day_04
{
    [Example(answer: 123, "aaaaa-bbb-z-y-x-123[abxyz]")]
    [Example(answer: 987, "a-b-c-d-e-f-g-h-987[abcde]")]
    [Example(answer: 404, "not-a-real-room-404[oarel]")]
    [Example(answer: 0, "totally-real-room-200[decoy]")]
    [Puzzle(answer: 158835, O.ms)]
    public int part_one(Lines lines) => lines.As(Secret.Parse).Sum(s => s.Id);

    [Puzzle(answer: 993, O.ms)]
    public int part_two(Lines lines) => lines.As(Secret.Parse)
        .Where(s => s.Id != 0)
        .First(s => s.Decrypted() == "northpole object storage").Id;

    record Secret(string Name, string Checksum, int Id)
    {
        public string Decrypted() => new([..Name.ToCharArray().Select(Decrypt)]);

        char Decrypt(char ch) => ch.InRange('a', 'z')
            ? Characters.a_z[(Characters.a_z.IndexOf(ch) + Id).Mod(26)] : ' ';

        public static Secret Parse(string line)
        {
            var splitted = line.Split('[');
            var checksum = splitted[1][..^1];
            var orderded = new string([..Characters.a_z.OrderByDescending(ch => splitted[0].Count(ch)).Take(checksum.Length)]);
            var last = splitted[0].LastIndexOf('-');
            return new(splitted[0][..last], checksum, checksum == orderded ? -line.Int32() : 0);
        }
    }
}
