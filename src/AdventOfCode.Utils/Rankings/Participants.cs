namespace Advent_of_Code.Rankings;

public class Participants : Dictionary<long, Participant> 
{
    public Participants() { }
    public Participants(IEnumerable<KeyValuePair<long, Participant>> collection) : base(collection) { }

    public Participant? Search(string name)
        => Values.FirstOrDefault(p => p.Matches(name));
}
