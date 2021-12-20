namespace Advent_of_Code_2015;

[Category(Category.Computation)]
public class Day_15
{
    [Example(answer: 62842880, "Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8;Cinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3")]
    [Puzzle(answer: 21367368, year: 2015, day: 15)]
    public long part_one(string input)
        => TotalScore(input, (distribution, ingredients) => true);

    [Example(answer: 57600000, "Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8;Cinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3")]
    [Puzzle(answer: 1766400, year: 2015, day: 15)]
    public long part_two(string input)
        => TotalScore(input, (distribution, ingredients) => Score(distribution, ingredients, i => i.Calories) == 500);

    static long TotalScore(string input, Func<int[], Ingredient[], bool> where)
    {
        var ingredients = input.Lines(Ingredient.Parse).ToArray();
        var selectors = new Func<Ingredient, long>[] { i => i.Capacity, i => i.Durability, i => i.Flavor, i => i.Texture };
        return new Distribution(ingredients.Length)
            .Where(d => where(d, ingredients))
            .Max(distribution => selectors.Select(selector => Score(distribution, ingredients, selector)).Product());
    }
    static long Score(int[] distribution, Ingredient[] ingredients, Func<Ingredient, long> selector)
        => Math.Max(0, distribution.Select((count, index) => count * selector(ingredients[index])).Sum());

    record Ingredient(int Capacity, int Durability, int Flavor, int Texture, int Calories)
    {
        public static Ingredient Parse(string line)
        {
            var p = line.Seperate(": capacity ", ", durability ", ", flavor ", ", texture ", ", calories ");
            return new(p[1].Int32(), p[2].Int32(), p[3].Int32(), p[4].Int32(), p[5].Int32());
        }
    }

    struct Distribution : Iterator<int[]>
    {
        private int Number;

        public Distribution(int elements)
        {
            Current = new int[elements];
            Number = -1;
        }

        public int[] Current { get; private set; }

        public bool MoveNext()
        {
            while(true)
            {
                if (!Fill(++Number)) return false;
                else if (Current[0] >= 0) return true;
            }
            throw new InfiniteLoop();
        }

        private bool Fill(int remainder)
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

        public void Reset() => Do.Nothing();
        public void Dispose() => Do.Nothing();
    }
}
