using System.Numerics;
using AdventOfCode.Helpers;

namespace AdventOfCode._2023;

public class DayThree
{
    private Engine? _engine;

    [SetUp]
    public void Setup()
    {
        _engine = new Engine(PuzzleInput.Load(2023, 3));
    }

    [Test]
    public void PartOne()
    {
        Console.WriteLine($"Day Two, Part One Answer: {_engine?.SumOfValidPartNumbers}");
    }

    [Test]
    public void PartTwo()
    {
        Console.WriteLine($"Day Two, Part Two Answer: ");
    }
}

public class Engine
{
    private readonly char[,] _schematics;
    public List<int> ValidPartNumbers { get; } = new();
    public List<int> InvalidValidPartNumbers { get; } = new();
    public int SumOfValidPartNumbers => ValidPartNumbers.Sum();
    public int SumOfInvalidValidPartNumbers => InvalidValidPartNumbers.Sum();
    private HashSet<Vector2> Gears { get; } = new();

    public Engine(IReadOnlyList<string> schematics)
    {
        _schematics = new char[schematics.Count, schematics[0].Length];

        BuildArray(schematics);

        //PrintOutSchematics();
        FetchPartNumbers();
    }

    private void BuildArray(IReadOnlyList<string> schematics)
    {
        for (var i = 0; i < schematics.Count; i++)
        {
            for (var j = 0; j < schematics[i].Length; j++)
            {
                _schematics[i, j] = schematics[i][j];
            }
        }

        Console.WriteLine(
            $"Completed building array of size {_schematics.GetLength(0)}x{_schematics.GetLength(1)} (HxW).");
    }

    private void PrintOutSchematics()
    {
        for (var i = 0; i < _schematics.GetLength(0); i++)
        {
            for (var j = 0; j < _schematics.GetLength(1); j++)
            {
                Console.Write(_schematics[i, j]);
            }

            Console.WriteLine();
        }
    }

    private void FetchPartNumbers()
    {
        var tmpPartNumber = string.Empty;
        var tmpPartNumberValidated = false;

        for (var i = 0; i < _schematics.GetLength(0); i++)
        {
            for (var j = 0; j < _schematics.GetLength(1); j++)
            {
                if (char.IsDigit(_schematics[i, j]))
                {
                    tmpPartNumber += _schematics[i, j];

                    if (!tmpPartNumberValidated)
                    {
                        tmpPartNumberValidated = IsPartNumber(i, j);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(tmpPartNumber))
                    {
                        if (tmpPartNumberValidated)
                        {
                            ValidPartNumbers.Add(Convert.ToInt32(tmpPartNumber));
                            //Console.WriteLine($"✅ {tmpPartNumber} [{i},{j}]");
                        }
                        else
                        {
                            InvalidValidPartNumbers.Add(Convert.ToInt32(tmpPartNumber));
                            //Console.WriteLine($"⛔ {tmpPartNumber} [{i},{j}]");
                        }
                        
                        tmpPartNumber = string.Empty;
                        tmpPartNumberValidated = false;
                    }
                }
            }
        }

        return;

        bool IsPartNumber(int i, int j)
        {
            return CheckSurroundings(CheckIfValid, i, j);

            bool CheckIfValid(int tmpI, int tmpJ)
            {
                if (tmpI < 0 || tmpI >= _schematics.GetLength(0)) return false;
                if (tmpJ < 0 || tmpJ >= _schematics.GetLength(1)) return false;
                if (char.IsDigit(_schematics[tmpI, tmpJ])) return false;
                switch (_schematics[tmpI, tmpJ])
                {
                    case '.':
                        return false;
                    case '*':
                        Gears.Add(new Vector2(tmpI, tmpJ));
                        break;
                }

                return true;
            }
        }
    }

    private bool CheckSurroundings(Func<int, int, bool> validate, int i, int j)
    {
        foreach (Directions direction in Enum.GetValues(typeof(Directions)))
        {
            var isValid = direction switch
            {
                Directions.North => validate(i - 1, j),
                Directions.NorthEast => validate(i - 1, j + 1),
                Directions.East => validate(i, j + 1),
                Directions.SouthEast => validate(i + 1, j + 1),
                Directions.South => validate(i + 1, j),
                Directions.SouthWest => validate(i + 1, j - 1),
                Directions.West => validate(i, j - 1),
                Directions.NorthWest => validate(i - 1, j - 1),
                _ => throw new ArgumentOutOfRangeException()
            };

            if (isValid)
            {
                return true;
            }
        }

        return false;
    }

    enum Directions
    {
        North,
        NorthEast,
        East,
        SouthEast,
        South,
        SouthWest,
        West,
        NorthWest
    }
}