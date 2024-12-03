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
        parse(input, Regex::new(r"mul\((\d*),(\d*)\)|do\(\)|don't\(\)").unwrap())
            .iter()
            .map(|mul| mul.0 * mul.1)
            .sum()
    }
}

fn parse(input: &str, regex: Regex) -> Vec<(i32, i32)> {
    let mut muls : Vec<(i32, i32)> = Vec::new();
    let mut enabled = true;

    regex.captures_iter(input)
        .for_each(|captures| {
            let instruction = captures.get(0).map(|m|m.as_str()).unwrap();

            match instruction {
                "don't()" => enabled = false,
                "do()" => enabled = true,
                _ => {
                    if enabled {
                        muls.push((
                            captures.get(1).map(|m|m.as_str()).unwrap().parse::<i32>().unwrap(), 
                            captures.get(2).map(|m|m.as_str()).unwrap().parse::<i32>().unwrap()
                        ));
                    }
                }
            }
        });

    muls
}