using AdventOfCode._2023.Day04;
using System.Reflection;

var dailyPuzzle = new DailyPuzzle();

var inputPath = Path.Combine(
    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!,
    $"{dailyPuzzle.Year}\\{dailyPuzzle.Day}\\input.txt");

var input = File.ReadAllLines(inputPath);

Console.WriteLine(dailyPuzzle.SolvePartOne(input));
Console.WriteLine(dailyPuzzle.SolvePartTwo(input));
