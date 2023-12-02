namespace Advent_of_Code_2021;

[Category(Category.ExpressionParsing)]
public class Day_16
{
    [Example(answer: 31, "A0016C880162017C3686B18A3D4780")]
    [Puzzle(answer: 996, O.μs10)]
    public int part_one(string str) => new Parser(str).Read().Versions;

    [Example(answer: 3, "C200B40A82")]
    [Example(answer: 54, "04005AC33890")]
    [Example(answer: 7, "880086C3E88112")]
    [Example(answer: 9, "CE00C43D881120")]
    [Example(answer: 1, "D8005AC2A8F0")]
    [Example(answer: 0, "F600BC2D8F")]
    [Example(answer: 0, "9C005AC2F8F0")]
    [Example(answer: 1, "9C0141080250320F1802104A08")]
    [Puzzle(answer: 96257984154, O.μs10)]
    public long part_two(string str) => new Parser(str).Read().Value;

    enum TypeId { Sum = 0, Product = 1, Min = 2, Max = 3, Literal = 4, GT = 5, LT = 6, Eq = 7 }
    
    class Packet(int version, TypeId type) : SyntaxNode
    {
        public int Version { get; } = version;
        public TypeId Type { get; } = type;
        public int Versions => Version + Children<Packet>().Sum(ch => ch.Versions);
        public virtual long Value => Type switch
        {
            TypeId.Product => Children<Packet>().Select(ch => ch.Value).Product(),
            TypeId.Min => Children<Packet>().Min(ch => ch.Value),
            TypeId.Max => Children<Packet>().Max(ch => ch.Value),
            TypeId.GT => Children<Packet>()[0].Value >  Children<Packet>()[1].Value ? 1 : 0,
            TypeId.LT => Children<Packet>()[0].Value <  Children<Packet>()[1].Value ? 1 : 0,
            TypeId.Eq => Children<Packet>()[0].Value == Children<Packet>()[1].Value ? 1 : 0,
            TypeId.Sum or _ => Children<Packet>().Sum(ch => ch.Value),
        };
    }
    class Literal(int version, long val) : Packet(version, TypeId.Literal)
    {
        public override long Value { get; } = val;
    }
    class Parser : SyntaxParser
    {
        public Parser(string str) : base(string.Concat(str.Select(ch => bits[1 + "0123456789ABCDEF".IndexOf(ch)]))) => Do.Nothing();
        public Packet Read()
        {
            var version = (int)ReadBinary(3);
            var type = (TypeId)ReadBinary(3);
            if (type != TypeId.Literal)
            {
                var packet = new Packet(version, type);
                if (ReadBinary(1) == 0)
                {
                    var end = (int)ReadBinary(15) + Position;
                    while (Position < end) { packet.AddChild(Read()); }
                }
                else
                {
                    var count = (int)ReadBinary(11);
                    while (packet.Children().Count < count) { packet.AddChild(Read()); }
                }
                return packet;
            }
            else return new Literal(version, ReadLiteral());
        }
        public long ReadLiteral()
        {
            ulong literal = 0;
            ulong block;
            do
            {
                block = ReadBinary(5);
                literal = (literal << 4) | block & 0b1111;
            }
            while ((block & 0b10000) != 0);
            return (long)literal;
        }
        static readonly string[] bits = ["", "0000", "0001", "0010", "0011", "0100", "0101", "0110", "0111", "1000", "1001", "1010", "1011", "1100", "1101", "1110", "1111"];
    }
}
