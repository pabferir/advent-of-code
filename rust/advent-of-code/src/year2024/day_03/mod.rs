use regex::Regex;

use crate::shared::{daily_puzzle::DailyPuzzle, days::DAY_03, events::YEAR_2024};

#[cfg(test)]
mod tests;

pub struct Day03;

impl DailyPuzzle<i32, i32> for Day03 {
    const YEAR: &'static str = YEAR_2024;
    const DAY: &'static str = DAY_03;

    fn solve_part_one(input: &str) -> i32 {
        parse(input, Regex::new(r"mul\((\d*),(\d*)\)").unwrap())
            .iter()
            .map(|mul| mul.0 * mul.1)
            .sum()
    }

    fn solve_part_two(input: &str) -> i32 {
        todo!()
    }
}

fn parse(input: &str, regex: Regex) -> Vec<(i32, i32)> {
    regex.captures_iter(input)
        .map(|captures | (captures.get(1).map(|m|m.as_str()).unwrap().parse::<i32>().unwrap(), captures.get(2).map(|m|m.as_str()).unwrap().parse::<i32>().unwrap()))
        .inspect(|tuple| {
            dbg!(tuple);
        })
        .collect()
}