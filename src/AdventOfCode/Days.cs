using System.Reflection;

namespace Advent_of_Code;

public class Days
{
    static readonly IEnumerable<Type> All = typeof(Days).Assembly
        .GetExportedTypes()
        .Where(tp => tp.GetMethods().Exists(m => m.GetCustomAttributes<PuzzleAttribute>().NotEmpty()));

    [TestCaseSource(nameof(All))]
    public void Should_be_catogized(Type type)
    {
        var categories = type.GetCustomAttribute<CategoryAttribute>()?.Categories.Where(c => c != default) ?? Array.Empty<Category>();
        categories.Should().NotBeEmpty(because: type.FullName);
    }
}
