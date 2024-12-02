use crate::shared::daily_puzzle::DailyPuzzle;

type Puzzle = crate::year2024::day_02::Day02;

#[test]
fn test_part_one() {
    let input = "7 6 4 2 1
1 2 7 8 9
9 7 6 2 1
1 3 2 4 5
8 6 4 4 1
1 3 6 7 9";

    assert_eq!(2, Puzzle::solve_part_one(input))
}

#[test]
fn test_part_two() {
}
