namespace AdventOfCode._2023;

public class DayEight : ExerciseBase
{
    private readonly Map _map;

    public DayEight() : base(2023, 8)
    {
        _map = new Map(Input);
    }

    [Test]
    public override void PartOne()
    {
        Console.WriteLine($"Day Eight, Part One Answer: {_map.PartOne("AAA", "ZZZ")}");
    }

    [Test]
    public override void PartTwo()
    {
        Console.WriteLine($"Day Eight, Part Two Answer: {_map.PartTwo()}");
    }

    private class Map
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
                currentValue = FetchValue(stepCount, currentValue);

                stepCount++;
            } while (currentValue != end);

            return stepCount;
        }

        public long PartTwo()
        {
            var stepCounts = new List<long>();

            foreach (var node in _nodes.Keys.Where(k => k.EndsWith('A')))
            {
                long stepCount = 0;
                var currentValue = node;

                do
                {
                    currentValue = FetchValue(stepCount, currentValue);

                    stepCount++;
                } while (!currentValue.EndsWith('Z'));

                stepCounts.Add(stepCount);
            }

            return stepCounts.Aggregate(1L, FindLcm);

            long FindLcm(long a, long b) => a * b / FindGcd(a, b);

            static long FindGcd(long a, long b)
            {
                if (a == 0 || b == 0) return Math.Max(a, b);
                return a % b == 0 ? b : FindGcd(b, a % b);
            }
        }

        private string FetchValue(long stepCount, string currentValue)
        {
            var tempInstructionIndex = stepCount % _instructions.Length;

            currentValue = _instructions[tempInstructionIndex] == 0
                ? _nodes[currentValue].left
                : _nodes[currentValue].right;
            return currentValue;
        }
    }
}