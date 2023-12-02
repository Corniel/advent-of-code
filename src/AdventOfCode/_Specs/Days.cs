using System.Reflection;

namespace Specs.Days;

public class Are
{
    static readonly IEnumerable<Type> All = typeof(Are).Assembly
        .GetExportedTypes()
        .Where(tp => tp.GetMethods().Exists(m => m.GetCustomAttributes<PuzzleAttribute>().NotEmpty()));

    [TestCaseSource(nameof(All))]
    public void Catogized(Type type)
    {
        var categories = type.GetCustomAttribute<CategoryAttribute>()?.Categories.Where(c => c != default) ?? Array.Empty<Category>();
        categories.Should().NotBeEmpty(because: type.FullName);
    }
}
