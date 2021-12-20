using System.Text.Json;

namespace Advent_of_Code_2015;

[Category(Category.ExpressionParsing)]
public class Day_12
{
    [Puzzle(answer: 111754, year: 2015, day: 12)]
    public int part_one(string input) => input.Int32s().Sum();

    [Puzzle(answer: 65402, year: 2015, day: 12)]
    public int part_two(string input) => Sum(JsonDocument.Parse(input).RootElement);

    static int Sum(JsonElement elm)
    {
        return elm.ValueKind switch
        {
            JsonValueKind.Number => elm.GetInt32(),
            JsonValueKind.Object => IsRed(elm) ? 0 : elm.EnumerateObject().Sum(child => Sum(child.Value)),
            JsonValueKind.Array => elm.EnumerateArray().Sum(child => Sum(child)),
            _ => 0,
        };
    }

    static bool IsRed(JsonElement elm)
        => elm.EnumerateObject()
        .Any(prop => prop.Value.ValueKind == JsonValueKind.String && prop.Value.GetString() == "red");
 }
 