namespace Advent_of_Code_2015;

[Category(Category.Computation)]
public class Day_15
{
    [Example(answer: 62842880, "Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8;Cinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3")]
    [Puzzle(answer: 21367368, O.ms10)]
    public int part_one(Lines lines)
        => Total(lines, (distribution, ingredients) => true);

    [Example(answer: 57600000, "Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8;Cinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3")]
    [Puzzle(answer: 1766400, O.ms10)]
    public int part_two(Lines lines)
        => Total(lines, (distribution, ingredients) => Score(distribution, ingredients, i => i.Calories) == 500);

    static int Total(Lines lines, Func<int[], Ingredient[], bool> where)
    {
        var ingredients = lines.As(Ingredient.Parse).ToArray();
        var selectors = new Func<Ingredient, int>[] { i => i.Capacity, i => i.Durability, i => i.Flavor, i => i.Texture };
        return new Distribution(ingredients.Length)
            .Where(d => where(d, ingredients))
            .Max(distribution => selectors.Select(selector => Score(distribution, ingredients, selector)).Product());
    }
    static int Score(int[] distribution, Ingredient[] ingredients, Func<Ingredient, int> selector)
        => Math.Max(0, distribution.Select((count, index) => count * selector(ingredients[index])).Sum());

    record Ingredient(int Capacity, int Durability, int Flavor, int Texture, int Calories)
    {
        public static Ingredient Parse(string line)
        {
            var p = line.Int32s().ToArray();
            return new(p[0], p[1], p[2], p[3], p[4]);
        }
    }

    struct Distribution(int elements) : Iterator<int[]>
    {
        private int Number = -1;

        public int[] Current { get; private set; } = new int[elements];

        public bool MoveNext()
        {
            while(true)
            {
                if (!Fill(++Number)) return false;
                else if (Current[0] >= 0) return true;
            }
            throw new InfiniteLoop();
        }

        readonly bool Fill(int remainder)
        {
            Current[0] = 100;
            var pos = 1;
            while(remainder > 0 && pos < Current.Length)
            {
                var total = remainder & 0x7F;
                Current[0] -= total;
                remainder >>= 7;

                if (Current[0] < 0) return true;
                else { Current[pos++] = total; }
            }
            return remainder == 0;
        }

        public readonly void Reset() => Do.Nothing();
        public readonly void Dispose() => Do.Nothing();
    }
}
