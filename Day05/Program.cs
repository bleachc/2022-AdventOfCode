using System.Text.RegularExpressions;

var inputPath = File.Exists(Path.Join(Directory.GetCurrentDirectory(), "input.txt"))
    ? Path.Join(Directory.GetCurrentDirectory(), "input.txt")
    : Path.Join(Directory.GetCurrentDirectory(), "..", "..", "..", "input.txt");

var input = File.ReadAllText(inputPath).Split("\n\n");
var initialState = input[0].Split("\n").Select(i => i.Chunk(4).Select(x => x[1]).ToArray()).SkipLast(1).ToArray();
var initialInstructions = input[1].Split("\n");

var regex = new Regex("move (\\d+) from (\\d+) to (\\d+)");
var instructions = initialInstructions
    .Select(i => regex.Match(i).Groups.Values.Skip(1).Select(x => int.Parse(x.ValueSpan)).ToArray()).ToArray();

Stack<char>[] GetInitialStacks()
{
    var stacks = new Stack<char>[initialState.First().Length].Select(s => new Stack<char>()).ToArray();

    for (var i = 0; i < stacks.Length; i++)
    {
        for (var j = initialState.Length - 1; j > -1; j--)
        {
            if (initialState[j][i] != ' ') stacks[i].Push(initialState[j][i]);
        }
    }

    return stacks;
}

void PartOne()
{
    var stacks = GetInitialStacks();

    foreach (var instruction in instructions)
    {
        for (var i = instruction[0]; i > 0; i--)
        {
            stacks[instruction[2] - 1].Push(stacks[instruction[1] - 1].Pop());
        }
    }

    Console.WriteLine($"Part 1: {string.Join("", stacks.Select(s => s.Peek()))}");
}

void PartTwo()
{
    var stacks = GetInitialStacks();

    foreach (var instruction in instructions)
    {
        var queue = new Queue<char>();

        for (var i = instruction[0]; i > 0; i--)
        {
            if (stacks[instruction[1] - 1].Count != 0) queue.Enqueue(stacks[instruction[1] - 1].Pop());
        }

        foreach (var box in queue.Reverse())
        {
            stacks[instruction[2] - 1].Push(box);
        }
    }

    Console.WriteLine($"Part 2: {string.Join("", stacks.Select(s => s.Peek()))}");
}

PartOne();
PartTwo();