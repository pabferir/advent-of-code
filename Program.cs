using AdventOfCode._2023.Day01;
using System.Reflection;

var dailyPuzzle = new DailyPuzzle();

var inputPath = Path.Combine(
    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!,
    $"{dailyPuzzle.YearDirectory}\\{dailyPuzzle.DayDirectory}\\input.txt");

var input = File.ReadAllLines(inputPath);

Console.WriteLine(dailyPuzzle.SolvePartOne(input));
Console.WriteLine(dailyPuzzle.SolvePartTwo(input));
