use crate::shared::{daily_puzzle::DailyPuzzle, days, events};

#[cfg(test)]
mod tests;

pub struct Day04;

impl DailyPuzzle<u32, u32> for Day04 {
    const YEAR: &'static str = events::YEAR_2024;
    const DAY: &'static str = days::DAY_04;

    fn solve_part_one(input: &str) -> u32 {
        let word_search = parse(input);

        count_xmas(&word_search)
    }

    fn solve_part_two(input: &str) -> u32 {
        let word_search = parse(input);

        count_x_mas(&word_search)
    }
}

fn parse(input: &str) -> Vec<Vec<char>> {
    let lines = input.lines().collect::<Vec<&str>>();
    let mut word_search = vec![vec![' '; lines[0].len()]; lines.len()];

    for (row, line) in lines.iter().enumerate() {
        for (col, c) in line.chars().enumerate() {
            word_search[row][col] = c;
        }
    }

    word_search
}

fn count_xmas(word_search: &Vec<Vec<char>>) -> u32 {
    let mut count = 0;

    let rows_count = word_search.len();
    for row in 0..rows_count {
        let cols_count = word_search[row].len();
        for col in 0..cols_count {
            if word_search[row][col] != 'X' {
                continue;
            }

            // hor left->right
            if col as isize + 3 < cols_count as isize
                && word_search[row][col + 1] == 'M'
                && word_search[row][col + 2] == 'A'
                && word_search[row][col + 3] == 'S'
            {
                count += 1;
            }

            // hor right->left
            if col as isize - 3 >= 0
                && word_search[row][col - 1] == 'M'
                && word_search[row][col - 2] == 'A'
                && word_search[row][col - 3] == 'S'
            {
                count += 1;
            }

            // ver up->down
            if row as isize + 3 < rows_count as isize
                && word_search[row + 1][col] == 'M'
                && word_search[row + 2][col] == 'A'
                && word_search[row + 3][col] == 'S'
            {
                count += 1;
            }

            // ver down->up
            if row as isize - 3 >= 0
                && word_search[row - 1][col] == 'M'
                && word_search[row - 2][col] == 'A'
                && word_search[row - 3][col] == 'S'
            {
                count += 1;
            }

            // dia down left->up right
            if col as isize + 3 < cols_count as isize
                && row as isize - 3 >= 0
                && word_search[row - 1][col + 1] == 'M'
                && word_search[row - 2][col + 2] == 'A'
                && word_search[row - 3][col + 3] == 'S'
            {
                count += 1;
            }

            // dia up left->down right
            if col as isize + 3 < cols_count as isize
                && row as isize + 3 < rows_count as isize
                && word_search[row + 1][col + 1] == 'M'
                && word_search[row + 2][col + 2] == 'A'
                && word_search[row + 3][col + 3] == 'S'
            {
                count += 1;
            }

            // dia down right->up left
            if col as isize - 3 >= 0
                && row as isize - 3 >= 0
                && word_search[row - 1][col - 1] == 'M'
                && word_search[row - 2][col - 2] == 'A'
                && word_search[row - 3][col - 3] == 'S'
            {
                count += 1;
            }

            // dia up right->down left
            if col as isize - 3 >= 0
                && row as isize + 3 < rows_count as isize
                && word_search[row + 1][col - 1] == 'M'
                && word_search[row + 2][col - 2] == 'A'
                && word_search[row + 3][col - 3] == 'S'
            {
                count += 1;
            }
        }
    }

    count
}

fn count_x_mas(word_search: &Vec<Vec<char>>) -> u32 {
    let mut count = 0;

    let rows_count = word_search.len();
    for row in 0..rows_count {
        let cols_count = word_search[row].len();
        for col in 0..cols_count {
            if word_search[row][col] != 'A' {
                continue;
            }

            // the whole square needs to be in the matrix
            if row as isize - 1 < 0
                || row as isize + 1 >= rows_count as isize
                || col as isize - 1 < 0
                || col as isize + 1 >= cols_count as isize
            {
                continue;
            }

            // check diagonals
            // (-1,-1) should be M or S and (1,1) the opposite
            // (1,-1) should be M or S and (-1,1) the opposite
            if ((word_search[row - 1][col - 1] == 'M' && word_search[row + 1][col + 1] == 'S')
                || (word_search[row - 1][col - 1] == 'S' && word_search[row + 1][col + 1] == 'M'))
                && ((word_search[row + 1][col - 1] == 'M' && word_search[row - 1][col + 1] == 'S')
                    || (word_search[row + 1][col - 1] == 'S'
                        && word_search[row - 1][col + 1] == 'M'))
            {
                count += 1;
            }
        }
    }

    count
}
