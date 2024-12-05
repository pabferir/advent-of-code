use crate::shared::daily_puzzle::DailyPuzzle;

type Puzzle = crate::year2024::day_04::Day04;

#[test]
fn test_part_one() {
    let input = "MMMSXXMASM
MSAMXMSMSA
AMXSXMAAMM
MSAMASMSMX
XMASAMXAMM
XXAMMXXAMA
SMSMSASXSS
SAXAMASAAA
MAMMMXMMMM
MXMXAXMASX";

    assert_eq!(18, Puzzle::solve_part_one(input))
}

#[test]
fn test_part_two() {
    todo!()
}
