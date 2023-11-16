namespace Advent_of_Code_2019;

[DebuggerDisplay("{Answer}, Ouput: {Output.Count}")]
[DebuggerTypeProxy(typeof(CollectionDebugView))]
public class Results(Int answer, IReadOnlyList<Int> output) : IEnumerable<Int>
{
    public Int Answer { get; internal set; } = answer;
    public IReadOnlyList<Int> Output { get; } = output;

    public IEnumerator<Int> GetEnumerator() => Output.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
