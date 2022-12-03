var inputPath = File.Exists(Path.Join(Directory.GetCurrentDirectory(), "input.txt"))
    ? Path.Join(Directory.GetCurrentDirectory(), "input.txt")
    : Path.Join(Directory.GetCurrentDirectory(), "..", "..", "..", "input.txt");

var priorities = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

var rucksacks = File.ReadLines(inputPath).ToArray();

var priorityTotal = rucksacks
    .Select(r => new[] { r[..(r.Length / 2)], r[(r.Length / 2)..] })
    .Select(r => new HashSet<char>(r[0]).Intersect(new HashSet<char>(r[1])).First())
    .Select(p => priorities.IndexOf(p) + 1)
    .Sum();

Console.WriteLine($"Part 1: {priorityTotal}");

var x = priorities.ToArray();

var totalBadgePriority = rucksacks.Chunk(3)
    .Select(g => g.Select(i => i.ToArray().ToHashSet()).ToArray())
    .Select(g => g[0].Intersect(g[1]).ToHashSet().Intersect(g[2]).First())
    .Select(b => priorities.IndexOf(b) + 1)
    .Sum();

Console.WriteLine($"Part 2: {totalBadgePriority}");
