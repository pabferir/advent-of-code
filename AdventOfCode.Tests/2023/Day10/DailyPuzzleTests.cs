using AdventOfCode._2023.Day10;

namespace AdventOfCode.Tests._2023.Day10;

public class DailyPuzzleTests
{
    private readonly DailyPuzzle _dailyPuzzle = new();

    [Fact]
    public void SolvePartOne_Test()
    {
        // Arrange
        string[] input = [
            "..F7.",
            ".FJ|.",
            "SJ.L7",
            "|F--J",
            "LJ..."
        ];

        // Act
        var output = _dailyPuzzle.SolvePartOne(input);

        // Assert
        Assert.Equal(8, output);
    }

    [Fact]
    public void SolvePartTwo_Test()
    {
        // Arrange
        string[] input = [
            "0 3 6 9 12 15",
            "1 3 6 10 15 21",
            "10 13 16 21 30 45"
        ];

        // Act
        var output = _dailyPuzzle.SolvePartTwo(input);

        // Assert
        Assert.Equal(2L, output);
    }
}
