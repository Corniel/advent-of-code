using System.Text.Json.Serialization;

namespace Advent_of_Code.Rankings.Json;

public class Part
{
    [JsonPropertyName("get_star_ts")]
    public long TimeStamp { get; set; }
    public DateTime Time => DateTime.UnixEpoch.AddSeconds(TimeStamp);
}
