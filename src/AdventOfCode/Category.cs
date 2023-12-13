#pragma warning disable S1133 // Deprecated code should be removed, but not this one.

namespace Advent_of_Code;

public enum Category
{

    [Obsolete("Choose an appropriate category.")]
    None,
    ASCII,
    BitManupilation,
    Cryptography,
    Computation,
    ExpressionEvaluation,
    ExpressionParsing,
    GameOfLife,
    Graph,
    Grid,
    IntComputer,
    PathFinding,
    Permutations,
    SequenceProgression,
    Simulation,
    VectorAlgebra,
    _2D,
    _3D,
    _4D,
}
