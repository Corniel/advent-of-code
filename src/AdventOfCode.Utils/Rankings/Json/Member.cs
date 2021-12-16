using System.Text.Json.Serialization;

namespace Advent_of_Code.Rankings.Json;

public class Member
{
    [JsonPropertyName("id")]
    public string id { get; set; }
    public long Id => id.Int64();
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("global_score")]
    public int GlobalScore { get; set; }
    [JsonPropertyName("completion_day_level")]
    public Dictionary<int, Day> Days { get; set; }
}
