namespace Advent_of_Code.Rankings;

public sealed class CustomBoard
{
    public required string Board { get; init; }
    public required int Year { get; init; }

    public Participants Participants { get; init; } = [];

    public static CustomBoard Load(FileInfo file, Participants lookup)
    {
        var parts = file.Name.Split('_', '.');
        var board = parts[0];
        var year = int.Parse(parts[1]);
        var participants = new List<Participant>();

        using var reader = file.OpenText();
        
        while(reader.ReadLine() is { Length: > 0 } line)
        {
            if(lookup.Search(line) is { } participant)
            {
                participants.Add(participant);
            }
        }

        return new()
        {
            Board = board,
            Year = year,
            Participants = new(participants.Select(p => KeyValuePair.Create(p.Id, p))),
        };
    }
}
