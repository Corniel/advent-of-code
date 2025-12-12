namespace SmartAss.Expressions;

[DebuggerTypeProxy(typeof(CollectionDebugView))]
[DebuggerDisplay("Count = {Count}")]
public sealed class Params : IReadOnlyCollection<Param>
{
    public Params() : this(new([]), [], [], false) { }

    private Params(
        Lines visited,
        Dictionary<string, Expr> lookup,
        Dictionary<string, long> cache,
        bool withCache)
    {
        Visited = visited;
        Lookup = lookup;
        Stored = cache;
        WithCache = withCache;
    }

    private readonly Lines Visited;
    private readonly Dictionary<string, Expr> Lookup;
    private readonly Dictionary<string, long> Stored;
    private bool WithCache;

    public int Count => Lookup.Count;

    public Expr this[string param]
    {
        get => Lookup[param];
        set => Lookup[param] = value;
    }

    public Expr TryGet(string param) 
        => Lookup.TryGetValue(param, out var expr)
        ? expr
        : null;

    public long Value(string param)
    {
        using var _ = Cache();
        return TryValue(param) ?? throw new NotSolved();
    }

    public long? TryValue(string param)
    {
        if (WithCache)
        {
            if (!Stored.TryGetValue(param, out var value))
            {
                if (Trace(param) is { } trace 
                    &&Lookup[param].TryValue(trace) is { } check)
                {
                    Stored[param] = check;
                    return check;
                }
                else return null;
            }
            else return value;
        }
        else
        {
            if (Trace(param) is { } trace)
            {
                return Lookup[param].TryValue(trace);
            }
            else return null;
        }
    }

    private Params Trace(string param)
    {
        if (Visited.Contains(param)) return null;
        return new(new([..Visited, param]), Lookup, Stored, WithCache);
    }

    public IDisposable Cache() => new Cached(this);

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

    private sealed class Cached : IDisposable
    {
        internal Cached(Params pars)
        {
            Pars = pars;
            Pars.WithCache = true;
        }
        readonly Params Pars;
   
        public void Dispose()
        {
            Pars.WithCache= false;
            Pars.Stored.Clear();
        }
    }
}
