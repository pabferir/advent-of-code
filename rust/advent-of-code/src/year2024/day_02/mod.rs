use core::panic;
use crate::shared::{daily_puzzle::DailyPuzzle, days::DAY_02, events::YEAR_2024};

#[cfg(test)]
mod tests;

pub struct Day02;

impl DailyPuzzle<i32, i32> for Day02 {
    const YEAR: &'static str = YEAR_2024;
    const DAY: &'static str = DAY_02;

    fn solve_part_one(input: &str) -> i32 {
        input.lines()
        .filter(|report| is_save(&report))
        .count() as i32
    }

    fn solve_part_two(input: &str) -> i32 {
        panic!()
    }
}

fn is_save(report: &str) -> bool {
    let mut levels: Vec<i32> = report
        .split_whitespace()
        .map(|level| level.parse::<i32>().unwrap())
        .collect();

    if levels.first() > levels.last() {
        levels.reverse();
    }

    levels.windows(2)
        .all(|pair| {
            let (a, b) = (pair[0], pair[1]);
            a < b && (a - b).abs() < 4
        })
}