namespace SmartAss;

public static class Parse
{
    [Pure]
    public static Vector Dir(char c) => c switch
    {
        '^' or 'U' => Vector.N,
        '>' or 'R' => Vector.E,
        'v' or 'D' => Vector.S,
        '<' or 'L' => Vector.W,

        _ => throw Qowaiv.Text.Unparsable.ForValue<Vector>($"'{c}'", "Not a valid direction.")
    };
}
