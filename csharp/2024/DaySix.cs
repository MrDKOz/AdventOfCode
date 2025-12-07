namespace AdventOfCode._2024;

public class DaySix : ExerciseBase
{
    private readonly GuardRouting _guardRouting;

    public DaySix() : base(2024, 6)
    {
        _guardRouting = new GuardRouting(Input);
    }

    [Test, Description("Answer: 5453")]
    public override void PartOne() => Console.WriteLine($"Day Six, Part One Answer: {_guardRouting.StepOne()}");

    [Test, Description("Answer: 2188")]
    public override void PartTwo() => Console.WriteLine($"Day Six, Part Two Answer: {_guardRouting.StepTwo()}");

    private class GuardRouting
    {
        private readonly char[,] _map;
        private char[,] _testMap;
        private (int row, int col, char heading) _currentPosition;
        private readonly (int row, int col, char heading) _startingPosition;

        private readonly int _rows;
        private readonly int _cols;

        public GuardRouting(IReadOnlyList<string> input)
        {
            _rows = input.Count;
            _cols = input[0].Length;

            _map = new char[_rows, _cols];

            for (var row = 0; row < _rows; row++)
            {
                for (var col = 0; col < _cols; col++)
                {
                    var value = input[row][col];

                    if (value == '^')
                    {
                        _currentPosition = (row, col, 'N');
                        _startingPosition = (row, col, 'N');
                        Console.WriteLine($"Starting position: R:{row}, C:{col}");
                    }

                    _map[row, col] = value;
                }
            }
        }

        public int StepOne()
        {
            while (!LeavingArea())
            {
                if (Blocked())
                {
                    TurnRight();
                    continue;
                }

                Step();
            }

            var result = 0;

            for (var row = 0; row < _rows; row++)
            {
                for (var col = 0; col < _cols; col++)
                {
                    result += _map[row, col] != '.' && _map[row, col] != '#' ? 1 : 0;
                }
            }

            return result;
        }

        public int StepTwo()
        {
            var result = 0;

            for (var row = 0; row < _rows; row++)
            {
                for (var col = 0; col < _cols; col++)
                {
                    if (GetAtCoord(row, col) == '#') continue;

                    _currentPosition = _startingPosition;
                    
                    _testMap = (char[,])_map.Clone();
                    _testMap[row, col] = '#';

                    result += LoopDetected() ? 1 : 0;
                }
            }

            return result;
        }

        private bool LoopDetected()
        {
            var positionHistory = new HashSet<(int row, int col, int heading)>();
            
            while (!LeavingArea())
            {
                var currentState = (_currentPosition.row, _currentPosition.col, _currentPosition.heading);

                if (!positionHistory.Add(currentState))
                    return true;

                if (Blocked(true))
                {
                    TurnRight();
                    continue;
                }

                Step(true);
            }
            
            return false;
        }

        private void Step(bool useTestMap = false)
        {
            var newPosition = _currentPosition;

            switch (_currentPosition.heading)
            {
                case 'N':
                    newPosition.row -= 1;
                    break;
                case 'E':
                    newPosition.col += 1;
                    break;
                case 'S':
                    newPosition.row += 1;
                    break;
                case 'W':
                    newPosition.col -= 1;
                    break;
            }

            if (!useTestMap) MarkAsVisited();
            _currentPosition = newPosition;
            if (!useTestMap) MarkAsCurrent();
        }

        private void MarkAsCurrent() => _map[_currentPosition.row, _currentPosition.col] = _currentPosition.heading;

        private void MarkAsVisited() => _map[_currentPosition.row, _currentPosition.col] = 'X';

        private void TurnRight() =>
            _currentPosition.heading = _currentPosition.heading switch
            {
                'N' => 'E',
                'E' => 'S',
                'S' => 'W',
                'W' => 'N',
                _ => throw new Exception($"Heading of '{_currentPosition.heading}' is not valid.")
            };

        private bool Blocked(bool useTestMap = false) =>
            _currentPosition.heading switch
            {
                'N' => GetAtCoord(_currentPosition.row - 1, _currentPosition.col, useTestMap) == '#',
                'E' => GetAtCoord(_currentPosition.row, _currentPosition.col + 1, useTestMap) == '#',
                'S' => GetAtCoord(_currentPosition.row + 1, _currentPosition.col, useTestMap) == '#',
                'W' => GetAtCoord(_currentPosition.row, _currentPosition.col - 1, useTestMap) == '#',
                _ => throw new Exception($"Heading of '{_currentPosition.heading}' is not valid.")
            };

        private char GetAtCoord(int row, int col, bool useTestMap = false) =>
            useTestMap
                ? _testMap[row, col]
                : _map[row, col];

        private bool LeavingArea() =>
            _currentPosition.heading switch
            {
                'N' => _currentPosition.row - 1 < 0,
                'E' => _currentPosition.col + 1 >= _map.GetLength(1),
                'S' => _currentPosition.row + 1 >= _map.GetLength(0),
                'W' => _currentPosition.col - 1 < 0,
                _ => throw new Exception($"Heading of '{_currentPosition.heading}' is not valid.")
            };
    }
}