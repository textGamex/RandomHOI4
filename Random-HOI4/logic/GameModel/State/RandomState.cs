using CWTools.Parser;
using CWTools.Process;
using MathNet.Numerics.Random;
using NLog;
using Random_HOI4.logic.Util.CWTool;
using System;
using System.Collections.Generic;
using System.Linq;
using static Random_HOI4.logic.Settings;

namespace Random_HOI4.Logic.GameModel.State
{
    internal class RandomState
    {
        private readonly CWToolsAdapter _root;
        private readonly Random _random = new MersenneTwister();
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public static class Key
        {
            public const string MANPOWER = "manpower";
            public const string HISTORY = "history";
            public const string BUILDINGS = "buildings";
            public const string STATE = "state";
            public const string STATE_CATEGORY = "state_category";
            public const string RESOURCES = "resources";
        }

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
            var state = _root.Root.Child(Key.STATE).Value;
            var newData = CWToolsHelper.NewLeaf(Key.MANPOWER, manpower);

            if (state.Has(Key.MANPOWER))
            {
                var list = state.AllChildren;
                list.RemoveAll(x => x.IsLeafC && x.leaf.Key == Key.MANPOWER);
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
            var state = _root.Root.Child(Key.STATE).Value;
            Node buildings;
            if (state.Has(Key.HISTORY))
            {
                var history = state.Child(Key.HISTORY).Value;
                if (history.Has(Key.BUILDINGS))
                {
                    buildings = history.Child(Key.BUILDINGS).Value;
                    //清空原有的建筑
                    buildings.ClearAllChilds();
                }
                else
                {
                    buildings = Node.Create(Key.BUILDINGS);
                    history.AddChildDirectly(Child.NewNodeC(buildings));
                }
            }
            else
            {
                var historyNode = Node.Create(Key.HISTORY);
                buildings = Node.Create(Key.BUILDINGS);
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
            var state = _root.Root.Child(Key.STATE).Value;

            if (state.Has(Key.STATE_CATEGORY))
            {
                var list = state.AllChildren;
                list.RemoveAll(x => x.IsLeafC && x.leaf.Key == Key.STATE_CATEGORY);
                list.Add(CWToolsHelper.NewLeafWhitString(Key.STATE_CATEGORY, stateType));
                state.AllChildren = list;
            }
            return stateType;
        }

        public Dictionary<string, int> RandomizationResources()
        {
            var state = _root.Root.Child(Key.STATE).Value;
            var map = new Dictionary<string, int>();
            Node resourcesNode;

            if (state.Has(Key.RESOURCES))
            {
                resourcesNode = state.Child(Key.RESOURCES).Value;
            }
            else
            {
                resourcesNode = Node.Create(Key.RESOURCES);
                state.AddNodeDirectly(resourcesNode);
            }

            foreach (var resources in StateSettings.Resources ?? throw new ArgumentException(nameof(StateSettings.Resources)))
            {
                if (_random.NextDouble() > resources.ProbabilityOfOccurrence)
                {
                    continue;
                }
                int resourcesAmount = _random.Next(resources.MinRandomNumber, resources.MaxRandomNumber + 1);
                if (resourcesAmount == 0)
                {
                    continue;
                }
                resourcesNode.AddChildDirectly(CWToolsHelper.NewLeaf(
                    resources.Type ?? throw new ArgumentException(nameof(resources.Type)), resourcesAmount));
                //为了防止异常, 因为可能有重复的资源类型
                map[resources.Type] = resourcesAmount;
            }
            return map;
        }

        public string Content => CKPrinter.printKeyValueList(_root.Root.ToRaw, 0);
    }
}
