namespace Advent_of_Code_2015;

[Category(Category.Simulation)]
public class Day_14
{
    [Example(answer: 2660, "Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.;Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.")]
    [Puzzle(answer: 2640, O.μs)]
    public int part_one(Inputs<Reindeer> inputs) => inputs.As(r => r.Travel(2503)).Max();

    [Example(answer: 1564, "Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.;Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.")]
    [Puzzle(answer: 1102, O.μs100)]
    public int part_two(Inputs<Reindeer> inputs)
    {
        var distances = inputs.ToDictionary(r => r, r => 0);
        var points = distances.ToDictionary(kvp => kvp.Key, kvp => 0);

        for (var s = 0; s < 2503; s++)
        {
            foreach (var reindeer in distances.Keys.Where(r => r.Flying(s)))
                distances[reindeer] += reindeer.Speed;

            var max = distances.Values.Max();
            
            foreach (var (key, _) in distances.Where(kvp => kvp.Value == max)) points[key]++;
        }
        return points.Values.Max();
    }

    public record Reindeer(int Speed, int Fly, int Rest)
    {
        public int Period => Fly + Rest;
        public bool Flying(int duration) => duration % Period < Fly;
        public int Travel(int duration)
        {
            var periods = duration / Period;
            var period_last = duration % Period;
            return Speed * (Fly * periods + Math.Min(Fly, period_last));
        }

        public static Reindeer Parse(string line) => Ctor.New<Reindeer>(line.Int32s());
    }
}
