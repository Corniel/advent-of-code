namespace Advent_of_Code_2025;

/// <summary>
/// The safe has a dial with the numbers 0 through 99.
/// 
/// Part one: Count the number of times you stop at zero.
/// Part two: Count the number of times you pass zero.
/// </summary>
[Category(Category.SequenceProgression)]
public class Day_01
{
    [Example(answer: 3, "L68;L30;R48;L5;R60;L55;L1;L99;R14;L82")]
    [Puzzle(answer: 1150, O.μs10)]
    public int part_one(Ints numbers)
    {
        var pt = 50; var zero = 0;

        foreach (var rot in numbers)
            if ((pt = (pt + rot).Mod(100)) is 0) zero++;

        return zero;
    }

    [Example(answer: 6, "L68;L30;R48;L5;R60;L55;L1;L99;R14;L82")]
    [Example(answer: 10, "R1000")]
    [Puzzle(answer: 6738, O.μs10)]
    public int part_two(Ints numbers)
    {
        var pt = 50; var pass = 0;

        foreach (var rot in numbers)
        {
            var (mod, rot_) = Math.DivRem(rot.Abs(), 100);

            pass += rot > 0
                // Above 100.
                ? pt + rot_ >= 100 ? 1 : 0
                //  Below 0, not starting there.
                : pt > 0 && pt <= rot_ ? 1 : 0;

            // Add extra rotations.
            pass += mod;
            pt = (pt + rot).Mod(100);
        }

        return pass;
    }

    /// <summary>L is positive, R negative.</summary>
    public static int Parse(string l) => l[0] is 'L' ? l.Int32() : -l.Int32();
}
