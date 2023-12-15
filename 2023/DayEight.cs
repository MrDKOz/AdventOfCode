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
        Console.WriteLine($"Day Eight, Part One Answer: {_map?.PartOne()}");
    }

    [Test]
    public void PartTwo()
    {
        Console.WriteLine($"Day Eight, Part Two Answer:");
    }
}

public class Map
{
    private NodePart[] _instructions = null!;
    private Dictionary<string, NodeValues> _nodes;
    private string _startingLabel = string.Empty;

    public Map(List<string> input)
    {
        ProcessInput(input);
    }

    private void ProcessInput(List<string> input)
    {
        _nodes = new Dictionary<string, NodeValues>(input.Count);

        foreach (var line in input)
        {
            if (line == input.First())
            {
                ProcessInstructions(line);
            }
            else
            {
                ProcessNodes(line);
            }
        }

        return;

        void ProcessInstructions(string line)
        {
            var length = line.Length;
            _instructions = new NodePart[length];

            for (var i = 0; i < length; i++)
            {
                _instructions[i] = line[i] == 'L'
                    ? NodePart.Left
                    : NodePart.Right;
            }
        }

        void ProcessNodes(string line)
        {
            if (string.IsNullOrEmpty(line)) return;

            if (_startingLabel == string.Empty) _startingLabel = GetNodePart(NodePart.Label, line);

            _nodes.Add(GetNodePart(NodePart.Label, line), new NodeValues(GetNodePart(NodePart.Left, line), GetNodePart(NodePart.Right, line)));
        }

        string GetNodePart(NodePart part, string fetchFrom) =>
            part switch
            {
                NodePart.Label => fetchFrom[..3],
                NodePart.Left => fetchFrom[7..10],
                NodePart.Right => fetchFrom[12..15],
                _ => throw new Exception($"No part found for {part}")
            };
    }

    public long PartOne()
    {
        long stepCount = 0;
        var currentValue = _startingLabel;
        var target = new HashSet<string> { "ZZZ" };

        while (true)
        {
            foreach (var instruction in _instructions)
            {
                if (target.Contains(currentValue)) return stepCount;

                currentValue = GetNodeSide(currentValue, instruction);

                stepCount++;
            }
        }
    }

    private string GetNodeSide(string label, NodePart part)
    {
        if (_nodes.TryGetValue(label, out var nodeValues))
        {
            return part == NodePart.Left
                ? nodeValues.LeftSide
                : nodeValues.RightSide;
        }

        throw new Exception($"Node not found with label {label}");
    }

    private record NodeValues(string LeftSide, string RightSide);
    
    private enum NodePart
    {
        Label,
        Left,
        Right
    }
}