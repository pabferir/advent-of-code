using AdventOfCode.Shared;
using System.Text.RegularExpressions;

namespace AdventOfCode._2023.Day08;

public class DailyPuzzle : IDailyPuzzle
{
    private static readonly Regex NodeRegex = new Regex(@"([A-Z\d]+)");

    public string Year => AdventOfCodeEvents.Year2023;

    public string Day => AdventDays.Day08;

    public object SolvePartOne(string[] input)
    {
        var instructions = input[0];
        var nodes = ParseNodes(input.Skip(2).ToArray())
                        .ToDictionary(node => node.Id);

        var currentNode = nodes["AAA"];
        var steps = 0;

        while (currentNode.Id != "ZZZ")
        {
            var nextStep = instructions[steps % instructions.Length];

            if (nextStep == 'L')
                currentNode = nodes[currentNode.NextLeft];
            else
                currentNode = nodes[currentNode.NextRight];

            steps++;
        }

        return steps;
    }

    public object SolvePartTwo(string[] input)
    {
        throw new NotImplementedException();
    }

    private static IEnumerable<NetworkNode> ParseNodes(string[] input) =>
        input.Select(line => NodeRegex.Matches(line))
             .Select(matches => new NetworkNode(matches[0].Value, matches[1].Value, matches[2].Value));

    record NetworkNode(string Id, string NextLeft, string NextRight);
}
