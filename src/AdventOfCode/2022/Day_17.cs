namespace Advent_of_Code_2022;

[Category(Category.BitManupilation)]
public class Day_17
{
    [Example(answer: 3068, ">>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>")]
    [Puzzle(answer: 3168L, O.μs100)]
    public long part_one(string str) => Play(str, 2022);

    [Example(answer: 1514285714288, ">>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>")]
    [Puzzle(answer: 1554117647070, O.μs100)]
    public long part_two(string str) => Play(str, 1_000_000_000_000);

    static long Play(string shifts, long turns)
    {
        long extra = -1; long shift = 0;
        var hashes = new Dictionary<Last14, Snapshot>();
        List<Row> wall = [new(255)];

        for (long turn = 0; turn < turns; turn++)
        {
            var block = Block.New(turn, shifts, ref shift);
            var offset = wall.Count;

            while (!block.Overlaps(wall, offset - 1))
            {
                offset--;
                var shifted = block.Shift(shifts, ref shift);
                block = shifted.Overlaps(wall, offset) ? block : shifted;
            }
            foreach (var row in block.Rows)
            {
                if (offset < wall.Count) wall[offset] = wall[offset].Merge(row);
                else wall.Add(row);
                offset++;
            }
            if (turn.Mod(5) == 0)
            {
                var hash = Last14.New(wall);
                
                if (!hashes.TryAdd(hash, new(turn, wall.Count)))
                {
                    var first = hashes[hash];
                    var period = turn - first.Turn;
                    var periods = (turns - first.Turn) / period;
                    var remainder = turns - periods * period - first.Turn;
                    turns = turn + remainder;
                    extra += (wall.Count - first.Count) * (periods - 1);
                }
            }
        }
        return wall.Count + extra;
    }

    record struct Snapshot(long Turn, int Count);

    record struct Last14(long Lo, long Hi)
    {
        public static Last14 New(List<Row> rows)
        {
            long lo = 0; long hi = 0;
            foreach (var row in rows.Skip(rows.Count - 7)) lo = lo << 7 | row.Index;
            foreach (var row in rows.Skip(rows.Count - 14).Take(7)) hi = hi << 7 | row.Index;
            return new(lo, hi);
        }
    }
    
    record struct Block(params Row[] Rows)
    {
        public static Block New(long turn, string shifts, ref long shift)
        {
            var block = Init[turn % 5];
            for (var i = 0; i < 4; i++) block = block.Shift(shifts, ref shift);
            return block;
        }

        public readonly bool Overlaps(IReadOnlyList<Row> other, int offset)
        {
            for (var i = 0; i < Rows.Length; i++)
            {
                var o = i + offset;
                if (o >= other.Count) return false;
                if (Rows[i].Overlaps(other[o])) return true;
            }
            return false;
        }

        public readonly Block Shift(string shifts, ref long shift) => shifts[(int)(shift++ % shifts.Length)] == '<' ? Left() : Right();
        readonly Block Left() => Rows.Select(r => r.Left()).ToArray() is { } rows && rows.TrueForAll(r => r != default) ? new(rows) : this;
        readonly Block Right() => Rows.Select(r => r.Right()).ToArray() is { } rows && rows.TrueForAll(r => r != default) ? new(rows) : this;

        readonly static Block[] Init =
        [
            /* - */ new(new Row(0b_00_111_10)),
            /* + */ new(new Row(0b_00_010_00), new Row(0b_00_111_00), new Row(0b_00_010_00)),
            /* J */ new(new Row(0b_00_111_00), new Row(0b_00_001_00), new Row(0b_00_001_00)),
            /* | */ new(new Row(0b_00_100_00), new Row(0b_00_100_00), new Row(0b_00_100_00), new Row(0b_00_100_00)),
            /* O */ new(new Row(0b_00_110_00), new Row(0b_00_110_00)),
        ];
    }

    record struct Row(byte cells)
    {
        public readonly byte Cells = cells;
        public readonly long Index => Cells;
        public readonly bool Overlaps(Row other) => (Cells & other.Cells) != 0;
        public readonly Row Merge(Row other) => new((byte)(Cells | other.Cells));
        public readonly Row Left() => (Cells << 1) is var r && r < 128 ? new((byte)r) : default;
        public readonly Row Right() => (Cells >> 1) is var l && (l << 1) == Cells ? new((byte)l) : default;
    }
}
