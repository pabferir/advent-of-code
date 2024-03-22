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
        var nodes = ParseNodes(input.Skip(2));

        return CalculateSteps(
            instructions,
            nodes.Single(node => node.Id == "AAA"),
            nodes.Where(node => node.Id == "ZZZ"),
            nodes.ToDictionary(node => node.Id));
    }

    public object SolvePartTwo(string[] input)
    {
        var instructions = input[0];
        var nodes = ParseNodes(input.Skip(2));

        var startNodes = ParseNodes(input.Skip(2)).Where(node => node.Id.Last() == 'A');

        return startNodes.Select(node =>
                       CalculateSteps(
                           instructions,
                           node,
                           nodes.Where(node => node.Id.EndsWith('Z')),
                           nodes.ToDictionary(node => node.Id)))
                  .Aggregate(1L, Lcm);
    }

    private static long CalculateSteps(string instructions, NetworkNode start, IEnumerable<NetworkNode> ends, Dictionary<string, NetworkNode> network)
    {
        var steps = 0;
        var currentNode = start;

        while (!ends.Contains(currentNode))
        {
            var nextStep = instructions[steps % instructions.Length];

            if (nextStep == 'L')
                currentNode = network[currentNode.NextLeft];
            else
                currentNode = network[currentNode.NextRight];

            steps++;
        }

        return steps;
    }

    // https://en.wikipedia.org/wiki/Least_common_multiple
    private static long Lcm(long a, long b) =>
        a * b / Gcd(a, b);

    // https://en.wikipedia.org/wiki/Greatest_common_divisor
    private static long Gcd(long a, long b) =>
        b == 0 ? a : Gcd(b, a % b);

    private static IEnumerable<NetworkNode> ParseNodes(IEnumerable<string> input) =>
        input.Select(line => NodeRegex.Matches(line))
             .Select(matches => new NetworkNode(matches[0].Value, matches[1].Value, matches[2].Value));

    record NetworkNode(string Id, string NextLeft, string NextRight);
}
