using System.Collections.Generic;

namespace Random_HOI4.logic.Configuration
{
    public class StateSettings
    {
        public List<StateCategory>? StateCategory { get; set; }
        public List<Buildings>? Buildings { get; set; }
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
}
