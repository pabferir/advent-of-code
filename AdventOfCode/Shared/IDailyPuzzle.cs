namespace AdventOfCode.Shared;

internal interface IDailyPuzzle
{
    public string Year { get; }
    public string Day { get; }

    object SolvePartOne(string[] input);

    object SolvePartTwo(string[] input);
}
