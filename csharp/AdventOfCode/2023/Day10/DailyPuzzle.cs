using AdventOfCode.Shared;
using System.Text.RegularExpressions;

namespace AdventOfCode._2023.Day10;

public class DailyPuzzle : IDailyPuzzle
{
    private static readonly Regex TileRegex = new Regex(@"[\||\-|L|J|7|F|S]");

    public string Year => AdventOfCodeEvents.Year2023;

    public string Day => AdventDays.Day10;

    public object SolvePartOne(string[] input)
    {
        // Parse all tiles
        // Find S
        // Find all connected tiles*
        //  * any tile that:
        //      - IsAdjacent && CanConnect or
        //      - tile.AdjacentTiles.Any(IsAdjacent && CanConnect)

        var tiles = input.SelectMany(ParseTiles);
        var loop = ParseLoop(tiles);

        return (int)Math.Ceiling((double)(loop.Count / 2));
    }

    public object SolvePartTwo(string[] input)
    {
        throw new NotImplementedException();
    }

    private static IEnumerable<Tile> ParseTiles(string line, int index) =>
        TileRegex.Matches(line)
                 .Select(match => new Tile(char.Parse(match.Value), match.Index, index));

    private static HashSet<Tile> ParseLoop(IEnumerable<Tile> tiles)
    {
        // Breadth-first search:
        // https://en.wikipedia.org/wiki/Breadth-first_search

        var loop = new HashSet<Tile>();
        var start = tiles.Single(tile => tile.Type == 'S');

        var queue = new Queue<Tile>();
        queue.Enqueue(start);
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            foreach (var pipe in FindAdjacentConnections(current, tiles))
            {
                if (loop.Contains(pipe))
                    continue;

                queue.Enqueue(pipe);
            }

            loop.Add(current);
        }

        return loop;
    }

    private static bool IsConnected(Tile first, Tile second, IEnumerable<Tile> tiles)
    {
        // A connected tile is any tile that is adjacent and can connect
        // or any tile that is connected to a tile that is adjacent and can connect
        if (first.IsAdjacent(second) && first.CanConnect(second))
            return true;
        else
            return FindAdjacentConnections(second, tiles).Any(third => IsConnected(first, third, tiles));
    }

    private static IEnumerable<Tile> FindAdjacentConnections(Tile start, IEnumerable<Tile> tiles) =>
        tiles.Where(tile => start.IsAdjacent(tile) && start.CanConnect(tile));

    record Tile(char Type, int Col, int Row)
    {
        public Direction[] Connects =>
            Type switch
            {
                '|' => [Direction.North, Direction.South],
                '-' => [Direction.East, Direction.West],
                'L' => [Direction.North, Direction.East],
                'J' => [Direction.North, Direction.West],
                '7' => [Direction.South, Direction.West],
                'F' => [Direction.South, Direction.East],
                'S' => [Direction.North, Direction.South, Direction.East, Direction.West],
                _ => throw new InvalidOperationException()
            };

        public bool CanConnect(Tile other) =>
            CanConnectRight(other) || CanConnectLeft(other) || CanConnectUp(other) || CanConnectDown(other);

        // other is on the rigth && this.Connects East && other.Connects West
        public bool CanConnectRight(Tile other) =>
            Row - other.Row == 0
            && Col - other.Col == -1
            && Connects.Contains(Direction.East)
            && other.Connects.Contains(Direction.West);

        public bool CanConnectLeft(Tile other) =>
           Row - other.Row == 0
           && Col - other.Col == 1
           && Connects.Contains(Direction.West)
           && other.Connects.Contains(Direction.East);

        public bool CanConnectUp(Tile other) =>
           Row - other.Row == 1
           && Col - other.Col == 0
           && Connects.Contains(Direction.North)
           && other.Connects.Contains(Direction.South);

        public bool CanConnectDown(Tile other) =>
           Row - other.Row == -1
           && Col - other.Col == 0
           && Connects.Contains(Direction.South)
           && other.Connects.Contains(Direction.North);

        public bool IsAdjacent(Tile other) =>
            Math.Abs(Row - other.Row) <= 1 && Math.Abs(Col - other.Col) <= 1;
    };

    public enum Direction
    {
        North, South, East, West
    }
}
