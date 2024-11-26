pub trait DailyPuzzle<R1, R2> {
    const YEAR: &'static str;
    const DAY: &'static str;

    fn solve_part_one(input: &str) -> R1;
    fn solve_part_two(input: &str) -> R2;
}