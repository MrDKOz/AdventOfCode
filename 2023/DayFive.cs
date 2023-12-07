﻿using AdventOfCode.Helpers;

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
        Console.WriteLine($"Day Two, Part One Answer: {_almanac?.Locations.Min()}");
    }

    [Test]
    public void PartTwo()
    {
        Console.WriteLine($"Day Two, Part Two Answer: {_almanac?.PartTwo()}");
    }
}

public class Almanac
{
    public List<long> SeedsToPlant { get; set; }
    public List<Map> Maps = new();
    public List<long> Locations = new();
    public List<long> Locations2 = new();
    
    public Almanac(IReadOnlyCollection<string> input)
    {
        ProcessInput();
        FetchLocations();
        
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

        void FetchLocations()
        {
            foreach (var seed in SeedsToPlant)
            {
                var focus = seed;

                foreach (var map in Maps)
                {
                    foreach (var mapping in map.Mappings)
                    {
                        if (focus >= mapping.SourceRangeStart && focus <= mapping.SourceRangeEnd)
                        {
                            focus = mapping.DestinationRangeStart + (focus - mapping.SourceRangeStart);
                            break;
                        }
                    }
                }

                Locations.Add(focus);
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
                var currentMap = Maps.SingleOrDefault(tm => tm.Name == currentMapTypeAsString);
                
                if (currentMap == null)
                {
                    currentMap = new Map {Name = currentMapTypeAsString};
                    Maps.Add(currentMap);
                }

                var numbers = line.Split(' ');
                
                currentMap.Mappings.Add(new Map.Mapping
                {
                    DestinationRangeStart = long.Parse(numbers[0]),
                    SourceRangeStart = long.Parse(numbers[1]),
                    RangeLength = long.Parse(numbers[2])
                });
            }
        }
    }

    public long PartTwo()
    {
        var lowestFound = long.MaxValue;
        foreach (var seedChunk in SeedsToPlant.Chunk(2).ToArray())
        {
            for (var i = seedChunk[0]; i < seedChunk[0] + seedChunk[1]; i++)
            {
                var focus = i;

                foreach (var map in Maps)
                {
                    foreach (var mapping in map.Mappings)
                    {
                        if (focus >= mapping.SourceRangeStart && focus <= mapping.SourceRangeEnd)
                        {
                            focus = mapping.DestinationRangeStart + (focus - mapping.SourceRangeStart);
                            break;
                        }
                    }
                
                    lowestFound = Math.Min(lowestFound, focus);
                }
            }
        }

        return lowestFound;
    }

    
    public class Map
    {
        public string Name { get; set; }
        public List<Mapping> Mappings = new();

        public class Mapping
        {
            public long DestinationRangeStart { get; set; }
            public long DestinationRangeEnd => DestinationRangeStart + (RangeLength - 1);
            public long SourceRangeStart { get; set; }
            public long SourceRangeEnd => SourceRangeStart + (RangeLength - 1);
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