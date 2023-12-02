using AdventOfCode._2023.Day01;

namespace AdventOfCode.Tests._2023.Day01;

public class DailyPuzzleTests
{
    private readonly DailyPuzzle _dailyPuzzle = new();

    [Fact]
    public void SolvePartOne_Test()
    {
        // Arrange
        string[] input = [
            "1abc2",
            "pqr3stu8vwx",
            "a1b2c3d4e5f",
            "treb7uchet"
        ];

        // Act
        var output = _dailyPuzzle.SolvePartOne(input);

        // Assert
        Assert.Equal(142, output);
    }

    [Fact]
    public void SolvePartTwo_Test()
    {
        // Arrange
        string[] input = [
            "two1nine",
            "eightwothree",
            "abcone2threexyz",
            "xtwone3four",
            "4nineeightseven2",
            "zoneight234",
            "7pqrstsixteen",
        ];

        // Act
        var output = _dailyPuzzle.SolvePartTwo(input);

        // Assert
        Assert.Equal(281, output);
    }
}
