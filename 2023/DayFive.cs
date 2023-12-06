using AdventOfCode.Helpers;

namespace AdventOfCode._2023;

public class DayFive
{
    private Almanac? _almanac;
    
    [SetUp]
    public void Setup()
    {
        _almanac = new Almanac(PuzzleInput.Load(2023, 5));
    }

    [Test]
    public void PartOne()
    {
        Console.WriteLine($"Day Two, Part One Answer: ");
    }

    [Test]
    public void PartTwo()
    {
        Console.WriteLine($"Day Two, Part Two Answer: ");
    }
}

public class Almanac
{
    public List<long> SeedsToPlant { get; set; }
    public List<TypeMap> TypeMaps = new();
    
    public Almanac(IReadOnlyCollection<string> input)
    {
        ProcessInput();
        
        return;

        void ProcessInput()
        {
            var currentMapType = MapTypes.None;

            foreach (var line in input.Where(line => !string.IsNullOrEmpty(line)))
            {
                if (currentMapType == MapTypes.None && line == input.First())
                {
                    ProcessCategoryData(line, currentMapType);
                }
                else if (!char.IsDigit(line[0]))
                {
                    currentMapType = DetermineMapType(line);
                }
                else
                {
                    ProcessCategoryData(line, currentMapType);
                }
            }
        }

        MapTypes DetermineMapType(string categoryLine)
        {
            var mapName = categoryLine.Split(' ').First();

            return mapName switch
            {
                AlmanacConstants.SeedToSoil => MapTypes.SeedToSoil,
                AlmanacConstants.SoilToFertilizer => MapTypes.SoilToFertilizer,
                AlmanacConstants.FertilizerToWater => MapTypes.FertilizerToWater,
                AlmanacConstants.WaterToLight => MapTypes.WaterToLight,
                AlmanacConstants.LightToTemperature => MapTypes.LightToTemperature,
                AlmanacConstants.TemperatureToHumidity => MapTypes.TemperatureToHumidity,
                AlmanacConstants.HumidityToLocation => MapTypes.HumidityToLocation,
                _ => throw new Exception("Unknown map type")
            };
        }

        void ProcessCategoryData(string line, MapTypes currentMapType)
        {
            if (currentMapType == MapTypes.None)
            {
                HandleSeeds();
            }
            else
            {
                HandleMappings();
            } 

            return;

            void HandleSeeds() => SeedsToPlant = line
                    .Split(':')
                    .Last()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(long.Parse)
                    .ToList();

            void HandleMappings()
            {
                var currentMapTypeAsString = currentMapType.ToString();
                var currentMap = TypeMaps.SingleOrDefault(tm => tm.Name == currentMapTypeAsString);
                
                if (currentMap == null)
                {
                    currentMap = new TypeMap {Name = currentMapTypeAsString};
                    TypeMaps.Add(currentMap);
                }

                var numbers = line.Split(' ');
                
                currentMap.Mappings.Add(new TypeMap.Mapping
                {
                    DestinationRangeStart = long.Parse(numbers[0]),
                    SourceRangeStart = long.Parse(numbers[1]),
                    RangeLength = long.Parse(numbers[2])
                });
            }
        }
    }

    public class TypeMap
    {
        public string Name { get; set; }
        public List<Mapping> Mappings = new();

        public class Mapping
        {
            public long DestinationRangeStart { get; set; }

            public List<long> DestinationRangeNumbers
            {
                get
                {
                    var numbers = new List<long>();
                    for (var i = DestinationRangeStart; i <= DestinationRangeEnd; i++)
                    {
                        numbers.Add(i);
                    }

                    return numbers;
                }
            }
            public long DestinationRangeEnd => DestinationRangeStart + (RangeLength - 1);
            public long SourceRangeStart { get; set; }
            public long SourceRangeEnd => SourceRangeStart + (RangeLength - 1);
            public List<long> SourceRangeNumbers
            {
                get
                {
                    var numbers = new List<long>();
                    for (var i = SourceRangeStart; i <= SourceRangeEnd; i++)
                    {
                        numbers.Add(i);
                    }

                    return numbers;
                }
            }
            public long RangeLength { get; set; }
        }
    }

    private enum MapTypes
    {
        SeedToSoil,
        SoilToFertilizer,
        FertilizerToWater,
        WaterToLight,
        LightToTemperature,
        TemperatureToHumidity,
        HumidityToLocation,
        None
    }

    private static class AlmanacConstants
    {
        public const string SeedToSoil = "seed-to-soil";
        public const string SoilToFertilizer = "soil-to-fertilizer";
        public const string FertilizerToWater = "fertilizer-to-water";
        public const string WaterToLight = "water-to-light";
        public const string LightToTemperature = "light-to-temperature";
        public const string TemperatureToHumidity = "temperature-to-humidity";
        public const string HumidityToLocation = "humidity-to-location";
    }
}