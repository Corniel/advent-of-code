namespace Advent_of_Code_2019;

[DebuggerDisplay("{Answer}, Ouput: {Output.Count}")]
[DebuggerTypeProxy(typeof(CollectionDebugView))]
public class Results : IEnumerable<Int>
{
    public Results(Int answer, IReadOnlyList<Int> output)
    {
        Answer = answer;
        Output = output;
    }
    public Int Answer { get; internal set; }
    public IReadOnlyList<Int> Output { get; }
    public bool None() => Output.Count == 0;

    public IEnumerator<Int> GetEnumerator() => Output.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
