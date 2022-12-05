namespace Advent_of_Code;

public enum Category
{
    [Obsolete("Choose an appropriate category.")]
    None,
    /// <summary>Nanoseconds of magnitude.</summary>
    ns,
    /// <summary>Microseconds of magnitude.</summary>
    μs,
    /// <summary>Milliseconds of magnitude.</summary>
    ms,
    /// <summary>Seconds of magnitude.</summary>
    sec,
    ASCII,
    BitManupilation,
    Cryptography,
    Computation,
    ExpressionParsing,
    GameOfLife,
    Graph,
    Grid,
    IntComputer,
    PathFinding,
    SequenceProgression,
    Simulation,
    VectorAlgebra,
    _2D,
    _3D,
    _4D,
}
