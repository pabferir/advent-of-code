﻿using AdventOfCode._2023.Day02;

namespace AdventOfCode.Tests._2023.Day02;

public class DailyPuzzleTests
{
    private readonly DailyPuzzle _dailyPuzzle = new();

    [Fact]
    public void SolvePartOne_Test()
    {
        // Arrange
        string[] input = [
            "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
            "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
            "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
            "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
            "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green",
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
            "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
            "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
            "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
            "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
            "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green",
        ];

        // Act
        var output = _dailyPuzzle.SolvePartTwo(input);

        // Assert
        Assert.Equal(2286, output);
    }
}
