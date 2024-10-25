namespace Advent_of_Code_2020;

[Category(Category.Cryptography)]
public class Day_04
{
    [Example(answer: 2, Example._1)]
    [Puzzle(answer: 228, O.μs100)]
    public int part_one(GroupedLines groups) => groups.Select(Passport.Parse).Count(p => p.IsValid());

    [Puzzle(answer: 175, O.μs100)]
    public int part_two(GroupedLines groups) => groups.Select(Passport.Parse).Count(p => p.StrictValid());

    public class Passport : Dictionary<string, string>
    {
        private bool duplicate;

        public bool byr => TryGetValue(nameof(byr), out var str)
            && int.TryParse(str, out var year) && year.InRange(1920, 2002);

        public bool iyr => TryGetValue(nameof(iyr), out var str)
            && int.TryParse(str, out var year) && year.InRange(2010, 2020);

        public bool hgt => TryGetValue(nameof(hgt), out var str)
           && int.TryParse(str[..^2], out var length)
           && (str.EndsWith("cm") && length.InRange(150, 193) ||
            (str.EndsWith("in") && length.InRange(59, 76)));

        public bool eyr => TryGetValue(nameof(eyr), out var str)
          && int.TryParse(str, out var year) && year.InRange(2020,2030);

        public bool ecl => TryGetValue(nameof(ecl), out var str)
            && Ecls.Contains(str);

        public bool hcl => TryGetValue(nameof(hcl), out var str) && str.IsMatch("^#[0-9a-f]{6}$");

        public bool pid => TryGetValue(nameof(pid), out var str) && str.IsMatch("^[0-9]{9}$");

        static readonly string[] Ecls = ["amb", "blu", "brn", "gry", "grn", "hzl", "oth"];

        public bool StrictValid() => IsValid()
            && byr && iyr && eyr && hgt && hcl && ecl && pid;

        public bool IsValid() => !duplicate
            && ContainsKey(nameof(byr)) && ContainsKey(nameof(iyr)) && ContainsKey(nameof(eyr)) && ContainsKey(nameof(hgt))
            && ContainsKey(nameof(hcl)) && ContainsKey(nameof(ecl)) && ContainsKey(nameof(pid))
            && ((ContainsKey("cid") && Count == 8) || Count == 7);

        public static Passport Parse(string[] lines)
        {
            var passport = new Passport();

            foreach (var line in lines)
            {
                foreach (var block in line.SpaceSeparated())
                {
                    var kvp = block.Separate(':');
                    var key = kvp[0];
                    var value = kvp[1];
                    passport.duplicate = passport.ContainsKey(key);
                    passport[key] = value;
                }
            }
            return passport;
        }
    }
}
