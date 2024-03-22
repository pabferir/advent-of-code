using AdventOfCode.Shared;
using System.Text.RegularExpressions;

namespace AdventOfCode._2023.Day03;

public class FailedAttempt : IDailyPuzzle
{
    private static readonly Regex NumberRegex = new(@"(\d+)");
    private static readonly Regex SymbolRegex = new(@"[^\.\w]");

    public string Year => AdventOfCodeEvents.Year2023;
    public string Day => AdventDays.Day03;

    /// <summary>
    /// <para> --- Day 3: Gear Ratios ---</para>
    /// <para> You and the Elf eventually reach a gondola lift station; he says the gondola lift will take you up to the water source, but this is as far as he can bring you. You go inside.</para>
    /// <para> It doesn't take long to find the gondolas, but there seems to be a problem: they're not moving.</para>
    /// <para>"Aaah!"</para>
    /// <para> You turn around to see a slightly-greasy Elf with a wrench and a look of surprise. "Sorry, I wasn't expecting anyone! The gondola lift isn't working right now; it'll still be a while before I can fix it." You offer to help.</para>
    /// <para> The engineer explains that an engine part seems to be missing from the engine, but nobody can figure out which one. If you can add up all the part numbers in the engine schematic, it should be easy to work out which part is missing.</para>
    /// <para> The engine schematic (your puzzle input) consists of a visual representation of the engine. There are lots of numbers and symbols you don't really understand, but apparently any number adjacent to a symbol, even diagonally, is a "part number" and should be included in your sum. (Periods (.) do not count as a symbol.)</para>
    /// <para>Here is an example engine schematic:</para>
    /// <code>
    /// 467..114..
    /// ...*......
    /// ..35..633.
    /// ......#...
    /// 617*......
    /// .....+.58.
    /// ..592.....
    /// ......755.
    /// ...$.*....
    /// .664.598..
    /// </code>
    /// <para>In this schematic, two numbers are not part numbers because they are not adjacent to a symbol: 114 (top right) and 58 (middle right). Every other number is adjacent to a symbol and so is a part number; their sum is 4361.</para>
    /// <para>Of course, the actual engine schematic is much larger.What is the sum of all of the part numbers in the engine schematic?</para>
    /// </summary>
    /// <param name="input">The engine schematic, a visual representation of the engine</param>
    /// <returns>The sum of all part numbers in the engine schematic, where a part number is any number adjacent to a symbol, even diagonally</returns>
    public object SolvePartOne(string[] input)
    {
        return input.SelectMany((line, index) =>
                            ParseSchematicNumbers(
                                line,
                                input.ElementAtOrDefault(index - 1) ?? string.Empty,
                                input.ElementAtOrDefault(index + 1) ?? string.Empty,
                                index + 1))
                    .Where(number => number.IsPartNumber)
                    .Select(partNumber => partNumber.Value)
                    .Sum();
    }

    public object SolvePartTwo(string[] input)
    {
        throw new NotImplementedException();
    }

    private static IEnumerable<SchematicNumber> ParseSchematicNumbers(string line, string previous, string next, int row) =>
        NumberRegex.Matches(line)
                   .Select(match =>
                        new SchematicNumber(
                            ParseSchematicNumberSection(previous, match.Index, match.Length) + ParseSchematicNumberSection(line, match.Index, match.Length) + ParseSchematicNumberSection(next, match.Index, match.Length),
                            row,
                            match.Index
                        ));

    private static string ParseSchematicNumberSection(string line, int start, int length) =>
        string.IsNullOrEmpty(line)
        ? line
        : line.Substring(
            Math.Max(0, start - 1),
            Math.Min(line.Length - start + 1, length + 2));

    record SchematicNumber(
        string Content,
        int Row,
        int Col)
    {
        public bool IsPartNumber => SymbolRegex.IsMatch(Content);
        public int Value => int.Parse(NumberRegex.Match(Content).Value);
    };
}
