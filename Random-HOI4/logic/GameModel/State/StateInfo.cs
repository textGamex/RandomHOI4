//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Random_HOI4.Logic.GameModel.State
//{
//    internal class StateInfo
//    {
//        public string OwnerTag { get; }
//        public int Id { get; }
//        public int Manpower { get; }
//        private readonly List<string> _hasCoreTags;
//        private readonly Dictionary<ResourcesType, ushort> _resourcesMap = new Dictionary<ResourcesType, ushort>();
//        private readonly Dictionary<BuildingType, byte> _buildingMap = new Dictionary<BuildingType, byte>();

//        public StateInfo(string path)
//        {
//            var parser = new StateFileParser(path ?? throw new ArgumentNullException(nameof(path)));

//            Id = parser.GetId();
//            OwnerTag = parser.GetOwnerTag();
//            Manpower = parser.GetManpower();
//            _hasCoreTags = parser.GetHasCoreCountryTags();
//            AddAllResourcesNumber(parser);
//            AddAllBuildingLevel(parser);
//        }

//        public ReadOnlyCollection<string> GetHasCoreTags()
//        {
//            return _hasCoreTags.AsReadOnly();
//        }

//        private void AddAllResourcesNumber(StateFileParser parser)
//        {
//            foreach (ResourcesType type in Enum.GetValues(typeof(ResourcesType)))
//            {
//                var resourcesNumber = parser.GetResourcesNumber(type);
//                _resourcesMap.Add(type, resourcesNumber);
//            }
//        }

//        private void AddAllBuildingLevel(StateFileParser parser)
//        {
//            foreach (BuildingType buildingType in Enum.GetValues(typeof(BuildingType)))
//            {
//                var buildingLevel = parser.GetBuildingLevel(buildingType);
//                _buildingMap.Add(buildingType, buildingLevel);
//            }
//        }
//    }
//}
