using AdventOfCode.Shared;

namespace AdventOfCode._2023.Day12;

public class DailyPuzzle : IDailyPuzzle
{
    private const char Damaged = '#';
    private const char Operational = '.';
    private const char Unknown = '.';

    public string Year => AdventOfCodeEvents.Year2023;

    public string Day => AdventDays.Day12;

    public object SolvePartOne(string[] input)
    {
        // ParseConditionRecords
        var conditionRecords = ParseConditionRecords(input).ToArray();
        // CountAllPossibleArrangements
        // Sum

        throw new NotImplementedException();
    }

    public object SolvePartTwo(string[] input)
    {
        throw new NotImplementedException();
    }

    private static int CountAllPossibleArrangements(ConditionRecord conditionRecord)
    {
        /*
            "???.### 1,1,3",
            ".??..??...?##. 1,1,3",
            "?#?#?#?#?#?#?#? 1,3,1,6",
            "????.#...#... 4,1,1",
            "????.######..#####. 1,6,5",
            "?###???????? 3,2,1"
         */

        // the second is a representation of the first
        // missing '#' count = criteria.Sum() - '#'.match.Count

        // split on '.'
        //      > this gives all groups of ? and #
        //      > can compare the size of both arrays
        // if lenghts ==
        //      possi == sum(?) that are in groups without

        return 1;
    }

    private static IEnumerable<ConditionRecord> ParseConditionRecords(string[] input) =>
        input.Select(line => line.Split(' ', ','))
             .Select(parts => new ConditionRecord(parts[0], parts[1..].Select(group => int.Parse(group)).ToArray()));

    record ConditionRecord(string SpringConditions, int[] DamageCriteria);

    private enum SpringStatus
    {
        Unknown, Damaged, Operational
    }
}
