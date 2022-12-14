using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using NLog;
using System.Linq;

namespace Random_HOI4.Logic.Configuration
{
    public class StateSettings
    {
        public List<StateCategory> StateCategory => _stateCategory.ToList();
        public List<Buildings> Buildings => _buildings.ToList();
        public List<Resources> Resources => _resources.ToList();
        public Manpower Manpower => _manpower;

        private readonly List<StateCategory> _stateCategory;
        private readonly List<Buildings> _buildings;
        private readonly List<Resources> _resources;
        private readonly Manpower _manpower;

        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        [JsonConstructor]
        public StateSettings(List<StateCategory>? stateCategory, List<Buildings>? buildings, List<Resources>? resources, Manpower? manpower)
        {
            _stateCategory = stateCategory ?? throw new ArgumentNullException(nameof(stateCategory));
            _buildings = buildings ?? throw new ArgumentNullException(nameof(buildings));
            _resources = resources ?? throw new ArgumentNullException(nameof(resources));
            _manpower = manpower ?? throw new ArgumentNullException(nameof(manpower));
        }
    }

    public class StateCategory
    {
        public string Type { get; }
        public byte Slot { get; }

        [JsonConstructor]
        public StateCategory(string? type, byte slot)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Slot = slot;
        }
    }

    public class Buildings
    {
        public string BuildingName { get; }
        public byte MaxLevel { get; }
        public bool IsProvincesBuilding { get; }

        [JsonConstructor]
        public Buildings(string? buildingName, byte maxLevel, bool isProvincesBuilding)
        {
            BuildingName = buildingName ?? throw new ArgumentNullException(nameof(buildingName));
            MaxLevel = maxLevel;
            IsProvincesBuilding = isProvincesBuilding;
        }
    }

    public class Resources
    {
        public string Type { get; }
        public double ProbabilityOfOccurrence { get; }
        public int MinRandomNumber { get; }
        public int MaxRandomNumber { get; }
        
        [JsonConstructor]
        public Resources(string? type, double probabilityOfOccurrence, int maxRandomNumber, int minRandomNumber)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            ProbabilityOfOccurrence = probabilityOfOccurrence;
            MinRandomNumber = minRandomNumber;
            MaxRandomNumber = maxRandomNumber;            
        }
    }

    public class Manpower
    {
        public int MinValue { get; }
        public int MaxValue { get; }

        [JsonConstructor]
        public Manpower(int minManpower, int maxManpower)
        {
            MinValue = minManpower;
            MaxValue = maxManpower;
        }
    }
}
