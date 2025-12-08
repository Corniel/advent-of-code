namespace Advent_of_Code_2022;

[Category(Category.SequenceProgression)]
public class Day_19
{
    [Example(answer: 09, "Blueprint 1: Each ore robot costs 4 ore. Each clay robot costs 2 ore. Each obsidian robot costs 3 ore and 14 clay. Each geode robot costs 2 ore and 07 obsidian.")]
    [Example(answer: 12, "Blueprint 1: Each ore robot costs 2 ore. Each clay robot costs 3 ore. Each obsidian robot costs 3 ore and 08 clay. Each geode robot costs 3 ore and 12 obsidian.")]
    [Example(answer: 00, "Blueprint 1: Each ore robot costs 3 ore. Each clay robot costs 9 ore. Each obsidian robot costs 2 ore and 19 clay. Each geode robot costs 9 ore and 13 obsidian.")]
    [Puzzle(answer: 1081, O.ms100)]
    public int part_one(Inputs<Blueprint> input) => input.Sum(p => Produces(p, 24) * p.Id);

    [Puzzle(answer: 2415, O.ms100)]
    public int part_two(Inputs<Blueprint> input) => input[..3].Select(p => Produces(p, 32)).Product();

    static int Produces(Blueprint p, int turns) => Max(p, new State(turns, default, new(Ore: 1)), []);

    static int Max(Blueprint p, State state, Dictionary<State, int> done)
    {
        if (state.Done(p)) return state.Curr.Geo;
        else if (done.TryGetValue(state, out var geo)) return geo;
        else
        {
            var max = state.Nexts(p).Select(n => Max(p, n.Next(state.Prod), done)).Max();
            done[state] = max;
            return max;
        }
    }

    public record struct Resource(short Ore = 0, short Cly = 0, short Obs = 0, short Geo = 0)
    {
        public readonly bool CanBuild(Bot bot) => Ore >= bot.Cost.Ore && Cly >= bot.Cost.Cly && Obs >= bot.Cost.Obs && Geo >= bot.Cost.Geo;

        public static Resource operator +(Resource l, Resource r) => new((short)(l.Ore + r.Ore), (short)(l.Cly + r.Cly), (short)(l.Obs + r.Obs), (short)(l.Geo + r.Geo));
        public static Resource operator -(Resource l, Resource r) => new((short)(l.Ore - r.Ore), (short)(l.Cly - r.Cly), (short)(l.Obs - r.Obs), (short)(l.Geo - r.Geo));
    }

    record struct State(int Time, Resource Curr, Resource Prod)
    {
        public State Next(Resource prod) => this with { Time = Time - 1, Curr = Curr + prod };
        public readonly bool Done(Blueprint p) => Time == 0 || (Prod.Obs == 0 && p.Geo.Cost.Obs > Time * 2);
        public State Build(Bot bot) => this with { Curr = Curr - bot.Cost, Prod = Prod + bot.Prod };
        public IEnumerable<State> Nexts(Blueprint p)
        {
            var prev = Curr - Prod;

            if (Curr.CanBuild(p.Geo)) { yield return Build(p.Geo); yield break; }

            if (!prev.CanBuild(p.Obs) && Curr.CanBuild(p.Obs)) yield return Build(p.Obs);
            if (!prev.CanBuild(p.Cly) && Curr.CanBuild(p.Cly)) yield return Build(p.Cly);
            if (!prev.CanBuild(p.Ore) && Curr.CanBuild(p.Ore) && NeedOre(p)) yield return Build(p.Ore);

            yield return this;
        }

        readonly bool NeedOre(Blueprint p) => Prod.Ore < p.Geo.Cost.Ore || Prod.Ore < p.Obs.Cost.Ore || Prod.Ore < p.Cly.Cost.Ore;
    }

    public record Bot(Resource Cost, Resource Prod);

    public record Blueprint(int Id, Bot Ore, Bot Cly, Bot Obs, Bot Geo)
    {
        public static Blueprint Parse(string line)
        {
            short[] n = [..line.Int32s().Select(n => (short)n)];
            return new Blueprint(n[0],
                Ore: new Bot(new(Ore: n[1]), new(Ore: 1)),
                Cly: new Bot(new(Ore: n[2]), new(Cly: 1)),
                Obs: new Bot(new(Ore: n[3], Cly: n[4]), new(Obs: 1)),
                Geo: new Bot(new(Ore: n[5], Obs: n[6]), new(Geo: 1))
            );
        }
    }
}
