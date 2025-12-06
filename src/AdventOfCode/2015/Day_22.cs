namespace Advent_of_Code_2015;

/// <summary>
/// There is a boss that has to be fought with.
///
/// Part one: Minium of mana needed to defaut the boss.
/// Part two: Maxium of mana spent and still lose to the boss.
/// </summary>
[Category(Category.Simulation)]
public class Day_22
{
    [Puzzle(answer: 900, O.ms)]
    public int part_one(Ints numbers) => Run(numbers, 0);

    [Puzzle(answer: 1216, O.ms10)]
    public int part_two(Ints numbers) => Run(numbers, 1);

    private static int Run(Ints numbers, int pen)
    {
        var (bos, armor) = (numbers[0], numbers[1]);
        var q = new PriorityQueue<State, int>([(new State(0, 500, 50, bos, default), 0)]);

        while (q.TryDequeue(out var state, out _))
        {
            foreach (var spell in Spells.Where(state.Applicable))
            {
                if (state.Your(spell, pen)?.Boss(armor) is { } next)
                {
                    if (next.Bos <= 0) return next.Cost;
                    else q.Enqueue(next, next.Cost);
                }
            }
        }
        throw new NoAnswer();
    }

    readonly record struct State(int Cost, int Mana, int You, int Bos, Durs Durs)
    {
        public bool Applicable(Spell s) => Mana >= s.Cost && Durs[s.Pos] <= 1;

        public State? Boss(int damg)
        {
            var st = Spell();

            if (st.Bos <= 0) return st;

            var you = st.You - Math.Max(1, damg - (Durs[Shield.Pos] is 0 ? 0 : Shield.Armor));

            return you <= 0 ? null : st with { You = you };
        }

        public State? Your(Spell spell, int pen)
        {
            var st = pen is 0 ? this : this with { You = You - pen };

            // Killed due to the penalty.s
            if (st.You <= 0) return null;

            st = st.Spell();

            return st with
            {
                Cost = st.Cost + spell.Cost,
                Mana = st.Mana - spell.Cost,
                Durs = st.Durs.Set(spell.Pos, spell.Dur),
            };
        }

        private State Spell()
        {
            if (Durs.Value is 0) return this;

            var damg = 0; var heal = 0; var mana = 0; var durs = Durs;

            foreach (var s in Spells)
            {
                var dur = durs[s.Pos];
                if (dur == 0) continue;

                damg += s.Damage;
                heal += s.Heal;
                mana += s.Mana;
                durs = durs.Set(s.Pos, dur - 1);
            }
            return this with
            {
                Mana = Mana + mana,
                You = You + heal,
                Bos = Bos - damg,
                Durs = durs,
            };
        }
    }

    [DebuggerDisplay("{this[0]}, {this[1]}, {this[2]}, {this[3]}, {this[4]}")]
    readonly record struct Durs(int Value)
    {
        public int this[int pos] => (Value >> (pos << 2)) & 7;
        public Durs Set(byte pos, int val) => new((Value & Mask[pos]) | (val << (pos << 2)));
        static readonly int[] Mask = [0xFFFF0, 0xFFF0F, 0xFF0FF, 0xF0FFF, 0x0FFFF];
    }

    record Spell(byte Pos, int Cost, int Damage = 0, int Heal = 0, int Armor = 0, int Mana = 0, int Dur = 0);

    static readonly Spell Missile = new(0, Cost: 53, Damage: 4, Dur: 1);
    static readonly Spell Drain = new(1, Cost: 73, Damage: 2, Heal: 2, Dur: 1);
    static readonly Spell Shield = new(2, Cost: 113, Armor: 7, Dur: 6);
    static readonly Spell Poison = new(3, Cost: 173, Damage: 3, Dur: 6);
    static readonly Spell Recharge = new(4, Cost: 229, Mana: 101, Dur: 5);
    static readonly Spell[] Spells = [Missile, Drain, Shield, Poison, Recharge];
}
