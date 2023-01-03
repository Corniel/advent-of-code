using Qowaiv.Hashing;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Advent_of_Code_2015;

[Category(Category.SequenceProgression)]
public class Day_19
{
    [Example(answer: 4, "H => HO;H => OH;O => HH;;HOH")]
    [Example(answer: 7, "H => HO;H => OH;O => HH;;HOHOHO")]
    [Puzzle(answer: 535, O.ms)]
    public int part_one(string input)
    {
        var group = input.GroupedLines().ToArray();
        var molecule = new Molecule(Atom.Parse(group[1][0]).ToArray());
        var replacements = group[0].Select(Replace.Parse).ToArray();
        var molecules = new HashSet<Molecule>(new Distinct());

        foreach(var r in replacements)
        {
            molecules.AddRange(molecule.Replace(r));
        }
        return molecules.Count;
    }

    [Example(answer: 3, "e => H;e => O;H => HO;H => OH;O => HH;;HOH")]
    [Example(answer: 6, "e => H;e => O;H => HO;H => OH;O => HH;;HOHOHO")]
    [Puzzle(answer: 535, O.ms)]
    public int part_two(string input)
    {
        var group = input.GroupedLines().ToArray();
        var molecule = new Molecule(Atom.Parse(group[1][0]).ToArray());
        var replacements = group[0].Select(Replace.Parse).ToArray();
        
        var molecules = new HashSet<Molecule>(new Distinct());

        foreach (var r in replacements)
        {
            molecules.AddRange(molecule.Replace(r));
        }
        return molecules.Count;
    }
    

    public sealed record Replace(Atom Atom, Molecule Molecule, bool Containing)
    {
        public static Replace Parse(string line)
        {
            var all = Atom.Parse(line.Replace(" => ", "")).ToArray();
            var atom = all[0];
            var atoms = all[1..];
            return new(atom, new(atoms), atoms.Contains(atom));
        }
    }

    public sealed class Distinct : IEqualityComparer<Molecule>
    {
        public bool Equals(Molecule x, Molecule y) => x.Atoms.SequenceEqual(y.Atoms);
        public int GetHashCode(Molecule obj) => Hash.Code(obj.Atoms);
    }

    public record struct Molecule(Atom[] Atoms)
    {
        public IEnumerable<Molecule> Replace(Replace rep)
        {
            var atoms = Atoms;

            return atoms.Select((atom, index) => (atom, index))
                .Where(a => a.atom == rep.Atom)
                .Select(a => new Molecule(
                    atoms[0..a.index].Concat(rep.Molecule.Atoms).Concat(atoms[(a.index + 1)..]).ToArray()));
        }

        public override string ToString() => string.Concat(Atoms);
    }

    public record struct Atom(short Val)
    {
        public override string ToString() => Val < 255 ? $"{(char)(Val & 255)}" : $"{(char)(Val & 255)}{(char)(Val >> 8)}";


        public static IEnumerable<Atom> Parse(string str)
        {
            for(var i = 0; i < str.Length; i++)
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
