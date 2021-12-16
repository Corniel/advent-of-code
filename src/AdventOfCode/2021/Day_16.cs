namespace Advent_of_Code_2021;

public class Day_16
{
    [Example(answer: 31, "A0016C880162017C3686B18A3D4780")]
    [Puzzle(answer: 996, year: 2021, day: 16)]
    public int part_one(string input) => new Reader(input).Read().Versions;

    [Example(answer: 3, "C200B40A82")]
    [Example(answer: 54, "04005AC33890")]
    [Example(answer: 7, "880086C3E88112")]
    [Example(answer: 9, "CE00C43D881120")]
    [Example(answer: 1, "D8005AC2A8F0")]
    [Example(answer: 0, "F600BC2D8F")]
    [Example(answer: 0, "9C005AC2F8F0")]
    [Example(answer: 1, "9C0141080250320F1802104A08")]
    [Puzzle(answer: 96257984154, year: 2021, day: 16)]
    public long part_two(string input) => new Reader(input).Read().Value;

    enum TypeId { Sum = 0, Product = 1, Min = 2, Max = 3, Literal = 4, GT = 5, LT = 6, Eq = 7 }
    record Packet(int Version, TypeId Type)
    {
        public int Versions => Version + Children.Sum(ch => ch.Versions);
        public virtual long Value => Type switch
        {
            TypeId.Product => Children.Select(ch => ch.Value).Product(),
            TypeId.Min => Children.Min(ch => ch.Value),
            TypeId.Max => Children.Max(ch => ch.Value),
            TypeId.GT => Children[0].Value > Children[1].Value ? 1 : 0,
            TypeId.LT => Children[0].Value < Children[1].Value ? 1 : 0,
            TypeId.Eq => Children[0].Value == Children[1].Value ? 1 : 0,
            TypeId.Sum or _ => Children.Sum(ch => ch.Value),
        };
        public List<Packet> Children { get; } = new();
    }
    record Literal(int Version, long Val) : Packet(Version, TypeId.Literal)
    {
        public override long Value => Val;
    }
    class Reader
    {
        public Reader(string input) => Bin = string.Concat(input.Select(ch => bits[1 + "0123456789ABCDEF".IndexOf(ch)]));
        public int Length => Bin.Length - Offset;
        private readonly string Bin;
        private int Offset;
        public Packet Read()
        {
            var version = (int)Read(3);
            var type = (TypeId)Read(3);
            if (type != TypeId.Literal)
            {
                var packet = new Packet(version, type);
                if (Read(1) == 0)
                {
                    var length = Length - Read(15) - 15;
                    while (Length > length)
                    {
                        packet.Children.Add(Read());
                    }
                }
                else
                {
                    var count = Read(11);
                    while (packet.Children.Count < count)
                    {
                        packet.Children.Add(Read());
                    }
                }
                return packet; 
            }
            else return new Literal(version, Literal());
        }
        public long Read(int size)
        {
            var read = Bits.UInt64.Parse(Bin.Substring(Offset, size));
            Offset += size;
            return (int)read;
        }
        public long Literal()
        {
            long literal = 0;
            long block;
            do
            {
                block = Read(5);
                literal = (literal << 4) | block & 0b1111;
            }
            while ((block & 0b10000) != 0);
            return literal;
        }
        static readonly string[] bits = new[] { "", "0000", "0001", "0010", "0011", "0100", "0101", "0110", "0111", "1000", "1001", "1010", "1011", "1100", "1101", "1110", "1111" };
    }
}
