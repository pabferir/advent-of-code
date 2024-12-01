use crate::shared::daily_puzzle::DailyPuzzle;

type Puzzle = crate::year2024::day_01::Day01;

#[test]
fn test_part_one() {
    let input = "3   4
4   3
2   5
1   3
3   9
3   3";

    assert_eq!(11, Puzzle::solve_part_one(input))
}

#[test]
fn test_part_two() {
    let input = "3   4
4   3
2   5
1   3
3   9
3   3";

    assert_eq!(31, Puzzle::solve_part_two(input))
}
