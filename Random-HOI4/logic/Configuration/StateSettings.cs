using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Random_HOI4.logic.Configuration
{
    public class StateSettings
    {
        /// <summary>
        /// Key为地块类别, Value为地块提供的建筑槽位
        /// </summary>
        public readonly Dictionary<string, byte>? StateCategory;
        public int MinManpower { get; set; }
        public int MaxManpower { get; set; }
    }
}
