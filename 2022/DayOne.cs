using AdventOfCode.Helpers;

namespace AdventOfCode._2022;

public class Expedition
{
    public List<Elf> Elves { get; } = new();
}

public class Elf
{
    public void AddItem(string calories) => Calories.Add(Convert.ToInt32(calories));
    public List<int> Calories { get; } = new();
    public int SumOfCalories => Calories.Sum();
}

/// <summary>
/// Puzzle link: https://adventofcode.com/2022/day/1
/// </summary>
public class DayOne
{
    private Expedition? _expedition;
    
    [SetUp]
    public void Setup()
    {
        _expedition = BuildExpedition(PuzzleInput.Load(2022, 1));
    }

    private static Expedition BuildExpedition(List<string> input)
    {
        var expedition = new Expedition();
        var tempElf = new Elf();

        foreach (var line in input)
        {
            if (string.IsNullOrEmpty(line))
            {
                expedition.Elves.Add(tempElf);
                tempElf = new Elf();
            }
            else
            {
                tempElf.AddItem(line);
            }
        }

        if (tempElf.Calories.Any())
        {
            expedition.Elves.Add(tempElf);
        }

        return expedition;
    }

    [Test]
    public void PartOne()
    {
        var sum = _expedition!.Elves.Max(e => e.SumOfCalories);
        
        Console.WriteLine($"Answer: {sum}");
    }

    [Test]
    public void PartTwo()
    {
        var sum = _expedition!.Elves
            .OrderByDescending(e => e.SumOfCalories)
            .Take(3)
            .Sum(e => e.SumOfCalories);
        
        Console.WriteLine($"Answer: {sum}");
    }
}