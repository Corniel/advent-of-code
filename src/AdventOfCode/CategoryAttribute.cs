namespace Advent_of_Code
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CategoryAttribute(params Category[] catagories) : Attribute, IApplyToTest
    {
        public IReadOnlyCollection<Category> Categories { get; } = catagories;

        public void ApplyToTest(Test test)
        {
            foreach (var category in Categories)
            {
                test.Properties.Add(PropertyNames.Category, category);
            }
        }

        public override string ToString() => string.Join(',', Categories);
    }
}
