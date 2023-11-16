namespace SmartAss.Expressions;

[DebuggerTypeProxy(typeof(CollectionDebugView))]
[DebuggerDisplay("Count = {Count}")]
public sealed class Params : IReadOnlyCollection<Param>
{
    private readonly Dictionary<string, Expr> Lookup = [];
    private readonly Dictionary<string, long> Cache = [];
    private bool WithCache;

    public int Count => Lookup.Count;

    public Expr this[string name]
    {
        get => Lookup[name];
        set => Lookup[name] = value;
    }

    public long Value(string param)
    {
        WithCache = true;
        var value = TryValue(param) ?? throw new NotSolved();
        WithCache = false;
        Cache.Clear();
        return value;
    }

    public long? TryValue(string param)
    {
        if (WithCache)
        {
            if (!Cache.TryGetValue(param, out var value))
            {
                if (Lookup[param].TryValue(this) is { } check)
                {
                    Cache[param] = check;
                    return check;
                }
                else return null;
            }
            else return value;  
        }
        else return Lookup[param].TryValue(this);
    }

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
