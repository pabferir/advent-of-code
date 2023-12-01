namespace AdventOfCode;

internal interface IDailyPuzzle
{
    public string YearDirectory { get; }
    public string DayDirectory { get; }

    object SolvePartOne(string[] input);

    object SolvePartTwo(string[] input);
}
