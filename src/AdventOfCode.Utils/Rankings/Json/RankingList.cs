using System.Text.Json.Serialization;

namespace Advent_of_Code.Rankings.Json;

public class RankingList
{
    [JsonPropertyName("members")]
    public Dictionary<long, Member> Members { get; set; }
}
