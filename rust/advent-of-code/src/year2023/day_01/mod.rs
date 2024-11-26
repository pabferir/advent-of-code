use std::collections::HashMap;

use regex::Regex;

use crate::shared::daily_puzzle::DailyPuzzle;
use crate::shared::days::DAY_01;
use crate::shared::events::YEAR_2023;

#[cfg(test)]
mod tests;

pub struct Day01;

impl DailyPuzzle<i32, i32> for Day01 {
    const YEAR: &'static str = YEAR_2023;
    const DAY: &'static str = DAY_01;

    fn solve_part_one(input: &str) -> i32 {
        solve(input, Regex::new(r"\d").unwrap())
    }

    fn solve_part_two(input: &str) -> i32 {
        solve(input, Regex::new(r"\d|one|two|three|four|five|six|seven|eight|nine").unwrap())
    }
}

fn solve(input: &str, regex: Regex) -> i32 {
    let mut digits: HashMap<&'static str, i32> = HashMap::new();
    digits.insert("one", 1);
    digits.insert("two", 2);
    digits.insert("three", 3);
    digits.insert("four", 4);
    digits.insert("five", 5);
    digits.insert("six", 6);
    digits.insert("seven", 7);
    digits.insert("eight", 8);
    digits.insert("nine", 9);

    input.lines()
        .map(|line| {
            let matches: Vec<&str> = regex
                .find_iter(line)
                .map(|m| m.as_str())
                .collect();

            (*matches.first().unwrap(), *matches.last().unwrap())
        })
        .map(|t| parse_digit(t.0, &digits) * 10 + parse_digit(t.1, &digits))
        .sum()
}

fn parse_digit(digit: &str, look_up: &HashMap<&'static str, i32>) -> i32 {
    match digit.parse() {
        Ok(val) => val,
        _ => *look_up.get(digit).unwrap()
    }
}