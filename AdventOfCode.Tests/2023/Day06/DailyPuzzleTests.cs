using AdventOfCode._2023.Day06;

namespace AdventOfCode.Tests._2023.Day06;

public class DailyPuzzleTests
{
    private readonly DailyPuzzle _dailyPuzzle = new();

    [Fact]
    public void SolvePartOne_Test()
    {
        // Arrange
        string[] input = [
            "Time:      7  15   30",
            "Distance:  9  40  200"
        ];

        // Act
        var output = _dailyPuzzle.SolvePartOne(input);

        // Assert
        Assert.Equal(288L, output);
    }

    [Fact]
    public void SolvePartTwo_Test()
    {
        // Arrange
        string[] input = [
            "Time:      7  15   30",
            "Distance:  9  40  200"
        ];

        // Act
        var output = _dailyPuzzle.SolvePartTwo(input);

        // Assert
        Assert.Equal(71503L, output);
    }
}
