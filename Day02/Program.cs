var inputPath = File.Exists(Path.Join(Directory.GetCurrentDirectory(), "input.txt"))
    ? Path.Join(Directory.GetCurrentDirectory(), "input.txt")
    : Path.Join(Directory.GetCurrentDirectory(), "..", "..", "..", "input.txt");

var rounds = File.ReadLines(inputPath)
    .Select(r => r
        .Split(' ')
        .Select(c => c.ToCharArray().First())
        .ToArray()
    )
    .Select(r => new Round { OpponentChoice = r[0], PlayerCode = r[1] })
    .ToArray();

int GetChoiceScore(char choice) => choice switch
{
    'A' => 1,
    'B' => 2,
    'C' => 3,
    'X' => 1,
    'Y' => 2,
    'Z' => 3,
    _ => throw new Exception("Invalid choice")
};

int GetOutcomeScore(char playerChoice, char opponentChoice) => playerChoice switch
{
    'X' => opponentChoice switch
    {
        'A' => 3,
        'B' => 0,
        'C' => 6,
        _ => throw new Exception($"Invalid opponent choice: {opponentChoice}")
    },
    'Y' => opponentChoice switch
    {
        'A' => 6,
        'B' => 3,
        'C' => 0,
        _ => throw new Exception($"Invalid opponent choice: {opponentChoice}")
    },
    'Z' => opponentChoice switch
    {
        'A' => 0,
        'B' => 6,
        'C' => 3,
        _ => throw new Exception($"Invalid opponent choice: {opponentChoice}")
    },
    _ => throw new Exception($"Invalid player choice: {playerChoice}")
};

var scores = rounds.Select(r => GetOutcomeScore(r.PlayerCode, r.OpponentChoice) + GetChoiceScore(r.PlayerCode));
var total = scores.Sum();

Console.WriteLine($"Part 1: {total}");

char GetPlayerChoice(char opponentChoice, char outcomeCode) => opponentChoice switch
{
    'A' => outcomeCode switch
    {
        'X' => 'Z',
        'Y' => 'X',
        'Z' => 'Y',
        _ => throw new Exception($"Invalid outcome code: {outcomeCode}")
    },
    'B' => outcomeCode switch
    {
        'X' => 'X',
        'Y' => 'Y',
        'Z' => 'Z',
        _ => throw new Exception($"Invalid outcome code: {outcomeCode}")
    },
    'C' => outcomeCode switch
    {
        'X' => 'Y',
        'Y' => 'Z',
        'Z' => 'X',
        _ => throw new Exception($"Invalid outcome code: {outcomeCode}")
    },
    _ => throw new Exception($"Invalid opponent choice: {opponentChoice}")
};

var newScores = rounds.Select(r =>
{
    var playerChoice = GetPlayerChoice(r.OpponentChoice, r.PlayerCode);
    return GetOutcomeScore(playerChoice, r.OpponentChoice) + GetChoiceScore(playerChoice);
});

var newTotal = newScores.Sum();

Console.WriteLine($"Part 2: {newTotal}");

internal struct Round
{
    public char PlayerCode { get; init; }
    public char OpponentChoice { get; init; }
}