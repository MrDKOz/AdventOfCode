using AdventOfCode.Helpers;

namespace AdventOfCode._2023;

public class DayEight
{
    private Map? _map;

    [SetUp]
    public void Setup()
    {
        _map = new Map(PuzzleInput.Load(2023, 8));
    }

    [Test]
    public void PartOne()
    {
        Console.WriteLine($"Day Eight, Part One Answer: {_map?.PartOne("AAA", "ZZZ")}");
    }

    [Test]
    public void PartTwo()
    {
        Console.WriteLine($"Day Eight, Part Two Answer:");
    }
}

public class Map
{
    private readonly int[] _instructions;
    private readonly Dictionary<string, (string left, string right)> _nodes;

    public Map(IReadOnlyList<string> input)
    {
        _instructions = input[0].Trim().Select(i => i == 'L' ? 0 : 1).ToArray();
        _nodes = new Dictionary<string, (string left, string right)>(input.Count);

        for (var i = 2; i < input.Count; i++)
        {
            var line = input[i];
            _nodes.Add(line.Substring(0, 3), (line.Substring(7, 3), line.Substring(12, 3)));
        }
    }

    public long PartOne(string start, string end)
    {
        long stepCount = 0;
        var currentValue = start;

        do
        {
            var tempInstructionIndex = stepCount % _instructions.Length;

            currentValue = _instructions[tempInstructionIndex] == 0
                ? _nodes[currentValue].left
                : _nodes[currentValue].right;

            stepCount++;
        } while (currentValue != end);

        return stepCount;
    }
}