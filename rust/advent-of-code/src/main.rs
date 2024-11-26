use std::fs;

use crate::shared::daily_puzzle::DailyPuzzle;

mod shared;
mod year2023;

type Puzzle = year2023::day_01::Day01;

fn main() {
    let path = format!("src/{}/{}/input.txt", &Puzzle::YEAR, &Puzzle::DAY);
    let input = fs::read_to_string(path)
        .expect("Failed to read input file.");

    println!("{}", Puzzle::solve_part_one(&input));
    println!("{}", Puzzle::solve_part_two(&input));
}
