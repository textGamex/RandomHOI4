
using Newtonsoft.Json;

namespace Test
{
    public class Test
    {
        public static void Main()
        {
            var state = new StateSettings
            {
                Manpower = new Manpower()
                {
                    MinValue = 50,
                    MaxValue = 20000000
                }
            };

            /*
             * 地块类型.
             * 数据来自 https://hoi4.parawikis.com/wiki/%E5%9C%B0%E5%8C%BA%E6%A8%A1%E6%94%B9
             */
            state.StateCategory.Add(new StateCategory()
            {
                Type = "wasteland",
                Slot = 0
            });
            state.StateCategory.Add(new StateCategory()
            {
                Type = "enclave",
                Slot = 0
            });
            state.StateCategory.Add(new StateCategory()
            {
                Type = "tiny_island",
                Slot = 0
            });
            state.StateCategory.Add(new StateCategory()
            {
                Type = "small_island",
                Slot = 1
            });
            state.StateCategory.Add(new StateCategory()
            {
                Type = "pastoral",
                Slot = 1
            });
            state.StateCategory.Add(new StateCategory()
            {
                Type = "rural",
                Slot = 2
            });
            state.StateCategory.Add(new StateCategory()
            {
                Type = "town",
                Slot = 4
            });
            state.StateCategory.Add(new StateCategory()
            {
                Type = "large_town",
                Slot = 5
            });
            state.StateCategory.Add(new StateCategory()
            {
                Type = "city",
                Slot = 6
            });
            state.StateCategory.Add(new StateCategory()
            {
                Type = "large_city",
                Slot = 8
            });
            state.StateCategory.Add(new StateCategory()
            {
                Type = "metropolis",
                Slot = 10
            });
            state.StateCategory.Add(new StateCategory()
            {
                Type = "megalopolis",
                Slot = 12
            });

            /*
             * 建筑类型.
             * 数据来自HOI4文件, Path: Hearts of Iron IV\common\buildings\00_buildings.txt
             * HOI4版本: 1.12.*
             */
            state.Buildings.Add(new Buildings() 
            {
                BuildingName = "infrastructure",
                MaxLevel = 10,
                IsProvincesBuilding = false
            });
            state.Buildings.Add(new Buildings()
            {
                BuildingName = "arms_factory",
                MaxLevel = 20,
                IsProvincesBuilding = false
            });
            state.Buildings.Add(new Buildings()
            {
                BuildingName = "industrial_complex",
                MaxLevel = 20,
                IsProvincesBuilding = false
            });
            state.Buildings.Add(new Buildings()
            {
                BuildingName = "air_base",
                MaxLevel = 10,
                IsProvincesBuilding = false
            });
            state.Buildings.Add(new Buildings()
            {
                BuildingName = "supply_node",
                MaxLevel = 1,
                IsProvincesBuilding = false
            });
            state.Buildings.Add(new Buildings()
            {
                BuildingName = "naval_base",
                MaxLevel = 10,
                IsProvincesBuilding = true
            });
            state.Buildings.Add(new Buildings()
            {
                BuildingName = "bunker",
                MaxLevel = 10,
                IsProvincesBuilding = true
            });
            state.Buildings.Add(new Buildings()
            {
                BuildingName = "coastal_bunker",
                MaxLevel = 10,
                IsProvincesBuilding = true
            });
            state.Buildings.Add(new Buildings()
            {
                BuildingName = "dockyard",
                MaxLevel = 20,
                IsProvincesBuilding = false
            });
            state.Buildings.Add(new Buildings()
            {
                BuildingName = "anti_air_building",
                MaxLevel = 5,
                IsProvincesBuilding = false
            });
            //state.Buildings.Add(new Buildings()
            //{
            //    BuildingName = "fuel_silo",
            //    MaxLevel = 5,
            //    IsProvincesBuilding = false
            //});
            state.Buildings.Add(new Buildings()
            {
                BuildingName = "radar_station",
                MaxLevel = 6,
                IsProvincesBuilding = false
            });
            state.Buildings.Add(new Buildings()
            {
                BuildingName = "rocket_site",
                MaxLevel = 5,
                IsProvincesBuilding = false
            });
            state.Buildings.Add(new Buildings()
            {
                BuildingName = "nuclear_reactor",
                MaxLevel = 1,
                IsProvincesBuilding = false
            });

            /*
             * 战略资源.
             * 数据来自HOI4文件, path: Hearts of Iron IV\common\resources\00_resources.txt
             */
            state.Resources.Add(new Resources()
            {
                Type = "oil",
                ProbabilityOfOccurrence = 0.20,                
                MinRandomNumber = 0,
                MaxRandomNumber = 100
            });
            state.Resources.Add(new Resources()
            {
                Type = "aluminium",
                ProbabilityOfOccurrence = 0.35,                
                MinRandomNumber = 0,
                MaxRandomNumber = 100
            });
            state.Resources.Add(new Resources()
            {
                Type = "rubber",
                ProbabilityOfOccurrence = 0.35,                
                MinRandomNumber = 0,
                MaxRandomNumber = 100,
            });
            state.Resources.Add(new Resources()
            {
                Type = "tungsten",
                ProbabilityOfOccurrence = 0.35,                
                MinRandomNumber = 0,
                MaxRandomNumber = 100
            });
            state.Resources.Add(new Resources()
            {
                Type = "steel",
                ProbabilityOfOccurrence = 0.60,                
                MinRandomNumber = 5,
                MaxRandomNumber = 180
            });
            state.Resources.Add(new Resources()
            {
                Type = "chromium",
                ProbabilityOfOccurrence = 0.35,
                MinRandomNumber = 0,
                MaxRandomNumber = 100
            });

            using var stream = new FileStream("stateSettings.json", FileMode.Create);
            using var write = new StreamWriter(stream);
            write.Write(JsonConvert.SerializeObject(state, Formatting.Indented));
        }

        public class StateSettings
        {
            public readonly List<StateCategory> StateCategory = new(16);
            public readonly List<Buildings> Buildings = new(20);
            public readonly List<Resources> Resources = new(8);
            public Manpower? Manpower;
        }

        public class StateCategory
        {
            public string? Type { get; set; }
            public byte Slot { get; set; }
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
            public int MinRandomNumber { get; set; }
            public int MaxRandomNumber { get; set; }            
        }

        public class Manpower
        {
            public int MinValue { get; set; }
            public int MaxValue { get; set; }            
        }
    }
}