using CWTools.Process;
using NLog;
using Random_HOI4.logic.Util.CWTool;
using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.Random;
using Random_HOI4.logic;
using static Random_HOI4.logic.Settings;
using CWTools.Parser;

namespace Random_HOI4.Logic.GameModel.State
{
    internal class RandomState
    {
        private readonly CWToolsAdapter _root;
        private readonly Random _random = new MersenneTwister();
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public const string MANPOWER_KEY = "manpower";
        public const string HISTORY_KEY = "history";
        public const string BUILDINGS_KEY = "buildings";
        public const string STATE_KEY = "state";
        public const string STATE_CATEGORY_KEY = "state_category";

        public RandomState(string path)
        {
            if (CWToolsAdapter.TryParseFile(path, out var adapter))
            {
                _root = adapter;
            }
            else
            {
                var ex = new ArgumentException($"文件解析失败, path={path}");
                _logger.Warn(ex);
                throw ex;
            }
        }        

        /// <summary>
        /// 随机化地块人口
        /// </summary>
        /// <returns>随机人口</returns>
        public int RandomizationManpower()
        {
            //TODO: 应该使用离散分布 参阅: https://numerics.mathdotnet.com/Probability.html
            int manpower = _random.Next(StateSettings.MinManpower, StateSettings.MaxManpower + 1);
            var state = _root.Root.Child(STATE_KEY).Value;
            var newData = CWToolsHelper.NewLeaf(MANPOWER_KEY, manpower);

            if (state.Has(MANPOWER_KEY))
            {
                var list = state.AllChildren;
                list.RemoveAll(x => x.IsLeafC && x.leaf.Key == MANPOWER_KEY);
                list.Add(newData);
                state.AllChildren = list;
            }
            else
            {
                state.AddChildDirectly(newData);
            }
           
            return manpower;
        }

        public void RandomizationBuildings()
        {
            var state = _root.Root.Child(STATE_KEY).Value;
            Node buildings;
            if (state.Has(HISTORY_KEY))
            {
                var history = state.Child(HISTORY_KEY).Value;                
                if (history.Has(BUILDINGS_KEY))
                {
                    buildings = history.Child(BUILDINGS_KEY).Value;
                    //清空原有的建筑
                    buildings.ClearAllChilds();                    
                }
                else
                {
                    buildings = Node.Create(BUILDINGS_KEY);
                    history.AddChildDirectly(Child.NewNodeC(buildings));
                }
            }
            else
            {
                var historyNode = Node.Create(HISTORY_KEY);
                buildings = Node.Create(BUILDINGS_KEY);
                historyNode.AddNodeDirectly(buildings);
                state.AddNodeDirectly(historyNode);
            }

            //TODO: 暂时不搞省份建筑, 排除掉
            var optionalBuildingList = StateSettings.Buildings?.Where(x => !x.IsProvincesBuilding).ToList() ?? throw new ArgumentNullException();
            //TODO: 这个算法需要改进
            foreach (var building in optionalBuildingList)
            {
                if (_random.NextDouble() > 0.25)
                {
                    buildings.AddChildDirectly(CWToolsHelper.NewLeaf(building?.BuildingName ?? throw new ArgumentException(),
                        _random.Next(1, building.MaxLevel + 1)));
                }
            }
        }

        public string RandomizationStateCategory()
        {
            int index = _random.Next(StateSettings.StateCategory?.Count ?? throw new ArgumentException());
            string stateType = StateSettings.StateCategory[index]?.Type ?? throw new ArgumentException();
            var state = _root.Root.Child(STATE_KEY).Value;
            
            if (state.Has(STATE_CATEGORY_KEY))
            {
                var list = state.AllChildren;
                list.RemoveAll(x => x.IsLeafC && x.leaf.Key == STATE_CATEGORY_KEY);
                list.Add(CWToolsHelper.NewLeafWhitString(STATE_CATEGORY_KEY, stateType));
                state.AllChildren = list;
            }
            return stateType;
        }

        //public Dictionary<string, byte> RandomizationResources

        public string Content => CKPrinter.printKeyValueList(_root.Root.ToRaw, 0);
    }
}
