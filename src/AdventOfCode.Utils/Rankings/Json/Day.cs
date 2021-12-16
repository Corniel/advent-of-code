using System.Text.Json.Serialization;

namespace Advent_of_Code.Rankings.Json;

public class Day
{
    [JsonPropertyName("1")]
    public Part One { get; set; }
    [JsonPropertyName("2")]
    public Part Two { get; set; }
}
