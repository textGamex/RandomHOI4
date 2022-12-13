using System.Collections.Generic;

namespace Random_HOI4.logic.Configuration
{
    public class StateSettings
    {
        public List<StateCategory>? StateCategory { get; set; }
        public List<Buildings>? Buildings { get; set; }
        public List<Resources>? Resources { get; set; }
        public int MinManpower { get; set; }
        public int MaxManpower { get; set; }
    }

    public class StateCategory
    {
        public string? Type { get; set; }
        public byte? Slot { get; set; }
    }

    public class Buildings
    {
        public string? BuildingName { get; set; }
        public byte MaxLevel { get; set; }
        public bool IsProvincesBuilding { get; set; }
    }

    public class Resources
    {
        public string? Type { get; set; }
        public double ProbabilityOfOccurrence { get; set; }
        public int MaxRandomNumber { get; set; }
        public int MinRandomNumber { get; set; }
    }
}
