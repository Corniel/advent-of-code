namespace Advent_of_Code_2019;

[Category(Category.Simulation)]
public class Day_06
{
    [Example(answer: 42, @"COM)B; B)C; C)D; D)E;E)F; B)G; G)H; D)I; E)J; J)K; K)L")]
    [Puzzle(answer: 333679, O.ms)]
    public int part_one(string input)
        => Space.Parse(input).Connections;

    [Example(answer: 4, @"COM)B; B)C; C)D; D)E; E)F; B)G; G)H; D)I; E)J; J)K; K)L; K)YOU; I)SAN")]
    [Puzzle(answer: 370, O.ms10)]
    public int part_two(string input)
    {
        var space = Space.Parse(input);

        var you = space.Get("YOU");
        var san = space.Get("SAN");

        var minimum = space.Values.Min(obj =>
        {
            var y = obj.Distance(you);
            var s = obj.Distance(san);

            return y.HasValue && s.HasValue
                ? y.Value + s.Value - 2
                : int.MaxValue;
        });
        return minimum;
    }

    class Space : Dictionary<string, SpaceObject>
    {
        public int Connections => Values.Sum(obj => obj.Connections);

        public SpaceObject Get(string name)
        {
            if (!TryGetValue(name, out var obj))
            {
                obj = new SpaceObject(name);
                Add(name, obj);
            }
            return obj;
        }

        public static Space Parse(string str)
        {
            var space = new Space();

            foreach (var line in str.Lines())
            {
                var split = line.Split(')');
                var parent = space.Get(split[0]);
                var child = space.Get(split[1]);
                parent.Children.Add(child);
            }
            return space;
        }
    }

    class SpaceObject(string name) : IEquatable<SpaceObject>
    {
        public string Name { get; } = name;

        public ICollection<SpaceObject> Children { get; } = [];

        public int Connections => Children.Count + Children.Sum(ch => ch.Connections);

        public int? Distance(SpaceObject child)
        {
            if (Equals(child))
            {
                return default;
            }
            else if (Children.Any(ch => ch.Equals(child)))
            {
                return 1;
            }
            else
            {
                int? distance = int.MaxValue;
                foreach (var ch in Children)
                {
                    var test = ch.Distance(child);
                    if (test.HasValue && test < distance)
                    {
                        distance = test;
                    }
                }
                return distance == int.MaxValue ? default : 1 + distance;
            }
        }

        public override bool Equals(object obj)
            => obj is SpaceObject other && Equals(other);

        public bool Equals(SpaceObject other)
            => other != null && Name == other.Name;

        public override int GetHashCode() => Name.GetHashCode();

        public override string ToString() => $"{Name}, {Connections}";
    }
}
