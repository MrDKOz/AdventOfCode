using System.Numerics;

namespace AdventOfCode._2023;

public class DayTen : ExerciseBase
{
    private PipeNetwork _pipeNetwork;

    public DayTen() : base(2023, 10)
    {
        _pipeNetwork = new(Input);
    }

    [Test]
    public override void PartOne()
    {
        _pipeNetwork.GeneratePath();
        Console.WriteLine($"Day Ten, Part One Answer:");
    }

    [Test]
    public override void PartTwo()
    {
        Console.WriteLine($"Day Ten, Part Two Answer:");
    }

    private class PipeNetwork
    {
        private Pipe[,] _pipes = null!;
        private (int y, int x) _startingLocation;
        
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
                    var inputChar = input[y][x];

                    if (inputChar == 'S') _startingLocation = (y, x);

                    _pipes[y, x] = new Pipe(inputChar, (y, x));
                }
            }
        }

        public void GeneratePath()
        {
            var found = new Stack<Pipe>();
            var currentDistance = 0;

            found.Push(GetPipeAtLocation(_startingLocation.y, _startingLocation.x));

            do
            {
                var currentPipe = found.Pop();
                if (currentDistance > 0 && currentPipe.Location == _startingLocation) break;
                currentPipe.SetDistance(currentDistance++);

                FindValidDirectionsFromPoint(currentPipe);
            } while (found.Count > 0);

            return;

            void FindValidDirectionsFromPoint(Pipe pipe)
            {
                foreach (var surroundingPipe in GetValidSurroundingPipes(pipe.Location.y, pipe.Location.x))
                {
                    found!.Push(surroundingPipe);
                }
            }
        }

        private Pipe GetPipeAtDirection(Directions direction, int y, int x) =>
            direction switch
            {
                Directions.North => _pipes[y - 1, x],
                Directions.East => _pipes[y, x + 1],
                Directions.South => _pipes[y + 1, x],
                Directions.West => _pipes[y, x - 1],
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        
        private Pipe GetPipeAtLocation(int y, int x) => _pipes[y, x];
        
        private IEnumerable<Pipe> GetValidSurroundingPipes(int y, int x)
        {
            foreach (var direction in _pipes[y, x].Connections)
            {
                var pipeToCheck = GetPipeAtDirection(direction, y, x);
                if (pipeToCheck.AcceptsConnectionFrom(direction))
                {
                    yield return pipeToCheck;
                }
            }
        }

        private class Pipe
        {
            public string Description;
            public readonly List<Directions> Connections;
            public readonly (int y, int x) Location;
            public int Step;

            public Pipe(char character, (int y, int x) location)
            {
                (Description, Connections) = CharToPipe(character);
                Location = location;
            }
            
            public void SetDistance(int step) => Step = step;

            private (string description, List<Directions> directions) CharToPipe(char character) =>
                character switch
                {
                    '|' => ("North <-> South", new List<Directions>{Directions.North, Directions.South}),
                    '-' => ("East <-> West", new List<Directions>{Directions.East, Directions.West}),
                    'L' => ("North <-> East", new List<Directions>{Directions.North, Directions.East}),
                    'J' => ("North <-> West", new List<Directions>{Directions.North, Directions.West}),
                    '7' => ("South <-> West", new List<Directions>{Directions.South, Directions.West}),
                    'F' => ("South <-> East", new List<Directions>{Directions.South, Directions.East}),
                    '.' => ("No connections", new List<Directions>()),
                    'S' => ("Starting location", new List<Directions>{Directions.North, Directions.East, Directions.South, Directions.West}),
                    _ => throw new Exception($"Unknown character: {character}")
                };

            public bool AcceptsConnectionFrom(Directions directionToAccept) =>
                directionToAccept switch
                {
                    Directions.North => Connections.Contains(Directions.South),
                    Directions.East => Connections.Contains(Directions.West),
                    Directions.South => Connections.Contains(Directions.North),
                    Directions.West => Connections.Contains(Directions.East),
                    _ => throw new ArgumentOutOfRangeException(nameof(directionToAccept), directionToAccept, null)
                };
        }

        private enum Directions
        {
            North,
            East,
            South,
            West
        }
    }
}