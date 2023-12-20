namespace AdventOfCode._2023;

public class DayTen
{
    private PipeNetwork _pipeNetwork = new(PuzzleInput.Load(2023, 10));

    [Test]
    public void PartOne()
    {
        Console.WriteLine($"Day Ten, Part One Answer:");
    }

    [Test]
    public void PartTwo()
    {
        Console.WriteLine($"Day Ten, Part Two Answer:");
    }

    private class PipeNetwork
    {
        private Pipe[,] _pipes = null!;
        
        public PipeNetwork(IReadOnlyList<string> input)
        {
            BuildPipeNetwork(input);
        }

        private void BuildPipeNetwork(IReadOnlyList<string> input)
        {
            _pipes = new Pipe[input.Count, input[0].Length];

            for (var y = 0; y < input.Count; y++)
            {
                for (var x = 0; x < input[y].Length; x++)
                {
                    _pipes[y, x] = new Pipe(input[y][x]);
                }
            }
        }

        private class Pipe
        {
            public string Description;
            public PipeConnections Connections;

            public Pipe(char character)
            {
                (Description, Connections) = CharToPipe(character);
            }

            private (string Description, PipeConnections Connections) CharToPipe(char character) =>
                character switch
                {                                                 // North  East   South  West
                    '|' => ("North <-> South",   new PipeConnections(true,  false, true,  false)),
                    '-' => ("East <-> West",     new PipeConnections(false, true,  false, true)),
                    'L' => ("North <-> East",    new PipeConnections(true,  true,  false, false)),
                    'J' => ("North <-> West",    new PipeConnections(true,  false, false, true)),
                    '7' => ("South <-> West",    new PipeConnections(false, false, true,  true)),
                    'F' => ("South <-> East",    new PipeConnections(false, true,  true,  false)),
                    '.' => ("No connections",    new PipeConnections(false, false, false, false)),
                    'S' => ("Starting location", new PipeConnections(true,  true,  true,  true)),
                    _ => throw new Exception($"Unknown pipe character: {character}")
                };
        }
        
        private class PipeConnections
        {
            public bool North { get; init; }
            public bool East { get; init; }
            public bool South { get; init; }
            public bool West { get; init; }

            public PipeConnections(bool north, bool east, bool south, bool west)
            {
                North = north;
                East = east;
                South = south;
                West = west;
            }
            
            public bool CanConnectTo(PipeConnections connections)
            {
                if (North && connections.South) return true;
                return East && connections.West;
            }
        }
    }
}