using AdventOfCode.Shared;
using System.Text.RegularExpressions;

namespace AdventOfCode._2023.Day03;

public class DailyPuzzle : IDailyPuzzle
{
    private static readonly Regex NumberRegex = new(@"(\d+)");
    private static readonly Regex SymbolRegex = new(@"[^\.\w]");
    private static readonly Regex GearRegex = new(@"\*");

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
        var symbols = input.SelectMany((line, index) => ParseSchematicSymbols(line, index + 1, SymbolRegex));

        return input.SelectMany((line, index) => ParseSchematicNumbers(line, index + 1))
                    .Where(number => symbols.Any(symbol => symbol.IsAdjacent(number)))
                    .Select(partNumber => partNumber.AsInt)
                    .Sum();
    }

    /// <summary>
    /// <para>
    /// --- Part Two ---</para>
    /// <para> The engineer finds the missing part and installs it in the engine! As the engine springs to life, you jump in the closest gondola, finally ready to ascend to the water source.</para>
    /// <para> You don't seem to be going very fast, though. Maybe something is still wrong? Fortunately, the gondola has a phone labeled "help", so you pick it up and the engineer answers.</para>
    /// <para> Before you can explain the situation, she suggests that you look out the window.There stands the engineer, holding a phone in one hand and waving with the other.You're going so slowly that you haven't even left the station.You exit the gondola.</para>
    /// <para> The missing part wasn't the only issue - one of the gears in the engine is wrong. A gear is any * symbol that is adjacent to exactly two part numbers. Its gear ratio is the result of multiplying those two numbers together.</para>
    /// <para> This time, you need to find the gear ratio of every gear and add them all up so that the engineer can figure out which gear needs to be replaced.</para>
    /// <para> Consider the same engine schematic again:</para>
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
    /// <para>In this schematic, there are two gears. The first is in the top left; it has part numbers 467 and 35, so its gear ratio is 16345. The second gear is in the lower right; its gear ratio is 451490. (The* adjacent to 617 is not a gear because it is only adjacent to one part number.) Adding up all of the gear ratios produces 467835.</para>
    /// <para>What is the sum of all of the gear ratios in your engine schematic?</para>
    /// </summary>
    /// <param name="input"></param>
    public object SolvePartTwo(string[] input)
    {
        var gears = input.SelectMany((line, index) => ParseSchematicSymbols(line, index + 1, GearRegex));
        var numbers = input.SelectMany((line, index) => ParseSchematicNumbers(line, index + 1));

        return gears.Select(gear => numbers.Where(number => gear.IsAdjacent(number)))
                    .Where(partNumbers => partNumbers.Count() == 2)
                    .Select(partNumbers => partNumbers.First().AsInt * partNumbers.Last().AsInt)
                    .Sum();
    }

    private static IEnumerable<SchematicSymbol> ParseSchematicSymbols(string line, int row, Regex regex) =>
         regex.Matches(line)
              .Select(match => new SchematicSymbol(match.Value, row, match.Index));

    private static IEnumerable<SchematicNumber> ParseSchematicNumbers(string line, int row) =>
        NumberRegex.Matches(line)
                   .Select(match => new SchematicNumber(match.Value, row, match.Index));

    record SchematicSymbol(string Value, int Row, int Col)
    {
        // should be in the same row+-1
        // should be in the columns from number.Col -1 to number.Col + number.Value.Length
        public bool IsAdjacent(SchematicNumber number) =>
            Math.Abs(Row - number.Row) <= 1
                && Col <= number.Col + number.Value.Length
                && number.Col <= Col + Value.Length;
    }

    record SchematicNumber(string Value, int Row, int Col)
    {
        public int AsInt => int.Parse(Value);
    };
}
