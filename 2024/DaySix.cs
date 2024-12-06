using System.ComponentModel;

namespace AdventOfCode._2024;

public class DaySix : ExerciseBase
{
    private readonly GuardRouting _guardRouting;

    public DaySix() : base(2024, 6)
    {
        _guardRouting = new GuardRouting(Input);
    }

    [Test]
    public override void PartOne()
    {
        Console.WriteLine($"Day Six, Part One Answer: {_guardRouting.StepOne()}");
    }

    [Test]
    public override void PartTwo()
    {
        throw new NotImplementedException();
    }

    private class GuardRouting
    {
        private char[,] _map;
        private (int row, int col, char heading) _currentPosition;

        private int rows;
        private int cols;

        public GuardRouting(IReadOnlyList<string> input)
        {
            rows = input.Count;
            cols = input[0].Length;

            _map = new char[rows, cols];

            for (var row = 0; row < rows; row++)
            {
                for (var col = 0; col < cols; col++)
                {
                    var value = input[row][col];

                    if (value == '^')
                    {
                        _currentPosition = (row, col, 'N');
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

            for (var row = 0; row < rows; row++)
            {
                for (var col = 0; col < cols; col++)
                {
                    result += _map[row, col] != '.' && _map[row, col] != '#' ? 1 : 0;
                }
            }

            return result;
        }

        private void Step()
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

            MarkAsVisited();
            _currentPosition = newPosition;
            MarkAsCurrent();
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

        private bool Blocked() =>
            _currentPosition.heading switch
            {
                'N' => GetAtCoord(_currentPosition.row - 1, _currentPosition.col) == '#',
                'E' => GetAtCoord(_currentPosition.row, _currentPosition.col + 1) == '#',
                'S' => GetAtCoord(_currentPosition.row + 1, _currentPosition.col) == '#',
                'W' => GetAtCoord(_currentPosition.row, _currentPosition.col - 1) == '#',
                _ => throw new Exception($"Heading of '{_currentPosition.heading}' is not valid.")
            };

        private char GetAtCoord(int row, int col) => _map[row, col];

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