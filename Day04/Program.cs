using System.Text.RegularExpressions;

var inputPath = File.Exists(Path.Join(Directory.GetCurrentDirectory(), "input.txt"))
    ? Path.Join(Directory.GetCurrentDirectory(), "input.txt")
    : Path.Join(Directory.GetCurrentDirectory(), "..", "..", "..", "input.txt");

var assignmentPairs = File.ReadLines(inputPath).ToArray();

var numberRegex = new Regex("(\\d+)-(\\d+),(\\d+)-(\\d+)");

var assignments = assignmentPairs
    .Select(ap => numberRegex.Match(ap).Groups.Values
        .Skip(1)
        .Select(g => int.Parse(g.ValueSpan))
        .ToArray()
    )
    .ToArray();

var containedAssignments = assignments.Where(a => (a[0] >= a[2] && a[1] <= a[3]) || (a[2] >= a[0] && a[3] <= a[1]));

Console.WriteLine($"Part 1: {containedAssignments.Count()}");

var notOverlapped = assignments.Where(a => a[1] < a[2] || a[0] > a[3]);
var overlapped = assignments.Length - notOverlapped.Count();

Console.WriteLine($"Part 2: {overlapped}");