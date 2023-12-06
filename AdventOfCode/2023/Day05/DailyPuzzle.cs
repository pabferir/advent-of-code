using AdventOfCode.Shared;
using System.Text.RegularExpressions;

namespace AdventOfCode._2023.Day05;

public class DailyPuzzle : IDailyPuzzle
{
    private static readonly Regex NumberRegex = new(@"(\d+)");

    public string Year => AdventOfCodeEvents.Year2023;

    public string Day => AdventDays.Day05;

    /// <summary>
    /// <para> --- Day 5: If You Give A Seed A Fertilizer ---</para>
    /// <para> You take the boat and find the gardener right where you were told he would be: managing a giant "garden" that looks more to you like a farm.</para>
    /// <para> "A water source? Island Island is the water source!" You point out that Snow Island isn't receiving any water.</para>
    /// <para> "Oh, we had to stop the water because we ran out of sand to filter it with! Can't make snow with dirty water. Don't worry, I'm sure we'll get more sand soon; we only turned off the water a few days... weeks... oh no." His face sinks into a look of horrified realization.</para>
    /// <para> "I've been so busy making sure everyone here has food that I completely forgot to check why we stopped getting more sand! There's a ferry leaving soon that is headed over in that direction - it's much faster than your boat. Could you please go check it out?"</para>
    /// <para> You barely have time to agree to this request when he brings up another. "While you wait for the ferry, maybe you can help us with our food production problem. The latest Island Island Almanac just arrived and we're having trouble making sense of it."</para>
    /// <para> The almanac (your puzzle input) lists all of the seeds that need to be planted.It also lists what type of soil to use with each kind of seed, what type of fertilizer to use with each kind of soil, what type of water to use with each kind of fertilizer, and so on.Every type of seed, soil, fertilizer and so on is identified with a number, but numbers are reused by each category - that is, soil 123 and fertilizer 123 aren't necessarily related to each other.</para>
    /// <para> For example:</para>
    /// <code>
    /// seeds: 79 14 55 13
    ///
    /// seed-to-soil map:
    /// 50 98 2
    /// 52 50 48
    ///
    /// soil-to-fertilizer map:
    /// 0 15 37
    /// 37 52 2
    /// 39 0 15
    ///
    /// fertilizer-to-water map:
    /// 49 53 8
    /// 0 11 42
    /// 42 0 7
    /// 57 7 4
    ///
    /// water-to-light map:
    /// 88 18 7
    /// 18 25 70
    ///
    /// light-to-temperature map:
    /// 45 77 23
    /// 81 45 19
    /// 68 64 13
    ///
    /// temperature-to-humidity map:
    /// 0 69 1
    /// 1 0 69
    ///
    /// humidity-to-location map:
    /// 60 56 37
    /// 56 93 4
    /// </code>
    /// <para> The almanac starts by listing which seeds need to be planted: seeds 79, 14, 55, and 13.</para>
    /// <para> The rest of the almanac contains a list of maps which describe how to convert numbers from a source category into numbers in a destination category. That is, the section that starts with seed-to-soil map: describes how to convert a seed number (the source) to a soil number (the destination). This lets the gardener and his team know which soil to use with which seeds, which water to use with which fertilizer, and so on.</para>
    /// <para> Rather than list every source number and its corresponding destination number one by one, the maps describe entire ranges of numbers that can be converted. Each line within a map contains three numbers: the destination range start, the source range start, and the range length.</para>
    /// <para> Consider again the example seed-to-soil map:</para>
    /// <code>
    /// 50 98 2
    /// 52 50 48
    /// </code>
    /// <para> The first line has a destination range start of 50, a source range start of 98, and a range length of 2. This line means that the source range starts at 98 and contains two values: 98 and 99. The destination range is the same length, but it starts at 50, so its two values are 50 and 51. With this information, you know that seed number 98 corresponds to soil number 50 and that seed number 99 corresponds to soil number 51.</para>
    /// <para> The second line means that the source range starts at 50 and contains 48 values: 50, 51, ..., 96, 97. This corresponds to a destination range starting at 52 and also containing 48 values: 52, 53, ..., 98, 99. So, seed number 53 corresponds to soil number 55.</para>
    /// <para> Any source numbers that aren't mapped correspond to the same destination number. So, seed number 10 corresponds to soil number 10.</para>
    /// <para> So, the entire list of seed numbers and their corresponding soil numbers looks like this:</para>
    /// <code>
    /// seed soil
    /// 0     0
    /// 1     1
    /// ...   ...
    /// 48    48
    /// 49    49
    /// 50    52
    /// 51    53
    /// ...   ...
    /// 96    98
    /// 97    99
    /// 98    50
    /// 99    51
    /// </code>
    /// <para> With this map, you can look up the soil number required for each initial seed number:</para>
    /// <para> - Seed number 79 corresponds to soil number 81.</para>
    /// <para> - Seed number 14 corresponds to soil number 14.</para>
    /// <para> - Seed number 55 corresponds to soil number 57.</para>
    /// <para> - Seed number 13 corresponds to soil number 13.</para>
    /// <para> The gardener and his team want to get started as soon as possible, so they'd like to know the closest location that needs a seed. Using these maps, find the lowest location number that corresponds to any of the initial seeds. To do this, you'll need to convert each seed number through other categories until you can find its corresponding location number. In this example, the corresponding types are:</para>
    /// <para> - Seed 79, soil 81, fertilizer 81, water 81, light 74, temperature 78, humidity 78, location 82.</para>
    /// <para> - Seed 14, soil 14, fertilizer 53, water 49, light 42, temperature 42, humidity 43, location 43.</para>
    /// <para> - Seed 55, soil 57, fertilizer 57, water 53, light 46, temperature 82, humidity 82, location 86.</para>
    /// <para> - Seed 13, soil 13, fertilizer 52, water 41, light 34, temperature 34, humidity 35, location 35.</para>
    /// <para> So, the lowest location number in this example is 35. What is the lowest location number that corresponds to any of the initial seed numbers?</para>
    /// </summary>
    /// <param name="input"></param>
    public object SolvePartOne(string[] input)
    {
        var seeds = ParseNumbers(input[0]).ToArray();
        var maps = ParseMaps(input[1..]).ToArray();

        // Map: Destination <= Source, RangeLength
        // Maps are actually collections of ranges

        // can jump from map to map
        // if x is in any map range :: source <= x && x < source + length
        //      then => number - source + destination
        //      else => number

        for (var i = 0; i < seeds.Length; i++)
        {
            for (var k = 0; k < maps.Length; k++)
            {
                seeds[i] = maps[k].Convert(seeds[i]);
            }
        }

        return seeds.Min();
    }

    public object SolvePartTwo(string[] input)
    {
        throw new NotImplementedException();
    }

    private static IEnumerable<long> ParseNumbers(string line) =>
        NumberRegex.Matches(line)
                   .Select(match => long.Parse(match.Value));

    private static IEnumerable<AlmanacMap> ParseMaps(string[] input) =>
        input.Select((line, index) => new { Content = line, Index = index })
             .Where(item => string.IsNullOrWhiteSpace(item.Content))
             .Select(item => item.Index + 1)
             .Select(start =>
                new AlmanacMap(
                    start,
                    ParseMapLines(input.Skip(start).TakeWhile(line => line != " ")).ToList()));

    private static IEnumerable<AlmanacMapLine> ParseMapLines(IEnumerable<string> lines) =>
        lines.Select(ParseNumbers)
             .Where(numbers => numbers.Any())
             .Select(numbers => new AlmanacMapLine(numbers.ElementAt(0), numbers.ElementAt(1), numbers.ElementAt(2)));

    record AlmanacMap(int Start, IEnumerable<AlmanacMapLine> Lines)
    {
        public long Convert(long number)
        {
            if (Lines.FirstOrDefault(line => line.CanConvert(number)) is AlmanacMapLine mapLine)
                return number - mapLine.Source + mapLine.Destination;
            else
                return number;
        }
    };

    record AlmanacMapLine(long Destination, long Source, long Length)
    {
        public bool CanConvert(long number) =>
            Source <= number && number < Source + Length;
    };
}
