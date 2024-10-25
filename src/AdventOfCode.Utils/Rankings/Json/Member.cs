using System.Text.Json.Serialization;

namespace Advent_of_Code.Rankings.Json;

public class Member
{
    [JsonPropertyName("id")]
    public long Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("global_score")]
    public int GlobalScore { get; set; }
    [JsonPropertyName("completion_day_level")]
    public Dictionary<int, Day> Days { get; set; }
}
