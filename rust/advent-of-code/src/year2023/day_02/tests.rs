use crate::shared::daily_puzzle::DailyPuzzle;

type Puzzle = crate::year2023::day_02::Day02;

#[test]
fn test_part_one() {
    let input = "1abc2
    pqr3stu8vwx
    a1b2c3d4e5f
    treb7uchet";

    assert_eq!(142, Puzzle::solve_part_one(input))
}

#[test]
fn test_part_two() {
    let input = "two1nine
    eightwothree
    abcone2threexyz
    xtwone3four
    4nineeightseven2
    zoneight234
    7pqrstsixteen";

    assert_eq!(281, Puzzle::solve_part_two(input))
}
