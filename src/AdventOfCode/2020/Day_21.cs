namespace Advent_of_Code_2020;

[Category(Category.Cryptography)]
public class Day_21
{
    [Example(answer: 5, @"
        mxmxvkd kfcds sqjhc nhms (contains dairy, fish)
        trh fvjkl sbzzf mxmxvkd (contains dairy)
        sqjhc fvjkl (contains soy)
        sqjhc mxmxvkd sbzzf (contains fish)")]
    [Puzzle(answer: 2282, year: 2020, day: 21)]
    public long part_one(string input)
    {
        var foods = input.Lines(Food.Parse).ToArray();
        var allergens = Allergen.Init(foods).Select(allergen => allergen.Ingredient).ToArray();
        return foods.SelectMany(f => f.Ingredients).Count(i => !allergens.Contains(i));
    }

    [Example(answer: "mxmxvkd,sqjhc,fvjkl", @"
        mxmxvkd kfcds sqjhc nhms (contains dairy, fish)
        trh fvjkl sbzzf mxmxvkd (contains dairy)
        sqjhc fvjkl (contains soy)
        sqjhc mxmxvkd sbzzf (contains fish)")]
    [Puzzle(answer: "vrzkz,zjsh,hphcb,mbdksj,vzzxl,ctmzsr,rkzqs,zmhnj", year: 2020, day: 21)]
    public string part_two(string input)
    {
        var allergens = Allergen.Init(input.Lines(Food.Parse));
        return string.Join(',', allergens.OrderBy(m => m.Name).Select(m => m.Ingredient));
    }

    private record Allergen(string Name, List<string> Ingredients)
    {
        public string Ingredient => Ingredients[0];
        public bool Resolved => Ingredients.Count == 1;
        private void Reduce(IEnumerable<Allergen> allergens)
        {
            foreach (var ingredient in allergens.Where(allergen => allergen != this && allergen.Resolved).Select(other => other.Ingredient))
            {
                Ingredients.Remove(ingredient);
            }
        }
        public static Allergen[] Init(IEnumerable<Food> foods)
        {
            var allergens = foods
                .SelectMany(food => food.Allergens).Distinct().OrderBy(a => a)
                .Select(allergen => new Allergen(
                    Name: allergen,
                    Ingredients: foods
                        .Where(food => food.Allergens.Contains(allergen))
                        .IntersectMany(food => food.Ingredients)
                        .ToList()))
                .ToArray();

            while (allergens.Any(allergen => !allergen.Resolved))
            {
                foreach (var allergen in allergens.Where(allergen => !allergen.Resolved))
                {
                    allergen.Reduce(allergens);
                }
            }
            return allergens;
        }
    }
    private record Food(string[] Ingredients, string[] Allergens)
    {
        public static Food Parse(string line)
        {
            var splitted = line.Split(" (contains ");
            return new Food(
                Ingredients: splitted[0].SpaceSeperated().ToArray(),
                Allergens: splitted[1][..^1].CommaSeperated().ToArray());
        }
    }
}
