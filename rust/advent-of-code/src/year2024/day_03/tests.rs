use crate::shared::daily_puzzle::DailyPuzzle;

type Puzzle = crate::year2024::day_03::Day03;

#[test]
fn test_part_one() {
    let input = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";

    assert_eq!(161, Puzzle::solve_part_one(input))
}

#[test]
fn test_part_two() {
    let input = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";

    assert_eq!(281, Puzzle::solve_part_two(input))
}
