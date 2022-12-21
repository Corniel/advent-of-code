namespace SmartAss.Expressions;

[DebuggerTypeProxy(typeof(CollectionDebugView))]
[DebuggerDisplay("Count = {Count}")]
public sealed class Params : IReadOnlyCollection<Param>
{
    private readonly Dictionary<string, Expr> Lookup = new();

    public int Count => Lookup.Count;

    public Expr this[string name]
    {
        get => Lookup[name];
        set => Lookup[name] = value;
    }

    public long Value(string param) => TryValue(param) ?? throw new NotSolved();
    
    public long? TryValue(string param) => Lookup[param].TryValue(this);

    public IEnumerable<long> Values() => Lookup.Values.Select(v => v.Value(this));

    public static Params New(IEnumerable<Param> pars)
    {
        var @params = new Params();
        foreach (var par in pars) 
        {
            @params.Lookup.Add(par.Name, par.Expr);
        }
        return @params;
    }

    public IEnumerator<Param> GetEnumerator() => Lookup.Select(kvp => new Param(kvp.Key, kvp.Value)).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
