using AdventOfCode.Shared;

namespace AdventOfCode._2023.Day11;

public class DailyPuzzle : IDailyPuzzle
{
    private const char GalaxyChar = '#';

    public string Year => AdventOfCodeEvents.Year2023;

    public string Day => AdventDays.Day11;

    public object SolvePartOne(string[] input)
    {
        var universe = ParseUniverse(input);
        var galaxies = ParseGalaxies(universe).ToArray();

        return FindShortestPathsBetween(galaxies, universe, 2).Sum();
    }

    public object SolvePartTwo(string[] input)
    {
        var universe = ParseUniverse(input);
        var galaxies = ParseGalaxies(universe).ToArray();

        return FindShortestPathsBetween(galaxies, universe, 1000000).Sum();
    }

    private static IEnumerable<long> FindShortestPathsBetween(Galaxy[] galaxies, char[,] universe, int expansion)
    {
        var emptySpaceRows = ParseEmptySpaceRows(universe).ToArray();
        var emptySpaceCols = ParseEmptySpaceCols(universe).ToArray();

        for (int i = 0; i < galaxies.Length; i++)
        {
            var current = galaxies[i];
            for (int j = i + 1; j < galaxies.Length; j++)
            {
                var other = galaxies[j];
                yield return FindShortestPathBetween(current, other, expansion, emptySpaceRows, emptySpaceCols);
            }
        }
    }

    private static char[,] ParseUniverse(string[] input)
    {
        int rows = input.Length;
        int cols = input[0].Length;

        char[,] universe = new char[rows, cols];

        for (int row = 0; row < rows; row++)
        {
            var line = input[row];
            for (int col = 0; col < cols; col++)
            {
                universe[row, col] = line[col];
            }
        }

        return universe;
    }

    private static IEnumerable<int> ParseEmptySpaceRows(char[,] universe)
    {
        var rows = universe.GetLength(0);
        var cols = universe.GetLength(1);

        for (int row = 0; row < rows; row++)
        {
            bool isEmpty = true;
            for (int col = 0; col < cols; col++)
            {
                if (universe[row, col] == GalaxyChar)
                {
                    isEmpty = false;
                    break;
                }
            }

            if (isEmpty)
            {
                yield return row;
            }
        }
    }

    private static IEnumerable<int> ParseEmptySpaceCols(char[,] universe)
    {
        var rows = universe.GetLength(0);
        var cols = universe.GetLength(1);

        for (int col = 0; col < cols; col++)
        {
            bool isEmpty = true;
            for (int row = 0; row < rows; row++)
            {
                if (universe[row, col] == GalaxyChar)
                {
                    isEmpty = false;
                    break;
                }
            }

            if (isEmpty)
            {
                yield return col;
            }
        }
    }

    private static IEnumerable<Galaxy> ParseGalaxies(char[,] universe)
    {
        var rows = universe.GetLength(0);
        var cols = universe.GetLength(1);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (universe[row, col] == GalaxyChar)
                {
                    yield return new Galaxy(row, col);
                }
            }
        }
    }

    // Taxicab geometry
    // https://en.wikipedia.org/wiki/Taxicab_geometry
    private static long FindShortestPathBetween(Galaxy first, Galaxy second, int expansion, int[] emptySpaceRows, int[] emptySpaceCols) =>
        Math.Abs(first.Row - second.Row) + (CountEmptyRowsInBetween(first, second, emptySpaceRows) * (expansion - 1))
            + Math.Abs(first.Col - second.Col) + (CountEmptyColsInBetween(first, second, emptySpaceCols) * (expansion - 1));

    private static int CountEmptyRowsInBetween(Galaxy first, Galaxy second, int[] emptySpaceRows) =>
        emptySpaceRows.Count(row => Math.Min(first.Row, second.Row) < row && row < Math.Max(first.Row, second.Row));

    private static int CountEmptyColsInBetween(Galaxy first, Galaxy other, int[] emptySpaceCols) =>
        emptySpaceCols.Count(col => Math.Min(first.Col, other.Col) < col && col < Math.Max(first.Col, other.Col));

    record Galaxy(int Row, int Col);
}
