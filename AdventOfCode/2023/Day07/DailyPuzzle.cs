using AdventOfCode.Shared;
using System.Text.RegularExpressions;

namespace AdventOfCode._2023.Day07;

public class DailyPuzzle : IDailyPuzzle
{
    private static readonly Regex HandRegex = new Regex(@"([A|K|Q|J|T|9|8|7|6|5|4|3|2]+) (\d+)");
    private static readonly Regex AllCharactersRegex = new Regex(".");
    private static readonly Regex JokerRegex = new Regex("J");

    public string Year => AdventOfCodeEvents.Year2023;

    public string Day => AdventDays.Day07;

    public object SolvePartOne(string[] input)
    {
        // Parse Hands + Bid
        // Sort by type
        // Then by card label, where A is the strongest and 2 the weakest
        // Zip with Rank: weakest hand => Rank 1, strongest hand Rank N

        // Determine the total winnings of the hand set
        // Hand WinnedAmout = Bid * Rank
        // Sum
        var ranks = Enumerable.Range(1, input.Length).Reverse();

        return ParseHands(input)
                    .OrderBy(hand => ParseCards(hand.Cards).ToList() switch
                    {
                        [5] => HandType.FiveOfAKind,
                        [4, 1] => HandType.FourOfAKind,
                        [3, 2] => HandType.FullHouse,
                        [3, 1, 1] => HandType.ThreeOfAKind,
                        [2, 2, 1] => HandType.TwoPair,
                        [2, 1, 1, 1] => HandType.OnePair,
                        [1, 1, 1, 1, 1] => HandType.HighCard,
                        _ => throw new InvalidOperationException(),
                    })
                    .ThenBy(hand => hand, CompareHandsByStrength(['A', 'K', 'Q', 'J', 'T', '9', '8', '7', '6', '5', '4', '3', '2']))
                    .Zip(ranks)
                    .Select(tuple => tuple.First.Bid * tuple.Second)
                    .Sum();
    }

    public object SolvePartTwo(string[] input)
    {
        // J = Jokers
        // wildcards that can act like whatever card would MAKE THE HAND THE STRONGEST TYPE POSSIBLE.
        var ranks = Enumerable.Range(1, input.Length).Reverse();

        return ParseHands(input)
                    .OrderBy(hand => (ParseCards(hand.Cards).ToList(), hand.Jokers) switch
                    {
                        ([5], _) => HandType.FiveOfAKind,
                        ([4, 1], > 0) => HandType.FiveOfAKind,
                        ([4, 1], 0) => HandType.FourOfAKind,
                        ([3, 2], > 0) => HandType.FiveOfAKind,
                        ([3, 2], 0) => HandType.FullHouse,
                        ([3, 1, 1], > 0) => HandType.FourOfAKind,
                        ([3, 1, 1], 0) => HandType.ThreeOfAKind,
                        ([2, 2, 1], 2) => HandType.FourOfAKind,
                        ([2, 2, 1], 1) => HandType.ThreeOfAKind,
                        ([2, 2, 1], 0) => HandType.TwoPair,
                        ([2, 1, 1, 1], > 0) => HandType.ThreeOfAKind,
                        ([2, 1, 1, 1], 0) => HandType.OnePair,
                        ([1, 1, 1, 1, 1], > 0) => HandType.OnePair,
                        ([1, 1, 1, 1, 1], 0) => HandType.HighCard,
                        _ => throw new InvalidOperationException(),
                    })
                    .ThenBy(hand => hand, CompareHandsByStrength(['A', 'K', 'Q', 'T', '9', '8', '7', '6', '5', '4', '3', '2', 'J']))
                    .Zip(ranks)
                    .Select(tuple => tuple.First.Bid * tuple.Second)
                    .Sum();
    }

    private static IEnumerable<Hand> ParseHands(string[] input) =>
        input.SelectMany(line => HandRegex.Matches(line))
             .Select(match => new Hand(match.Groups[1].Value, JokerRegex.Matches(match.Groups[1].Value).Count, int.Parse(match.Groups[2].Value)));

    private static IEnumerable<int> ParseCards(string cards) =>
        AllCharactersRegex.Matches(cards)
                          .GroupBy(match => match.Value)
                          .Select(group => group.Count())
                          .OrderByDescending(value => value);

    private static Comparer<Hand> CompareHandsByStrength(char[] sortedDeck)
    {
        return Comparer<Hand>.Create((first, second) =>
        {
            foreach (var (First, Second) in first.Cards.Zip(second.Cards))
            {
                if (First != Second)
                {
                    return CompareCards(First, Second, sortedDeck);
                }
            }

            return 0;
        });
    }

    private static int CompareCards(char first, char second, char[] sortedDeck) =>
        Array.IndexOf(sortedDeck, first).CompareTo(Array.IndexOf(sortedDeck, second));

    record Hand(string Cards, int Jokers, int Bid);

    private enum HandType
    {
        FiveOfAKind, FourOfAKind, FullHouse, ThreeOfAKind, TwoPair, OnePair, HighCard
    }
}
