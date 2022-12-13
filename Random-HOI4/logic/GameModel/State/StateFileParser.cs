using CWTools.Process;
using NLog;
using Random_HOI4.logic.Util.CWTool;
using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.Random;
using Random_HOI4.logic;

namespace Random_HOI4.Logic.GameModel.State
{
    internal class RandomState
    {
        private readonly CWToolsAdapter _root;
        private readonly Random _random = new MersenneTwister();
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public const string MANPOWER_KEY = "manpower";

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

        public int RandomizationManpower()
        {
            //TODO: 应该用离散分布 参阅: https://numerics.mathdotnet.com/Probability.html
            int manpower = _random.Next(Settings.StateSettings.MinManpower, Settings.StateSettings.MaxManpower + 1);
            var state = _root.Root.Child("state").Value;
            var newData = CWToolsHelper.NewLeaf(MANPOWER_KEY, manpower);

            if (state.Has(MANPOWER_KEY))
            {
                var list = state.AllChildren;
                list.RemoveAll(x => x.IsLeafC && x.leaf.Key == "manpower");
                list.Add(newData);
                state.AllChildren = list;
            }
            else
            {
                state.AddChildDirectly(newData);
            }
           
            return manpower;
        }
    }
}
