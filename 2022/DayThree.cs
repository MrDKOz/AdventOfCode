using AdventOfCode.Helpers;

namespace AdventOfCode._2022;

internal class RuckSack
{
    private string AllItems { get; }
    private int ItemCount => AllItems.Length;
    private int CompartmentSize => ItemCount / 2;
    private string CompartmentOne => AllItems[..CompartmentSize];
    private string CompartmentTwo => AllItems[CompartmentSize..];
    private char CommonItem => CompartmentOne.Intersect(CompartmentTwo).Single();
    public int CommonItemPriority => FetchPriority(CommonItem);
    private static int FetchPriority(char character) => character % 32 + (char.IsUpper(character) ? 26 : 0);

    public RuckSack(string allItems)
    {
        AllItems = allItems;
    }
}

/// <summary>
/// Puzzle link: https://adventofcode.com/2022/day/3
/// </summary>
public class DayThree
{
    private readonly List<string> _puzzleInput = PuzzleInput.Load(2022, 3);
    private List<RuckSack> _ruckSacks = new();

    [SetUp]
    public void SetUp() => _ruckSacks = _puzzleInput.Select(rucksack => new RuckSack(rucksack)).ToList();
    
    [Test]
    public void PartOne()
    {
        var commonItemPriority = _ruckSacks.Sum(ruckSack => ruckSack.CommonItemPriority);
        
        Console.WriteLine($"Answer: {commonItemPriority}");
    }

    [Test]
    public void PartTwo()
    {
        var commonItemSum = _puzzleInput
            .Chunk(3)
            .Select(t => t[0].Intersect(t[1].Intersect(t[2])).Single())
            .Select(FetchPriority)
            .Sum();
        
        Console.WriteLine($"Answer: {commonItemSum}");
        return;

        int FetchPriority(char character) => character % 32 + (Char.IsUpper(character) ? 26 : 0);
    }
}