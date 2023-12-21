namespace AdventOfCode._2022;

/// <summary>
/// Puzzle link: https://adventofcode.com/2022/day/3
/// </summary>
public class DayThree : ExerciseBase
{
    private readonly IEnumerable<string> _puzzleInput;
    private List<RuckSack> _ruckSacks = new();

    public DayThree() : base(2022, 3)
    {
        _puzzleInput = Input;
    }
    
    [SetUp]
    public void SetUp() => _ruckSacks = _puzzleInput.Select(rucksack => new RuckSack(rucksack)).ToList();
    
    [Test]
    public override void PartOne()
    {
        var commonItemPriority = _ruckSacks.Sum(ruckSack => ruckSack.CommonItemPriority);
        
        Console.WriteLine($"Answer: {commonItemPriority}");
    }

    [Test]
    public override void PartTwo()
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

    private class RuckSack
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
}