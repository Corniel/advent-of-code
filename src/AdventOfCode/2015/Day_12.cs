using System.Text.Json;

namespace Advent_of_Code_2015;

[Category(Category.ExpressionParsing)]
public class Day_12
{
    [Puzzle(answer: 111754, O.μs)]
    public int part_one(Ints numbers) => numbers.Sum();

    [Puzzle(answer: 65402, O.μs100)]
    public int part_two(string str) => Sum(JsonDocument.Parse(str).RootElement);

    static int Sum(JsonElement elm) => elm.ValueKind switch
    {
        JsonValueKind.Number => elm.GetInt32(),
        JsonValueKind.Object => IsRed(elm) ? 0 : elm.EnumerateObject().Sum(child => Sum(child.Value)),
        JsonValueKind.Array => elm.EnumerateArray().Sum(Sum),
        _ => 0,
    };

    static bool IsRed(JsonElement elm) => elm.EnumerateObject()
        .Any(prop => prop.Value.ValueKind == JsonValueKind.String && prop.Value.GetString() == "red");
 }
