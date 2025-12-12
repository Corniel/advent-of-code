namespace Advent_of_Code_2020;

[Category(Category.Cryptography)]
public class Day_21
{
    [Example(answer: 5, "mxmxvkd kfcds sqjhc nhms (contains dairy, fish);trh fvjkl sbzzf mxmxvkd (contains dairy);sqjhc fvjkl (contains soy);sqjhc mxmxvkd sbzzf (contains fish)")]
    [Puzzle(answer: 2282, O.μs100)]
    public int part_one(Inputs<Food> foods)
    {
        var allergens = Allergen.Init(foods).As(allergen => allergen.Ingredient);
        return foods.SelectMany(f => f.Ingredients).Count(i => !allergens.Contains(i));
    }

    [Example(answer: "mxmxvkd,sqjhc,fvjkl", "mxmxvkd kfcds sqjhc nhms (contains dairy, fish);trh fvjkl sbzzf mxmxvkd (contains dairy);sqjhc fvjkl (contains soy);sqjhc mxmxvkd sbzzf (contains fish)")]
    [Puzzle(answer: "vrzkz,zjsh,hphcb,mbdksj,vzzxl,ctmzsr,rkzqs,zmhnj", O.μs10)]
    public string part_two(Inputs<Food> foods)
    {
        var allergens = Allergen.Init(foods);
        return string.Join(',', allergens.OrderBy(m => m.Name).Select(m => m.Ingredient));
    }

    public record Allergen(string Name, List<string> Ingredients)
    {
        public string Ingredient => Ingredients[0];
        public bool Resolved => Ingredients.Count == 1;
        
        public void Reduce(IEnumerable<Allergen> allergens)
        {
            foreach (var ingredient in allergens.Where(allergen => allergen != this && allergen.Resolved).Select(other => other.Ingredient))
                Ingredients.Remove(ingredient);
        }

        public static ImmutableArray<Allergen> Init(Inputs<Food> foods)
        {
            var allergens = foods
                .SelectMany(food => food.Allergens).Distinct().Order()
                .Fix(allergen => new Allergen(
                    Name: allergen,
                    Ingredients: [.. foods
                        .Where(food => food.Allergens.Contains(allergen))
                        .IntersectMany(food => food.Ingredients)]));

            while (allergens.Exists(allergen => !allergen.Resolved))
            {
                foreach (var allergen in allergens.Where(allergen => !allergen.Resolved))
                {
                    allergen.Reduce(allergens);
                }
            }
            return allergens;
        }
    }
    public record Food(string[] Ingredients, string[] Allergens)
    {
        public static Food Parse(string line)
        {
            var splitted = line.Split(" (contains ");
            return new Food(
                Ingredients: [.. splitted[0].SpaceSeparated()],
                Allergens: [.. splitted[1][..^1].CommaSeparated()]);
        }
    }
}
