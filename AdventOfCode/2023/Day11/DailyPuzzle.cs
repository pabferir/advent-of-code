using AdventOfCode.Shared;

namespace AdventOfCode._2023.Day11;

public class DailyPuzzle : IDailyPuzzle
{
    private static readonly char GalaxyChar = '#';

    public string Year => AdventOfCodeEvents.Year2023;

    public string Day => AdventDays.Day11;

    public object SolvePartOne(string[] input)
    {
        // 0. Parse Universe
        var universe = ParseUniverse(input);

        // 1. Expand the universe
        var expandedUniverse = Expand(universe);

        // 2. ParseGalaxies
        var galaxies = ParseGalaxies(expandedUniverse).ToArray();

        // 3. For each galaxy find shortes distance from it to all others
        //    Find the length of the shortest path between every pair of galaxies
        //          3.1 Skip pair if already processed
        var visited = new HashSet<(Galaxy, Galaxy)>();
        var sum = 0;
        foreach (var current in galaxies)
        {
            for (int i = 0; i < galaxies.Length; i++)
            {
                var other = galaxies[i];
                if (current != other && !visited.Contains((current, other)))
                {
                    sum += current.FindShortestPathTo(other);
                    visited.Add((current, other));
                    visited.Add((other, current));
                }
            }
        }

        return sum;
    }

    public object SolvePartTwo(string[] input)
    {
        throw new NotImplementedException();
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

        //char[,] second = new char[rows, cols];
        //input.SelectMany((line, row) => line.Select((character, col) => (character, row, col)))
        //     .ToList()
        //     .ForEach(element => second[element.row, element.col] = element.character);

        //if (universe != second)
        //    throw new Exception();

        return universe;
    }

    private static char[,] Expand(char[,] universe)
    {
        // duplicate all rows and columns where there are no galaxies
        var rows = universe.GetLength(0);
        var cols = universe.GetLength(1);

        // find empty rows
        var rowsToExpand = new List<int>();
        for (int row = 0, rowOffset = 0; row < rows; row++)
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

            if (isEmpty) rowsToExpand.Add(row + rowOffset++);
        }

        // find empty cols
        var colsToExpand = new List<int>();
        for (int col = 0, colOffset = 0; col < cols; col++)
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

            if (isEmpty) colsToExpand.Add(col + colOffset++);
        }

        foreach (int row in rowsToExpand)
        {
            universe = ExpandRow(universe, row);
        }
        foreach (int col in colsToExpand)
        {
            universe = ExpandColumn(universe, col);
        }

        return universe;
    }

    private static char[,] ExpandRow(char[,] universe, int rowToExpand)
    {
        char[,] expanded = new char[universe.GetLength(0) + 1, universe.GetLength(1)];

        // copy all rows before
        int row = 0;
        while (row <= rowToExpand)
        {
            for (int col = 0; col < expanded.GetLength(1); col++)
            {
                expanded[row, col] = universe[row, col];
            }
            row++;
        }

        // duplicate row
        for (int col = 0; col < expanded.GetLength(1); col++)
        {
            expanded[row, col] = universe[row - 1, col];
        }

        // copy all rows after
        while (row < universe.GetLength(0))
        {
            for (int col = 0; col < expanded.GetLength(1); col++)
            {
                expanded[row + 1, col] = universe[row, col];
            }
            row++;
        }

        return expanded;
    }

    private static char[,] ExpandColumn(char[,] universe, int colToExpand)
    {
        char[,] expanded = new char[universe.GetLength(0), universe.GetLength(1) + 1];

        // copy all columns before
        int col = 0;
        while (col <= colToExpand)
        {
            for (int row = 0; row < expanded.GetLength(0); row++)
            {
                expanded[row, col] = universe[row, col];
            }
            col++;
        }

        // duplicate column
        for (int row = 0; row < expanded.GetLength(0); row++)
        {
            expanded[row, col] = universe[row, col - 1];
        }

        // copy all columns after
        while (col < universe.GetLength(1))
        {
            for (int row = 0; row < expanded.GetLength(0); row++)
            {
                expanded[row, col + 1] = universe[row, col];
            }
            col++;
        }

        return expanded;
    }

    private static IEnumerable<Galaxy> ParseGalaxies(char[,] universe)
    {
        for (int row = 0; row < universe.GetLength(0); row++)
        {
            for (int col = 0; col < universe.GetLength(1); col++)
            {
                if (universe[row, col] == GalaxyChar)
                {
                    yield return new Galaxy(row, col);
                }
            }
        }
    }

    // Dijkstra's algorithm ??
    // https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm
    private static int FindShortestPathBetween(Galaxy first, Galaxy second) =>
        Math.Abs(first.Row - second.Row) + Math.Abs(first.Col - second.Col) + 2;

    record Galaxy(int Row, int Col)
    {
        // If they are in adjacent rows/columns do not sum in that axis
        public int FindShortestPathTo(Galaxy other) =>
            Math.Abs(Row - other.Row) + Math.Abs(Col - other.Col);
        //{
        //    var rowDiff = Math.Abs(Row - other.Row);
        //    var colDiff = Math.Abs(Col - other.Col);

        //    var isAdjacentRow = rowDiff == 1;
        //    var isAdjacentCol = colDiff == 1;

        //    return (isAdjacentRow, isAdjacentCol) switch
        //    {
        //        (false, true) => rowDiff,
        //        (true, false) => colDiff,
        //        _ => rowDiff + colDiff
        //    };
        //}
    };
}
