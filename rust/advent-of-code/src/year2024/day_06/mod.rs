use std::{collections::HashSet, fmt::Debug, str};

use crate::shared::{daily_puzzle::DailyPuzzle, days, events};

#[cfg(test)]
mod tests;

pub struct Day06;

impl DailyPuzzle<usize, u32> for Day06 {
    const YEAR: &'static str = events::YEAR_2024;
    const DAY: &'static str = days::DAY_06;

    fn solve_part_one(input: &str) -> usize {
        let (map, guard_position) = parse(input);
        
        predict_positions(&map, guard_position)
    }

    fn solve_part_two(input: &str) -> u32 {
        todo!()
    }
}

fn parse(input: &str) -> (Vec<Vec<char>>, GuardPosition) {
    let lines = input.lines().collect::<Vec<&str>>();

    let mut guard_initial_pos = GuardPosition::default();
    let mut map = vec![vec![' '; lines[0].len()]; lines.len()];

    for (row, line) in lines.iter().enumerate() {
        for (col, c) in line.chars().enumerate() {
            if c == '^' {
                guard_initial_pos = GuardPosition {
                    row,
                    col,
                    dir: Direction::Up,
                };
            } else if c == '>' {
                guard_initial_pos = GuardPosition {
                    row,
                    col,
                    dir: Direction::Right,
                };
                map[row][col] = '.';
            } else if c == 'v' {
                guard_initial_pos = GuardPosition {
                    row,
                    col,
                    dir: Direction::Down,
                };
            } else if c == '<' {
                guard_initial_pos = GuardPosition {
                    row,
                    col,
                    dir: Direction::Left,
                };
            }

            map[row][col] = c;
        }
    }

    (map, guard_initial_pos)
}

fn predict_positions(map: &Vec<Vec<char>>, mut guard_position: GuardPosition) -> usize {
    let mut positions: HashSet<(usize, usize)> = HashSet::new();

    loop {
        if (guard_position.dir == Direction::Up && guard_position.row as isize - 1 < 0)
            || (guard_position.dir == Direction::Right && guard_position.col + 1 >= map[0].len())
            || (guard_position.dir == Direction::Down && guard_position.row + 1 >= map.len())
            || (guard_position.dir == Direction::Left && guard_position.col as isize - 1 < 0)
        {
            break;
        }

        if (guard_position.dir == Direction::Up
            && map[guard_position.row - 1][guard_position.col].is_obstacle())
            || (guard_position.dir == Direction::Right
                && map[guard_position.row][guard_position.col + 1].is_obstacle())
            || (guard_position.dir == Direction::Down
                && map[guard_position.row + 1][guard_position.col].is_obstacle())
            || (guard_position.dir == Direction::Left
                && map[guard_position.row][guard_position.col - 1].is_obstacle())
        {
            guard_position = GuardPosition {
                row: guard_position.row,
                col: guard_position.col,
                dir: match guard_position.dir {
                    Direction::Up => Direction::Right,
                    Direction::Right => Direction::Down,
                    Direction::Down => Direction::Left,
                    Direction::Left => Direction::Up,
                },
            }
        }

        guard_position = match guard_position.dir {
            Direction::Up => GuardPosition {
                row: guard_position.row - 1,
                col: guard_position.col,
                dir: Direction::Up,
            },
            Direction::Right => GuardPosition {
                row: guard_position.row,
                col: guard_position.col + 1,
                dir: Direction::Right,
            },
            Direction::Down => GuardPosition {
                row: guard_position.row + 1,
                col: guard_position.col,
                dir: Direction::Down,
            },
            Direction::Left => GuardPosition {
                row: guard_position.row,
                col: guard_position.col - 1,
                dir: Direction::Left,
            },
        };

        positions.insert((guard_position.row, guard_position.col));
    }

    positions.len()
}

#[derive(Debug, Default)]
struct GuardPosition {
    row: usize,
    col: usize,
    dir: Direction,
}

#[derive(PartialEq)]
enum Direction {
    Up,
    Right,
    Down,
    Left,
}

impl Debug for Direction {
    fn fmt(&self, f: &mut std::fmt::Formatter<'_>) -> std::fmt::Result {
        match self {
            Self::Up => write!(f, "Up"),
            Self::Right => write!(f, "Right"),
            Self::Down => write!(f, "Down"),
            Self::Left => write!(f, "Left"),
        }
    }
}

impl Default for Direction {
    fn default() -> Self {
        Direction::Up
    }
}

trait IsObstacle {
    fn is_obstacle(&self) -> bool;
}

impl IsObstacle for char {
    fn is_obstacle(&self) -> bool {
        *self == '#'
    }
}
