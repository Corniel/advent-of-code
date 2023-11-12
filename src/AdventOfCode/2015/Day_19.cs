using Qowaiv.Hashing;

namespace Advent_of_Code_2015;

[Category(Category.SequenceProgression)]
public class Day_19
{
    [Example(answer: 4, "H => HO;H => OH;O => HH;;HOH")]
    [Example(answer: 7, "H => HO;H => OH;O => HH;;HOHOHO")]
    [Puzzle(answer: 535, O.ms)]
    public int part_one(GroupedLines groups)
    {
        var molecule = new Molecule([.. Atom.Parse(groups[1][0])]);
        var molecules = new HashSet<Molecule>(groups[0].Select(Replace.Parse).SelectMany(molecule.Replace));
        return molecules.Count;
    }

    // https://www.reddit.com/r/adventofcode/comments/3xflz8/comment/cy4h7ji/
    // All of the rules are of one of the following forms:
    //
    // α => βγ
    // α => βRnγAr
    // α => βRnγYδAr
    // α => βRnγYδYεAr
    //
    //As Rn, Ar, and Y are only on the left side of the equation, one merely
    // only needs to compute #NumSymbols - #Rn - #Ar - 2 * #Y - 1
    //
    // Subtract of #Rn and #Ar because those are just extras. Subtract
    // two times #Y because we get rid of the Ys and the extra elements
    // following them. Subtract one because we start with "e".
    [Puzzle(answer: 212, O.μs)]
    public int part_two(Lines lines)
        => lines[^1].Count(char.IsUpper)
        - lines[^1].Count("Rn")
        - lines[^1].Count("Ar")
        - 2 * lines[^1].Count("Y") - 1;

    sealed record Replace(Atom Atom, Molecule Molecule)
    {
        public static Replace Parse(string line)
        {
            Atom[] all = [..Atom.Parse(line.Replace(" => ", ""))];
            return new(all[0], new(all[1..]));
        }
    }

    readonly struct Molecule(Atom[] atoms) : IEquatable<Molecule>
    {
        public readonly Atom[] Atoms = atoms;

        public bool Equals(Molecule other) => Atoms.SequenceEqual(other.Atoms);

        public  override int GetHashCode() => Hash.Code(Atoms);

        public IEnumerable<Molecule> Replace(Replace rep)
        {
            var mol = Atoms;
            return mol.Select((atom, index) => (atom, index))
                .Where(a => a.atom == rep.Atom)
                .Select(a => new Molecule([.. mol[0..a.index], .. rep.Molecule.Atoms, .. mol[(a.index + 1)..]]));
        }
    }

    // Puts one ore two chars in a short.
    record struct Atom(short Val)
    {
        public static IEnumerable<Atom> Parse(string str)
        {
            for (var i = 0; i < str.Length; i++)
            {
                var ch = (short)str[i];
                if (i < str.Length - 1 && char.IsLower(str[i + 1]))
                {
                    ch |= (short)(str[i + 1] << 8);
                    i++;
                }
                yield return new(ch);
            }
        }
    }
}
