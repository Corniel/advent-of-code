namespace SmartAss.Circuits;

public abstract record CircuitNode<TValue> where TValue : struct
{
    public TValue? Output
    {
        get
        {
            value ??= Execute();
            return value;
        }
    }
    private TValue? value;

    protected abstract TValue? Execute();
}
