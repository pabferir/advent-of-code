using AdventOfCode._2023.Day03;

namespace AdventOfCode.Tests._2023.Day03;

public class DailyPuzzleTests
{
    private readonly DailyPuzzle _dailyPuzzle = new();

    [Fact]
    public void SolvePartOne_Test()
    {
        // Arrange
        string[] input = [
            "467..114..",
            "...*......",
            "..35..633.",
            "......#...",
            "617*......",
            ".....+.58.",
            "..592.....",
            "......755.",
            "...$.*....",
            ".664.598.."
        ];

        // Act
        var output = _dailyPuzzle.SolvePartOne(input);

        // Assert
        Assert.Equal(4361, output);
    }

    [Fact]
    public void SolvePartTwo_Test()
    {
        // Arrange
        string[] input = [
            "467..114..",
            "...*......",
            "..35..633.",
            "......#...",
            "617*......",
            ".....+.58.",
            "..592.....",
            "......755.",
            "...$.*....",
            ".664.598.."
        ];

        // Act
        var output = _dailyPuzzle.SolvePartTwo(input);

        // Assert
        Assert.Equal(467835, output);
    }
}
