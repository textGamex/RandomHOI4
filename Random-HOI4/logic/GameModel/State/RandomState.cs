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
        private const string STATE_KEY = "state";

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

            if (state.Has(HISTORY_KEY))
            {
                var history = state.Child(HISTORY_KEY).Value;
                if (history.Has(BUILDINGS_KEY))
                {
                    var buildings = history.Child(BUILDINGS_KEY).Value;                    
                    buildings.ClearAllChilds();
                    //TODO: 暂时先不搞省份建筑了, 排除掉
                    var optionalBuildingList = StateSettings.Buildings?.Where(x => !x.IsProvincesBuilding).ToList() ?? throw new ArgumentNullException();

                    //TODO: 这个算法需要改进
                    foreach (var building in optionalBuildingList) 
                    {
                        if (_random.NextDouble() > 0.20)
                        {
                            buildings.AddChildDirectly(CWToolsHelper.NewLeaf(building?.BuildingName, _random.Next(1, building.MaxLevel + 1)));
                        }
                    }
                }
                else
                {

                }
            }
            else
            {

            }
        }

        public string Content => CKPrinter.printKeyValueList(_root.Root.ToRaw, 0);
    }
}
