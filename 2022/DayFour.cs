﻿namespace AdventOfCode._2022;

/// <summary>
/// Puzzle link: https://adventofcode.com/2022/day/4
/// </summary>
public class DayFour : ExerciseBase
{
    private static IEnumerable<string> _puzzleInput = null!;
    private readonly List<Assignment> _assignments = _puzzleInput
        .Select(assignments => new Assignment(assignments))
        .ToList();

    public DayFour() : base(2022, 4)
    {
        _puzzleInput = Input;
    }
    
    [Test]
    public override void PartOne()
    {
        var totalCount = _assignments.Count(a => a.FullOverlap);

        Console.WriteLine($"Answer: {totalCount}");
    }

    [Test]
    public override void PartTwo()
    {
        var totalCount = _assignments.Count(a => a.PartialOverlap);

        Console.WriteLine($"Answer: {totalCount}");
    }

    private class Assignment
    {
        public Assignment(string input)
        {
            var split = input.Split(',');
            var firstSplit = split[0].Split('-');
            var secondSplit = split[1].Split('-');

            First = (int.Parse(firstSplit[0]), int.Parse(firstSplit[1]));
            Second = (int.Parse(secondSplit[0]), int.Parse(secondSplit[1]));
        }

        private (int Start, int End) First { get; }
        private (int Start, int End) Second { get; }
        public bool FullOverlap => (First.Start >= Second.Start && First.End <= Second.End) || (Second.Start >= First.Start && Second.End <= First.End);
        public bool PartialOverlap => (First.Start >= Second.Start && First.Start <= Second.End) || (Second.Start >= First.Start && Second.Start <= First.End);
    }
}