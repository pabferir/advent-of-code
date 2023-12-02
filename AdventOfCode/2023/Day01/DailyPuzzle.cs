using AdventOfCode.Shared;

namespace AdventOfCode._2023.Day01;

public class DailyPuzzle : IDailyPuzzle
{
    private static readonly Dictionary<string, string> _numbers =
    new()
    {
        { "one", "1" },
        { "two", "2" },
        { "three", "3" },
        { "four","4" },
        { "five", "5" },
        { "six", "6" },
        { "seven", "7" },
        { "eight", "8" },
        { "nine", "9" }
    };

    public string Year => AdventOfCodeEvents.Year2023;

    public string Day => AdventDays.Day01;

    /// <summary>
    /// <para>--- Day 1: Trebuchet?! --- </para>
    /// <para> Something is wrong with global snow production, and you've been selected to take a look. The Elves have even given you a map; on it, they've used stars to mark the top fifty locations that are likely to be having problems.</para>
    /// <para>You've been doing this long enough to know that to restore snow operations, you need to check all fifty stars by December 25th.</para>
    /// <para>Collect stars by solving puzzles.Two puzzles will be made available on each day in the Advent calendar; the second puzzle is unlocked when you complete the first.Each puzzle grants one star. Good luck!</para>
    /// <para>You try to ask why they can't just use a weather machine ("not powerful enough") and where they're even sending you ("the sky") and why your map looks mostly blank("you sure ask a lot of questions") and hang on did you just say the sky("of course, where do you think snow comes from") when you realize that the Elves are already loading you into a trebuchet("please hold still, we need to strap you in").</para>
    /// <para>As they're making the final adjustments, they discover that their calibration document (your puzzle input) has been amended by a very young Elf who was apparently just excited to show off her art skills. Consequently, the Elves are having trouble reading the values on the document.</para>
    /// <para>The newly-improved calibration document consists of lines of text; each line originally contained a specific calibration value that the Elves now need to recover.On each line, the calibration value can be found by combining the first digit and the last digit(in that order) to form a single two-digit number. For example:</para>
    /// <para>1abc2</para>
    /// <para>pqr3stu8vwx</para>
    /// <para>a1b2c3d4e5f</para>
    /// <para>treb7uchet</para>
    /// <para>In this example, the calibration values of these four lines are 12, 38, 15, and 77. Adding these together produces 142.</para>
    /// <para>Consider your entire calibration document.What is the sum of all of the calibration values?</para>
    /// </summary>
    public object SolvePartOne(string[] input)
    {
        var calibrationValuesSum = 0;

        foreach (string line in input)
        {
            string firstDigit = string.Empty, secondDigit = string.Empty;

            for (var i = 0; i < line.Length; i++)
            {
                if (TryExtractDigit(line[i], out firstDigit)) break;
            }

            for (var i = line.Length - 1; i >= 0; i--)
            {
                if (TryExtractDigit(line[i], out secondDigit)) break;
            }

            var calibrationValue = int.Parse(string.Concat(firstDigit, secondDigit));
            calibrationValuesSum += calibrationValue;
        }

        return calibrationValuesSum;
    }

    /// <summary>
    /// <para>--- Part Two ---</para>
    /// <para>Your calculation isn't quite right. It looks like some of the digits are actually spelled out with letters: one, two, three, four, five, six, seven, eight, and nine also count as valid "digits".</para>
    /// <para>Equipped with this new information, you now need to find the real first and last digit on each line. For example:</para>
    /// <para>two1nine</para>
    /// <para>eightwothree</para>
    /// <para>abcone2threexyz</para>
    /// <para>xtwone3four</para>
    /// <para>4nineeightseven2</para>
    /// <para>zoneight234</para>
    /// <para>7pqrstsixteen</para>
    /// <para>In this example, the calibration values are 29, 83, 13, 24, 42, 14, and 76. Adding these together produces 281.</para>
    /// <para>What is the sum of all of the calibration values?</para>
    /// </summary>
    public object SolvePartTwo(string[] input)
    {
        var calibrationValuesSum = 0;

        foreach (string line in input)
        {
            string firstDigit = string.Empty, secondDigit = string.Empty;

            for (var i = 0; i < line.Length; i++)
            {
                if (TryExtractDigit(line[i], out firstDigit)) break;
                else if (TryExtractLiteralDigit(line[..(i + 1)], out firstDigit)) break;
            }

            for (var i = line.Length - 1; i >= 0; i--)
            {
                if (TryExtractDigit(line[i], out secondDigit)) break;
                else if (TryExtractLiteralDigit(line[i..], out secondDigit)) break;
            }

            var calibrationValue = int.Parse(string.Concat(firstDigit, secondDigit));
            calibrationValuesSum += calibrationValue;
        }

        return calibrationValuesSum;
    }

    private static bool TryExtractDigit(char c, out string digit)
    {
        digit = string.Empty;

        if (char.IsDigit(c))
        {
            digit += c;
            return true;
        }

        return false;
    }

    private static bool TryExtractLiteralDigit(string fragment, out string digit)
    {
        digit = string.Empty;

        if (_numbers.Keys.Any(fragment.Contains))
        {
            var literal = _numbers.Keys.Single(fragment.Contains);
            digit += _numbers[literal];
            return true;
        }

        return false;
    }
}
