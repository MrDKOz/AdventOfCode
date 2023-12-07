using System.Numerics;
using System.Text;
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
        Console.WriteLine($"Day Three, Part One Answer: {_engine?.PartNumbers.SumOfValidPartNumbers}");
    }

    [Test]
    public void PartTwo()
    {
        Console.WriteLine($"Day Three, Part Two Answer: {_engine?.Gears.GearRatioSum}");
    }
}

public class Engine
{
    private readonly char[,] _schematics;
    public PartNumbers PartNumbers { get; } = new();
    public Gears Gears { get; } = new();

    public Engine(IReadOnlyList<string> schematics)
    {
        _schematics = new char[schematics.Count, schematics[0].Length];

        BuildArray(schematics);

        FetchPartNumbers();
        CalculateGearRatios();
        //PrintOutSchematics();
    }

    private void BuildArray(IReadOnlyList<string> schematics)
    {
        for (var x = 0; x < schematics.Count; x++)
        {
            for (var y = 0; y < schematics[x].Length; y++)
            {
                _schematics[x, y] = schematics[x][y];
            }
        }

        Console.WriteLine($"Completed building [{_schematics.GetLength(0)}h | {_schematics.GetLength(1)}w]");
    }

    private void PrintOutSchematics()
    {
        for (var x = 0; x < _schematics.GetLength(0); x++)
        {
            for (var y = 0; y < _schematics.GetLength(1); y++)
            {
                if (Gears.ValidGearExists(x, y))
                {
                    Console.Write("H");
                }
                else
                {
                    Console.Write(_schematics[x, y]);
                }
            }

            Console.WriteLine();
        }
    }

    private void FetchPartNumbers()
    {
        var partNumber = new StringBuilder();
        var partNumberValidated = false;

        for (var x = 0; x < _schematics.GetLength(0); x++)
        {
            for (var y = 0; y < _schematics.GetLength(1); y++)
            {
                if (ProcessPartNumber(x, y, out var number))
                {
                    partNumber.Append(number);

                    if (!partNumberValidated)
                    {
                        partNumberValidated = HasAdjacentSymbols(x, y);
                    }
                }
                else if (partNumber.Length > 0)
                {
                    var tmpPartNumber = partNumber.ToString();
                    var tmpX = y == 0 ? x - 1 : x;
                    var tmpY = y == 0 ? _schematics.GetLength(1) : y;
                    var tmpLength = partNumber.Length;
                    
                    PartNumbers.Add(tmpPartNumber, tmpX, tmpY, tmpLength, partNumberValidated);

                    partNumber.Clear();
                    partNumberValidated = false;
                }
            }
        }

        return;

        bool ProcessPartNumber(int x, int y, out string character)
        {
            var rawCharacter = _schematics[x, y];
            character = rawCharacter.ToString();

            return char.IsDigit(rawCharacter);
        }

        bool HasAdjacentSymbols(int x, int y)
        {
            foreach (Directions direction in Enum.GetValues(typeof(Directions)))
            {
                var isValid = direction switch
                {
                    Directions.North => IsASymbol(x - 1, y),
                    Directions.NorthEast => IsASymbol(x - 1, y + 1),
                    Directions.East => IsASymbol(x, y + 1),
                    Directions.SouthEast => IsASymbol(x + 1, y + 1),
                    Directions.South => IsASymbol(x + 1, y),
                    Directions.SouthWest => IsASymbol(x + 1, y - 1),
                    Directions.West => IsASymbol(x, y - 1),
                    Directions.NorthWest => IsASymbol(x - 1, y - 1),
                    _ => throw new Exception($"Unknown direction {direction}.")
                };

                if (isValid)
                {
                    return true;
                }
            }

            return false;

            bool IsASymbol(int x2, int y2)
            {
                if (!IsInBounds(x2, y2)) return false;
                if (char.IsDigit(_schematics[x2, y2])) return false;

                switch (_schematics[x2, y2])
                {
                    case '.':
                        return false;
                    case '*':
                        Gears.AddGear(x2, y2);
                        break;
                }

                return true;
            }
        }
    }

    private void CalculateGearRatios()
    {
        foreach (var validPartNumbers in PartNumbers.ValidPartNumbers)
        {
            var x = (int)validPartNumbers.Location.X;
            var y = (int)validPartNumbers.Location.Y;
            var loopEnd = y + validPartNumbers.Length;

            for (var j = y; j < loopEnd; j++)
            {
                if (!CheckForAdjacentGears(x, j, out var coords)) continue;

                Gears.FindAndAddRatio(coords, validPartNumbers.Number);

                break;
            }
        }

        return;

        bool CheckForAdjacentGears(int x, int y, out Vector2? coords)
        {
            foreach (Directions direction in Enum.GetValues(typeof(Directions)))
            {
                var isValid = direction switch
                {
                    Directions.North => HasAdjacentGear(x - 1, y, out coords),
                    Directions.NorthEast => HasAdjacentGear(x - 1, y + 1, out coords),
                    Directions.East => HasAdjacentGear(x, y + 1, out coords),
                    Directions.SouthEast => HasAdjacentGear(x + 1, y + 1, out coords),
                    Directions.South => HasAdjacentGear(x + 1, y, out coords),
                    Directions.SouthWest => HasAdjacentGear(x + 1, y - 1, out coords),
                    Directions.West => HasAdjacentGear(x, y - 1, out coords),
                    Directions.NorthWest => HasAdjacentGear(x - 1, y - 1, out coords),
                    _ => throw new Exception($"Unknown direction {direction}.")
                };

                if (isValid)
                {
                    return isValid;
                }
            }

            coords = null;
            return false;
        }

        bool HasAdjacentGear(int x, int y, out Vector2? coords)
        {
            coords = null;

            if (!IsInBounds(x, y)) return false;

            var hasGear = _schematics[x, y] == '*';
            if (hasGear)
            {
                coords = new Vector2(x, y);
            }

            return hasGear;
        }
    }

    private bool IsInBounds(int x, int y) =>
        x >= 0 && x < _schematics.GetLength(0) && y >= 0 && y < _schematics.GetLength(1);

    private enum Directions
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

public class PartNumbers
{
    public List<PartNumber> PartNumberList { get; } = new();
    public List<PartNumber> ValidPartNumbers => PartNumberList.Where(p => p.Valid).ToList();
    public int SumOfValidPartNumbers => ValidPartNumbers.Sum(p => p.Number);

    public void Add(string partNumber, int x, int y, int length, bool partNumberValidated)
    {
        var tmpLocation = new Vector2(x, y - length);

        if (tmpLocation.X < 0 || tmpLocation.Y < 0)
            throw new Exception($"Invalid location [{tmpLocation.X}x | {tmpLocation.Y}y].");
        
        PartNumberList.Add(new PartNumber
        {
            Number = Convert.ToInt32(partNumber),
            Location = tmpLocation,
            Length = length,
            Valid = partNumberValidated
        });
    }
}

public class PartNumber
{
    public int Number { get; init; }
    public Vector2 Location { get; init; }
    public int Length { get; init; }
    public bool Valid { get; init; }
}

public class Gears
{
    public List<Gear> GearsList { get; } = new();
    private IEnumerable<Gear> ValidGears => GearsList.Where(g => g.Valid).ToList();
    public int GearRatioSum => ValidGears.Sum(g => g.GearRatio);

    public void AddGear(int x, int y)
    {
        if (GearsList.Any(g => (int)g.Location.X == x && (int)g.Location.Y == y)) return;

        GearsList.Add(new Gear
        {
            Location = new Vector2(x, y)
        });
    }

    public Gear GetGear(int x, int y)
    {
        var tmpGear = GearsList.SingleOrDefault(g => (int)g.Location.X == x && (int)g.Location.Y == y);

        if (tmpGear == null) throw new Exception($"Gear not found [{x}x | {y}y].");

        return tmpGear;
    }
    
    public bool ValidGearExists(int x, int y) => GearsList
        .Where(g => g.Valid)
        .Any(g => (int)g.Location.X == x && (int)g.Location.Y == y);

    public void FindAndAddRatio(Vector2? location, int ratio)
    {
        if (location == null) throw new Exception("Gear location is null.");

        var tmpGear = GearsList.SingleOrDefault(g => g.Location == location);
        if (tmpGear == null) throw new Exception("Gear not found.");

        tmpGear.AddRatio(ratio);
    }
}

public class Gear
{
    public Vector2 Location { get; init; }
    private int RatioPartOne { get; set; }
    private int RatioPartTwo { get; set; }
    public bool Valid => RatioPartOne != 0 && RatioPartTwo != 0;
    public int GearRatio => RatioPartOne * RatioPartTwo;

    public void AddRatio(int ratio)
    {
        if (RatioPartOne == 0)
        {
            RatioPartOne = ratio;
        }
        else if (RatioPartTwo == 0)
        {
            RatioPartTwo = ratio;
        }
        else
        {
            throw new Exception("More than two values for gear ratio.");
        }
    }
}