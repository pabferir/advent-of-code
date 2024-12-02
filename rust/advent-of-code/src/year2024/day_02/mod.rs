use crate::shared::{daily_puzzle::DailyPuzzle, days::DAY_02, events::YEAR_2024};

#[cfg(test)]
mod tests;

pub struct Day02;

impl DailyPuzzle<i32, i32> for Day02 {
    const YEAR: &'static str = YEAR_2024;
    const DAY: &'static str = DAY_02;

    fn solve_part_one(input: &str) -> i32 {
        input.lines()
            .map(|line| Report::from_str(line))
            .filter(|report| report.is_safe())
            .count() as i32
    }

    fn solve_part_two(input: &str) -> i32 {
        input.lines()
            .map(|line| Report::from_str(line))
            .filter(|report| {
                if report.is_safe() {
                    return true
                }
                
                for i in 0..report.len() {
                    let subreport = [&report[0..i], &report[i+1..]].concat();
                    if subreport.is_safe() {
                        return true
                    }
                }

                false
            })
            .count() as i32
    }
}

type Report = Vec<i32>;

trait FromStr {
    fn from_str(s: &str) -> Self;
}

impl FromStr for Report {
    fn from_str(s: &str) -> Self {
        s.split_whitespace()
            .map(|level| level.parse::<i32>().unwrap())
            .collect()
    }
}

trait Safety {
    fn is_safe(&self) -> bool;
}

impl Safety for Report {
    fn is_safe(&self) -> bool {
        let is_dec = self.first() > self.last();

        self.windows(2)
            .all(|pair| {
                let (a, b) = (pair[0], pair[1]);
                let diff = (a - b).abs();

                let is_linear = if is_dec {
                    a > b
                } else {
                    a < b
                };

                1 <= diff && diff <= 3 && is_linear
            })
    }
}