var inputPath = File.Exists(Path.Join(Directory.GetCurrentDirectory(), "input.txt"))
    ? Path.Join(Directory.GetCurrentDirectory(), "input.txt")
    : Path.Join(Directory.GetCurrentDirectory(), "..", "..", "..", "input.txt");

var input = File.ReadAllText(inputPath);

var inventories = input
    .Split("\n\n")
    .Select(i => i
        .Split('\n')
        .Where(x => !string.Empty.Equals(x))
        .Select(int.Parse)
    )
    .ToArray();

var maxCalorieInventory = inventories.Select(i => i.Sum()).Max();

Console.WriteLine($"Part 1: {maxCalorieInventory}");

var sumOfTopThreeInventories = inventories
    .Select(i => i.Sum())
    .OrderByDescending(i => i)
    .Take(3)
    .Sum();

Console.WriteLine($"Part 2: {sumOfTopThreeInventories}");
