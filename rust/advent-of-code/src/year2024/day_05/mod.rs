use std::collections::HashMap;

use crate::shared::{daily_puzzle::DailyPuzzle, days, events};

#[cfg(test)]
mod tests;

pub struct Day05;

impl DailyPuzzle<u32, u32> for Day05 {
    const YEAR: &'static str = events::YEAR_2024;
    const DAY: &'static str = days::DAY_05;

    fn solve_part_one(input: &str) -> u32 {
        let rules = parse_rules(input);

        input
            .lines()
            .skip_while(|line| !line.is_empty())
            .skip(1)
            .map(|update| update.split(',').collect::<Vec<&str>>())
            .filter(|update_pages| {
                for (i, page) in update_pages.iter().enumerate() {
                    if rules.contains_key(page) {
                        let pages_after = &rules[page];

                        if update_pages[0..i]
                            .iter()
                            .any(|page_before| pages_after.contains(page_before))
                        {
                            return false;
                        }
                    }
                }

                true
            })
            .map(|update_pages| update_pages[update_pages.len() / 2].parse::<u32>().unwrap())
            .sum()
    }

    fn solve_part_two(input: &str) -> u32 {
        todo!()
    }
}

fn parse_rules(input: &str) -> HashMap<&str, Vec<&str>> {
    let mut rules = HashMap::new();
    input
        .lines()
        .take_while(|line| !line.is_empty())
        .for_each(|rule| {
            let parts = rule.split('|').collect::<Vec<&str>>();
            rules
                .entry(parts[0])
                .or_insert_with(|| Vec::new())
                .push(parts[1]);
        });

    rules
}
