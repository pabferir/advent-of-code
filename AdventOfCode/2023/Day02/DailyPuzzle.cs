using System.Text.RegularExpressions;
using AdventOfCode.Shared;

namespace AdventOfCode._2023.Day02;

public class DailyPuzzle : IDailyPuzzle
{
    private const int RedCubesInBag = 12;
    private const int GreenCubesInBag = 13;
    private const int BlueCubesInBag = 14;

    public string Year => AdventOfCodeEvents.Year2023;

    public string Day => AdventDays.Day02;

    /// <summary>
    /// <para>--- Day 2: Cube Conundrum ---</para>
    /// <para>You're launched high into the atmosphere! The apex of your trajectory just barely reaches the surface of a large island floating in the sky. You gently land in a fluffy pile of leaves. It's quite cold, but you don't see much snow. An Elf runs over to greet you.</para>
    /// <para>The Elf explains that you've arrived at Snow Island and apologizes for the lack of snow. He'll be happy to explain the situation, but it's a bit of a walk, so you have some time. They don't get many visitors up here; would you like to play a game in the meantime?</para>
    /// <para>As you walk, the Elf shows you a small bag and some cubes which are either red, green, or blue.Each time you play this game, he will hide a secret number of cubes of each color in the bag, and your goal is to figure out information about the number of cubes.</para>
    /// <para>To get information, once a bag has been loaded with cubes, the Elf will reach into the bag, grab a handful of random cubes, show them to you, and then put them back in the bag. He'll do this a few times per game.</para>
    /// <para>You play several games and record the information from each game (your puzzle input). Each game is listed with its ID number(like the 11 in Game 11: ...) followed by a semicolon-separated list of subsets of cubes that were revealed from the bag(like 3 red, 5 green, 4 blue).</para>
    /// <para>For example, the record of a few games might look like this:</para>
    /// <para>Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green</para>
    /// <para>Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue</para>
    /// <para>Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red</para>
    /// <para>Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red</para>
    /// <para>Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green</para>
    /// <para>In game 1, three sets of cubes are revealed from the bag(and then put back again). The first set is 3 blue cubes and 4 red cubes; the second set is 1 red cube, 2 green cubes, and 6 blue cubes; the third set is only 2 green cubes.</para>
    /// <para>The Elf would first like to know which games would have been possible if the bag contained only 12 red cubes, 13 green cubes, and 14 blue cubes?</para>
    /// <para>In the example above, games 1, 2, and 5 would have been possible if the bag had been loaded with that configuration. However, game 3 would have been impossible because at one point the Elf showed you 20 red cubes at once; similarly, game 4 would also have been impossible because the Elf showed you 15 blue cubes at once.If you add up the IDs of the games that would have been possible, you get 8.</para>
    /// <para>Determine which games would have been possible if the bag had been loaded with only 12 red cubes, 13 green cubes, and 14 blue cubes. What is the sum of the IDs of those games?</para>
    /// </summary>
    /// <param name="input"></param>
    public object SolvePartOne(string[] input)
    {
        int gameId = 1, possibleGamesIdsSum = 0;

        foreach (var line in input)
        {
            var gameSets = line.Split(':')[1].Split(';');
            int currentSet = 0;
            foreach (var set in gameSets)
            {
                currentSet++;
                var redCubes = GetCubesCountInSet(set, @"\d+ red");
                var greenCubes = GetCubesCountInSet(set, @"\d+ green");
                var blueCubes = GetCubesCountInSet(set, @"\d+ blue");

                if (IsGameImpossible(redCubes, greenCubes, blueCubes)) break;
                else if (currentSet == gameSets.Length) possibleGamesIdsSum += gameId;
            }

            gameId++;
        }

        return possibleGamesIdsSum;
    }

    /// <summary>
    /// <para>--- Part Two ---</para>
    /// <para>The Elf says they've stopped producing snow because they aren't getting any water! He isn't sure why the water stopped; however, he can show you how to get to the water source to check it out for yourself. It's just up ahead!</para>
    /// <para>As you continue your walk, the Elf poses a second question: in each game you played, what is the fewest number of cubes of each color that could have been in the bag to make the game possible?</para>
    /// <para>Again consider the example games from earlier:</para>
    /// <para>Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green</para>
    /// <para>Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue</para>
    /// <para>Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red</para>
    /// <para>Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red</para>
    /// <para>Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green</para>
    /// <para>In game 1, the game could have been played with as few as 4 red, 2 green, and 6 blue cubes.If any color had even one fewer cube, the game would have been impossible.
    /// Game 2 could have been played with a minimum of 1 red, 3 green, and 4 blue cubes.
    /// Game 3 must have been played with at least 20 red, 13 green, and 6 blue cubes.
    /// Game 4 required at least 14 red, 3 green, and 15 blue cubes.
    /// Game 5 needed no fewer than 6 red, 3 green, and 2 blue cubes in the bag.</para>
    /// <para>The power of a set of cubes is equal to the numbers of red, green, and blue cubes multiplied together.The power of the minimum set of cubes in game 1 is 48. In games 2-5 it was 12, 1560, 630, and 36, respectively.Adding up these five powers produces the sum 2286.</para>
    /// <para>For each game, find the minimum set of cubes that must have been present.What is the sum of the power of these sets?</para>
    /// </summary>
    public object SolvePartTwo(string[] input)
    {
        int gameId = 1, minSetPowerSum = 0;

        foreach (var line in input)
        {
            var gameSets = line.Split(':')[1].Split(';');
            int currentSet = 0, minRedCubesRequired = 0, minGreenCubesRequired = 0, minBlueCubesRequired = 0;
            foreach (var set in gameSets)
            {
                currentSet++;
                var redCubes = GetCubesCountInSet(set, @"\d+ red");
                if (redCubes > minRedCubesRequired) minRedCubesRequired = redCubes;

                var greenCubes = GetCubesCountInSet(set, @"\d+ green");
                if (greenCubes > minGreenCubesRequired) minGreenCubesRequired = greenCubes;

                var blueCubes = GetCubesCountInSet(set, @"\d+ blue");
                if (blueCubes > minBlueCubesRequired) minBlueCubesRequired = blueCubes;

                if (currentSet == gameSets.Length) minSetPowerSum += minRedCubesRequired * minGreenCubesRequired * minBlueCubesRequired;
            }

            gameId++;
        }

        return minSetPowerSum;
    }

    private static int GetCubesCountInSet(string set, string pattern)
    {
        var cubes = Regex.Match(set, pattern).Value;
        var count = Regex.Match(cubes, @"\d+");

        return count.Success ? int.Parse(count.Value) : 0;
    }

    private static bool IsGameImpossible(int redCubes, int greenCubes, int blueCubes)
    {
        if (redCubes > RedCubesInBag)
        {
            return true;
        }
        else if (greenCubes > GreenCubesInBag)
        {
            return true;
        }
        else if (blueCubes > BlueCubesInBag)
        {
            return true;
        }

        return false;
    }
}
