using System.Reflection;

namespace Specs.Days;

public class Are
{
    static readonly AdventPuzzles Puzzles = AdventPuzzles.Load();

    [TestCaseSource(nameof(Puzzles))]
    public void Catogized(AdventPuzzle puzzle)
    {
        var categories = puzzle.Method.DeclaringType.GetCustomAttribute<CategoryAttribute>()?.Categories.Where(c => c != default) ?? Array.Empty<Category>();
        categories.Should().NotBeEmpty(because: puzzle.Date.ToString());
    }

    [TestCaseSource(nameof(Puzzles))]
    public void Parameter_matching_type(AdventPuzzle puzzle)
    {
        var parameter = puzzle.Method.GetParameters()[0];
        parameter.Name.Should().Match(Name(parameter.ParameterType), because: puzzle.Date.ToString());

        static string Name(Type type)
        {
            if (type == typeof(string)) return "str";
            else if (type == typeof(Lines)) return "lines";
            else if (type == typeof(GroupedLines)) return "groups";
            else if (type == typeof(CharPixels)) return "chars";
            else if (type == typeof(CharGrid)) return "map";
            else if (type == typeof(Ints) || type == typeof(Longs)) return "numbers";
            else return "*";
        }
    }


}
