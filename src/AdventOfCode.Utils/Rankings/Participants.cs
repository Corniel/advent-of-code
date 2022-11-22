namespace Advent_of_Code.Rankings;

public class Participants : Dictionary<long, Participant> 
{
    public Participants() { }
    public Participants(IEnumerable<KeyValuePair<long, Participant>> collection) : base(collection) { }
}
