using AdventOfCode.Shared;
using System.Net;
using System.Text.RegularExpressions;

namespace AdventOfCode._2023.Day04;

public class DailyPuzzle : IDailyPuzzle
{
    private static readonly Regex NumberRegex = new(@"(\d+)");

    public string Year => AdventOfCodeEvents.Year2023;

    public string Day => AdventDays.Day04;

    /// <summary>
    /// <para> --- Day 4: Scratchcards ---</para>
    /// <para> The gondola takes you up. Strangely, though, the ground doesn't seem to be coming with you; you're not climbing a mountain.As the circle of Snow Island recedes below you, an entire new landmass suddenly appears above you! The gondola carries you to the surface of the new island and lurches into the station.</para>
    /// <para> As you exit the gondola, the first thing you notice is that the air here is much warmer than it was on Snow Island. It's also quite humid. Is this where the water source is?</para>
    /// <para> The next thing you notice is an Elf sitting on the floor across the station in what seems to be a pile of colorful square cards.</para>
    /// <para> "Oh! Hello!" The Elf excitedly runs over to you. "How may I be of service?" You ask about water sources.</para>
    /// <para> "I'm not sure; I just operate the gondola lift. That does sound like something we'd have, though - this is Island Island, after all! I bet the gardener would know. He's on a different island, though - er, the small kind surrounded by water, not the floating kind. We really need to come up with a better naming scheme. Tell you what: if you can help me with something quick, I'll let you borrow my boat and you can go visit the gardener. I got all these scratchcards as a gift, but I can't figure out what I've won."</para>
    /// <para> The Elf leads you over to the pile of colorful cards.There, you discover dozens of scratchcards, all with their opaque covering already scratched off. Picking one up, it looks like each card has two lists of numbers separated by a vertical bar (|): a list of winning numbers and then a list of numbers you have.You organize the information into a table(your puzzle input).</para>
    /// <para> As far as the Elf has been able to figure out, you have to figure out which of the numbers you have appear in the list of winning numbers.The first match makes the card worth one point and each match after the first doubles the point value of that card.</para>
    /// <para> For example:</para>
    /// <code>
    /// Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
    /// Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
    /// Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
    /// Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
    /// Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
    /// Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11
    /// </code>
    /// <para> In the above example, card 1 has five winning numbers (41, 48, 83, 86, and 17) and eight numbers you have(83, 86, 6, 31, 17, 9, 48, and 53). Of the numbers you have, four of them(48, 83, 17, and 86) are winning numbers! That means card 1 is worth 8 points(1 for the first match, then doubled three times for each of the three matches after the first).
    /// </para>
    /// <para>
    /// Card 2 has two winning numbers(32 and 61), so it is worth 2 points.
    /// Card 3 has two winning numbers(1 and 21), so it is worth 2 points.
    /// Card 4 has one winning number(84), so it is worth 1 point.
    /// Card 5 has no winning numbers, so it is worth no points.
    /// Card 6 has no winning numbers, so it is worth no points.
    /// So, in this example, the Elf's pile of scratchcards is worth 13 points.
    /// </para>
    /// <para>Take a seat in the large pile of colorful cards.How many points are they worth in total?</para>
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public object SolvePartOne(string[] input)
    {
        return input.Select(ParseScratchCard)
                    .Select(card => card.Matchings)
                    .Select(matchings => 1 * (int)Math.Pow(2, matchings - 1))
                    .Sum();
    }

    /// <summary>
    /// <para> --- Part Two --- </para>
    /// <para> Just as you're about to report your findings to the Elf, one of you realizes that the rules have actually been printed on the back of every card this whole time.</para>
    /// <para>There's no such thing as "points". Instead, scratchcards only cause you to win more scratchcards equal to the number of winning numbers you have.</para>
    /// <para>Specifically, you win copies of the scratchcards below the winning card equal to the number of matches.So, if card 10 were to have 5 matching numbers, you would win one copy each of cards 11, 12, 13, 14, and 15.</para>
    /// <para>Copies of scratchcards are scored like normal scratchcards and have the same card number as the card they copied. So, if you win a copy of card 10 and it has 5 matching numbers, it would then win a copy of the same cards that the original card 10 won: cards 11, 12, 13, 14, and 15. This process repeats until none of the copies cause you to win any more cards. (Cards will never make you copy a card past the end of the table.)</para>
    /// <para>This time, the above example goes differently:</para>
    /// <code>
    /// Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
    /// Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
    /// Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
    /// Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
    /// Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
    /// Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11
    /// </code>
    /// <para> Card 1 has four matching numbers, so you win one copy each of the next four cards: cards 2, 3, 4, and 5.
    /// Your original card 2 has two matching numbers, so you win one copy each of cards 3 and 4.
    /// Your copy of card 2 also wins one copy each of cards 3 and 4.
    /// Your four instances of card 3 (one original and three copies) have two matching numbers, so you win four copies each of cards 4 and 5.
    /// Your eight instances of card 4 (one original and seven copies) have one matching number, so you win eight copies of card 5.
    /// Your fourteen instances of card 5 (one original and thirteen copies) have no matching numbers and win no more cards.
    /// Your one instance of card 6 (one original) has no matching numbers and wins no more cards.</para>
    /// <para>Once all of the originals and copies have been processed, you end up with 1 instance of card 1, 2 instances of card 2, 4 instances of card 3, 8 instances of card 4, 14 instances of card 5, and 1 instance of card 6. In total, this example pile of scratchcards causes you to ultimately have 30 scratchcards!</para>
    /// <para> Process all of the original and copied scratchcards until no more scratchcards are won. Including the original set of scratchcards, how many total scratchcards do you end up with?</para>
    /// </summary>
    /// <param name="input"></param>
    public object SolvePartTwo(string[] input)
    {
        var instances = Enumerable.Repeat(1, input.Length).ToArray();

        return input.Select(ParseScratchCard)
                    .Select((card, cardIndex) =>
                    {
                        if (card.Matchings > 0)
                        {
                            var matchingsOrCardsLeft = Math.Min(card.Matchings, input.Length - 1 - cardIndex);

                            for (int i = 1; i <= matchingsOrCardsLeft; i++)
                            {
                                instances[cardIndex + i] += instances[cardIndex];
                            }
                        }

                        return instances[cardIndex];
                    })
                    .Sum();
    }

    private static ScratchCard ParseScratchCard(string line)
    {
        var cardParts = line.Split(':', '|');

        return new(ParseInts(cardParts[1], NumberRegex), ParseInts(cardParts[2], NumberRegex));
    }

    private static IEnumerable<int> ParseInts(string line, Regex regex) =>
        regex.Matches(line)
             .Select(match => int.Parse(match.Groups[1].Value));

    record ScratchCard(IEnumerable<int> WinningNumbers, IEnumerable<int> Numbers)
    {
        public int Matchings => WinningNumbers.Intersect(Numbers).Count();
    }
}
