use std::{cmp::Reverse, collections::{BinaryHeap, HashMap}};

use regex::Regex;

use crate::shared::{daily_puzzle::DailyPuzzle, days::DAY_01, events::YEAR_2024};

#[cfg(test)]
mod tests;

pub struct Day01;

impl DailyPuzzle<i32, i32> for Day01 {
    const YEAR: &'static str = YEAR_2024;
    const DAY: &'static str = DAY_01;

    fn solve_part_one(input: &str) -> i32 {
        let regex = Regex::new(r"(\d*)\s*(\d*)").unwrap();

        let mut left: BinaryHeap<Reverse<i32>> = BinaryHeap::new();
        let mut right: BinaryHeap<Reverse<i32>> = BinaryHeap::new();

        input.lines()
            .for_each(|line| {
                let captures = regex
                    .captures(line)
                    .unwrap();

                left.push(Reverse(captures.get(1).map(|m|m.as_str()).unwrap().parse::<i32>().unwrap()));
                right.push(Reverse(captures.get(2).map(|m|m.as_str()).unwrap().parse::<i32>().unwrap()));
            });

        let mut distance = 0;
        while !left.is_empty() && !right.is_empty() {
                distance += (left.pop().unwrap().0 - right.pop().unwrap().0).abs();
        }

        distance
    }

    fn solve_part_two(input: &str) -> i32 {
        let regex = Regex::new(r"(\d*)\s*(\d*)").unwrap();

        let mut left: Vec<i32> = Vec::new();
        let mut right: HashMap<i32, i32> = HashMap::new();

        input.lines()
            .for_each(|line| {
                let captures = regex
                    .captures(line)
                    .unwrap();

                left.push(captures.get(1).map(|m|m.as_str()).unwrap().parse::<i32>().unwrap());
                *right.entry(captures.get(2).map(|m|m.as_str()).unwrap().parse::<i32>().unwrap()).or_insert(0) += 1;
            });

        let mut similarity = 0;

        for num in left.iter() {
            similarity += num * right.get(num).unwrap_or(&0)
        }

        similarity
    }
}