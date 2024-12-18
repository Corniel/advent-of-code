namespace Advent_of_Code_2024;

[Category(Category.SequenceProgression)]
public class Day_11
{
    [Puzzle(answer: 193269L, "475449 2599064 213 0 2 65 5755 51149", O.μs100)]
    public long part_one(Longs numbers) => Blink(numbers, 25);

    [Puzzle(answer: 228449040027793, "475449 2599064 213 0 2 65 5755 51149", O.ms)]
    public long part_two(Longs numbers) => Blink(numbers, 75);

    static long Blink(Longs numbers, int blinks)
    {
        var curr = ItemCounter.New(numbers); var next = ItemCounter.New<long>();

        for (var blink = 0; blink < blinks; blink++)
        {
            foreach (var (n, count) in curr)
            {
                if (n == 0) next[1] += count;
                else
                {
                    var digits = n.DigitCount();
                    if (digits.IsEven())
                    {
                        var f = 10.Pow(digits / 2);
                        next[n / f] += count; 
                        next[n % f] += count;
                    }
                    else next[n * 2024] += count;
                }
            }
            (curr, next) = (next, curr.Clear());
        }
        return curr.Total;
    }
}
