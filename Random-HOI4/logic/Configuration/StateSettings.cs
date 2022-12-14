using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using NLog;
using System.Linq;

namespace Random_HOI4.logic.Configuration
{
    public class StateSettings
    {
        public List<StateCategory> StateCategory => _stateCategory.ToList();
        public List<Buildings> Buildings => _buildings.ToList();
        public List<Resources> Resources => _resources.ToList();
        public int MinManpower { get; }
        public int MaxManpower { get; }

        private readonly List<StateCategory> _stateCategory;
        private readonly List<Buildings> _buildings;
        private readonly List<Resources> _resources;

        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        [JsonConstructor]
        public StateSettings(List<StateCategory>? stateCategory, List<Buildings>? buildings, List<Resources>? resources, int minManpower, int maxManpower)
        {
            _stateCategory = stateCategory ?? throw new ArgumentNullException(nameof(stateCategory));
            _buildings = buildings ?? throw new ArgumentNullException(nameof(buildings));
            _resources = resources ?? throw new ArgumentNullException(nameof(resources));
            MinManpower = minManpower;
            MaxManpower = maxManpower;
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
        public int MaxRandomNumber { get; }
        public int MinRandomNumber { get; }

        [JsonConstructor]
        public Resources(string? type, double probabilityOfOccurrence, int maxRandomNumber, int minRandomNumber)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            ProbabilityOfOccurrence = probabilityOfOccurrence;
            MaxRandomNumber = maxRandomNumber;
            MinRandomNumber = minRandomNumber;
        }
    }
}
