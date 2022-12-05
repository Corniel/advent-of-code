namespace Advent_of_Code
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CategoryAttribute : Attribute, IApplyToTest
    {
        public CategoryAttribute(params Category[] catagories) => Categories = catagories;
        public IReadOnlyCollection<Category> Categories { get; }

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
