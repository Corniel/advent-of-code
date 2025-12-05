namespace Advent_of_Code_2015;

/// <summary>
/// There is a boss that has to be fought with.
///
/// Part one: Minium of gold needed to defaut the boss.
/// Part two: Maxium of gold spent and still lose to the boss.
/// </summary>
[Category(Category.Simulation)]
public class Day_21
{
    [Puzzle(answer: 78, O.μs10)]
    public int part_one(Ints numbers)
    {
        var bos = new Fighter(numbers[0], new(0, numbers[1], numbers[2]));
        var you = new Fighter(100, default);
        return Get().OrderBy(i => i.Cost).First(e => you.With(e).Attack(bos)).Cost;
    }

    [Puzzle(answer: 148, O.μs10)]
    public int part_two(Ints numbers)
    {
        var bos = new Fighter(numbers[0], new(0, numbers[1], numbers[2]));
        var you = new Fighter(100, default);
        return Get().OrderBy(i => i.Cost).Last(e => !you.With(e).Attack(bos)).Cost;
    }

    static IEnumerable<Items> Get()
    {
        foreach (var weapon in Weapons)
            foreach (var armor in Armors)
                foreach (var rings in Rings.RoundRobin())
                    yield return weapon.Add(armor).Add(rings.First).Add(rings.Second);
    }

    readonly record struct Fighter(int Hit, Items Items)
    {
        public Fighter With(Items items) => this with { Items = items };
        public bool Attack(Fighter other)
        {
            var self = Hit; var oppo = other.Hit;

            while (true)
            {
                if ((oppo -= Items.Damage - other.Items.Armor) <= 0)
                    return true;
                if ((self -= other.Items.Damage - Items.Armor) <= 0)
                    return false;
            }
        }
    }

    readonly record struct Items(int Cost, int Damage, int Armor)
    {
        public Items Add(Items o) => new(Cost + o.Cost, Damage + o.Damage, Armor + o.Armor);
    }

    static readonly Items[] Weapons =
    [
        new(08, 4, 0),
        new(10, 5, 0),
        new(25, 6, 0),
        new(40, 7, 0),
        new(74, 8, 0),
    ];

    static readonly Items[] Armors =
    [
        default,
        new(013, 0, 1),
        new(031, 0, 2),
        new(053, 0, 3),
        new(075, 0, 4),
        new(102, 0, 5),
    ];

    static readonly Items[] Rings =
    [
        default,
        default,
        new(025, 1, 0),
        new(050, 2, 0),
        new(100, 3, 0),
        new(020, 0, 1),
        new(040, 0, 2),
        new(080, 0, 3),
    ];
}
