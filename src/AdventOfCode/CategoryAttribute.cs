namespace Advent_of_Code
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CategoryAttribute : Attribute
    {
        public CategoryAttribute(params Category[] catagories) => Categories = catagories;
        public IReadOnlyCollection<Category> Categories { get; }
        public override string ToString() => string.Join(',', Categories);
    }
}
