
using Newtonsoft.Json;

namespace Test
{
    public class Test
    {
        public static void Main()
        {
            var state = new StateSettings
            {
                MinManpower = 50,
                MaxManpower = 20000000
            };

            /*
             *  数据来自 https://hoi4.parawikis.com/wiki/%E5%9C%B0%E5%8C%BA%E6%A8%A1%E6%94%B9
             */
            state.StateCategory.Add("wasteland", 0);
            state.StateCategory.Add("enclave", 0);
            state.StateCategory.Add("tiny_island", 0);
            state.StateCategory.Add("small_island", 1);
            state.StateCategory.Add("pastoral", 1);
            state.StateCategory.Add("rural", 2);
            state.StateCategory.Add("town", 4);
            state.StateCategory.Add("large_town", 5);
            state.StateCategory.Add("city", 6);
            state.StateCategory.Add("large_city", 8);
            state.StateCategory.Add("metropolis", 10);
            state.StateCategory.Add("megalopolis", 12);
            
            using var stream = new FileStream("stateSettings.json", FileMode.Create);
            using var write = new StreamWriter(stream);
            write.Write(JsonConvert.SerializeObject(state, Formatting.Indented));
        }

        public class StateSettings
        {
            /// <summary>
            /// Key为地块类别, Value为地块提供的建筑槽位
            /// </summary>
            public readonly Dictionary<string, byte>? StateCategory = new(16);
            public int MinManpower { get; set; }
            public int MaxManpower { get; set; }
        }
    }
}