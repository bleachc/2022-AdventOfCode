var inputPath = File.Exists(Path.Join(Directory.GetCurrentDirectory(), "input.txt"))
    ? Path.Join(Directory.GetCurrentDirectory(), "input.txt")
    : Path.Join(Directory.GetCurrentDirectory(), "..", "..", "..", "input.txt");

int GetMarker(int distinctCharacters)
{
    var signal = File.ReadAllText(inputPath).AsSpan();
    
    for (var i = distinctCharacters - 1; i < signal.Length; i++)
    {
        if (signal.Slice(i - distinctCharacters + 1, distinctCharacters).ToArray().ToHashSet().Count == distinctCharacters)
        {
            return i + 1;
        }
    }

    return -1;
}

Console.WriteLine($"Part One: {GetMarker(4)}");
Console.WriteLine($"Part Two: {GetMarker(14)}");
