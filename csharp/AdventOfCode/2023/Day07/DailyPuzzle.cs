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

    /// <summary>
    /// <para> --- Day 7: Camel Cards --- </para>
    /// <para> Your all-expenses-paid trip turns out to be a one-way, five-minute ride in an airship. (At least it's a cool airship!) It drops you off at the edge of a vast desert and descends back to Island Island.</para>
    /// <para> "Did you bring the parts?"</para>
    /// <para> You turn around to see an Elf completely covered in white clothing, wearing goggles, and riding a large camel.</para>
    /// <para> "Did you bring the parts?" she asks again, louder this time.You aren't sure what parts she's looking for; you're here to figure out why the sand stopped.</para>
    /// <para> "The parts! For the sand, yes! Come with me; I will show you." She beckons you onto the camel.
    ///     After riding a bit across the sands of Desert Island, you can see what look like very large rocks covering half of the horizon. The Elf explains that the rocks are all along the part of Desert Island that is directly above Island Island, making it hard to even get there.Normally, they use big machines to move the rocks and filter the sand, but the machines have broken down because Desert Island recently stopped receiving the parts they need to fix the machines.</para>
    /// <para> You've already assumed it'll be your job to figure out why the parts stopped when she asks if you can help.You agree automatically.
    ///     Because the journey will take a few days, she offers to teach you the game of Camel Cards. Camel Cards is sort of similar to poker except it's designed to be easier to play while riding a camel.
    /// </para>
    /// <para>In Camel Cards, you get a list of hands, and your goal is to order them based on the strength of each hand. A hand consists of five cards labeled one of A, K, Q, J, T, 9, 8, 7, 6, 5, 4, 3, or 2. The relative strength of each card follows this order, where A is the highest and 2 is the lowest.</para>
    /// <para>Every hand is exactly one type. From strongest to weakest, they are:</para>
    /// <para>
    /// <list type="bullet">
    ///     <item> Five of a kind, where all five cards have the same label: AAAAA</item>
    ///     <item> Four of a kind, where four cards have the same label and one card has a different label: AA8AA</item>
    ///     <item> Full house, where three cards have the same label, and the remaining two cards share a different label: 23332</item>
    ///     <item> Three of a kind, where three cards have the same label, and the remaining two cards are each different from any other card in the hand: TTT98</item>
    ///     <item> Two pair, where two cards share one label, two other cards share a second label, and the remaining card has a third label: 23432</item>
    ///     <item> One pair, where two cards share one label, and the other three cards have a different label from the pair and each other: A23A4</item>
    ///     <item> High card, where all cards' labels are distinct: 23456</item>
    /// </list>
    /// Hands are primarily ordered based on type; for example, every full house is stronger than any three of a kind.
    ///     If two hands have the same type, a second ordering rule takes effect.Start by comparing the first card in each hand. If these cards are different, the hand with the stronger first card is considered stronger. If the first card in each hand have the same label, however, then move on to considering the second card in each hand. If they differ, the hand with the higher second card wins; otherwise, continue with the third card in each hand, then the fourth, then the fifth.
    /// </para>
    /// <para>So, 33332 and 2AAAA are both four of a kind hands, but 33332 is stronger because its first card is stronger.Similarly, 77888 and 77788 are both a full house, but 77888 is stronger because its third card is stronger (and both hands have the same first and second card).</para>
    /// <para>To play Camel Cards, you are given a list of hands and their corresponding bid(your puzzle input). For example:</para>
    /// <code>
    /// 32T3K 765
    /// T55J5 684
    /// KK677 28
    /// KTJJT 220
    /// QQQJA 483
    /// </code>
    /// <para>This example shows five hands; each hand is followed by its bid amount.Each hand wins an amount equal to its bid multiplied by its rank, where the weakest hand gets rank 1, the second-weakest hand gets rank 2, and so on up to the strongest hand.Because there are five hands in this example, the strongest hand will have rank 5 and its bid will be multiplied by 5.</para>
    /// <para>So, the first step is to put the hands in order of strength:</para>
    /// <list type="bullet">
    ///     <item>32T3K is the only one pair and the other hands are all a stronger type, so it gets rank 1.</item>
    ///     <item>KK677 and KTJJT are both two pair.Their first cards both have the same label, but the second card of KK677 is stronger(K vs T), so KTJJT gets rank 2 and KK677 gets rank 3.</item>
    ///     <item>T55J5 and QQQJA are both three of a kind.QQQJA has a stronger first card, so it gets rank 5 and T55J5 gets rank 4.</item>
    /// </list>
    /// <para>Now, you can determine the total winnings of this set of hands by adding up the result of multiplying each hand's bid with its rank (765 * 1 + 220 * 2 + 28 * 3 + 684 * 4 + 483 * 5). So the total winnings in this example are 6440.</para>
    /// <para>Find the rank of every hand in your set. What are the total winnings?</para>
    /// </summary>
    /// <param name="input"></param>
    public object SolvePartOne(string[] input)
    {
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
                    .ThenBy(hand => hand, CompareHandsByStrength("AKQJT98765432"))
                    .Reverse()
                    .Select((hand, index) => hand.Bid * (index + 1))
                    .Sum();
    }

    /// <summary>
    /// <para> --- Part Two --- </para>
    /// <para> To make things a little more interesting, the Elf introduces one additional rule.Now, J cards are jokers - wildcards that can act like whatever card would make the hand the strongest type possible.</para>
    /// <para> To balance this, J cards are now the weakest individual cards, weaker even than 2. The other cards stay in the same order: A, K, Q, T, 9, 8, 7, 6, 5, 4, 3, 2, J.</para>
    /// <para> J cards can pretend to be whatever card is best for the purpose of determining hand type; for example, QJJQ2 is now considered four of a kind.However, for the purpose of breaking ties between two hands of the same type, J is always treated as J, not the card it's pretending to be: JKKK2 is weaker than QQQQ2 because J is weaker than Q.</para>
    /// <para> Now, the above example goes very differently:</para>
    /// <code>
    /// 32T3K 765
    /// T55J5 684
    /// KK677 28
    /// KTJJT 220
    /// QQQJA 483
    /// </code>
    /// <list type="bullet">
    ///     <item>32T3K is still the only one pair; it doesn't contain any jokers, so its strength doesn't increase.</item>
    ///     <item>KK677 is now the only two pair, making it the second-weakest hand.</item>
    ///     <item>T55J5, KTJJT, and QQQJA are now all four of a kind! T55J5 gets rank 3, QQQJA gets rank 4, and KTJJT gets rank 5.</item>
    /// </list>
    /// <para>With the new joker rule, the total winnings in this example are 5905.</para>
    /// <para>Using the new joker rule, find the rank of every hand in your set.What are the new total winnings?</para>
    /// </summary>
    /// <param name="input"></param>
    public object SolvePartTwo(string[] input)
    {
        return ParseHands(input)
                    .OrderBy(hand => (ParseCards(hand.Cards).ToList(), hand.Jokers) switch
                    {
                        ([5], _) => HandType.FiveOfAKind,
                        ([4, 1], 4 or 1) => HandType.FiveOfAKind,
                        ([4, 1], 0) => HandType.FourOfAKind,
                        ([3, 2], 3 or 2) => HandType.FiveOfAKind,
                        ([3, 2], 0) => HandType.FullHouse,
                        ([3, 1, 1], 3 or 1) => HandType.FourOfAKind,
                        ([3, 1, 1], 0) => HandType.ThreeOfAKind,
                        ([2, 2, 1], 2) => HandType.FourOfAKind,
                        ([2, 2, 1], 1) => HandType.FullHouse,
                        ([2, 2, 1], 0) => HandType.TwoPair,
                        ([2, 1, 1, 1], 2 or 1) => HandType.ThreeOfAKind,
                        ([2, 1, 1, 1], 0) => HandType.OnePair,
                        ([1, 1, 1, 1, 1], 1) => HandType.OnePair,
                        ([1, 1, 1, 1, 1], 0) => HandType.HighCard,
                        _ => throw new InvalidOperationException(),
                    })
                    .ThenBy(hand => hand, CompareHandsByStrength("AKQT98765432J"))
                    .Reverse()
                    .Select((hand, index) => hand.Bid * (index + 1))
                    .Sum();
    }

    private static IEnumerable<Hand> ParseHands(string[] input) =>
        input.SelectMany(line => HandRegex.Matches(line))
             .Select(match =>
                new Hand(
                    match.Groups[1].Value,
                    JokerRegex.Matches(match.Groups[1].Value).Count,
                    int.Parse(match.Groups[2].Value)
                ));

    private static IEnumerable<int> ParseCards(string cards) =>
        AllCharactersRegex.Matches(cards)
                          .GroupBy(match => match.Value)
                          .Select(group => group.Count())
                          .OrderByDescending(value => value);

    private static Comparer<Hand> CompareHandsByStrength(string sortedDeck)
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

    private static int CompareCards(char first, char second, string sortedDeck) =>
        sortedDeck.IndexOf(first).CompareTo(sortedDeck.IndexOf(second));

    record Hand(string Cards, int Jokers, int Bid);

    private enum HandType
    {
        FiveOfAKind, FourOfAKind, FullHouse, ThreeOfAKind, TwoPair, OnePair, HighCard
    }
}
