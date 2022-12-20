using Newtonsoft.Json;
using Random_HOI4.Logic.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Random_HOI4.Logic
{
    public static class Settings
    {
        public readonly static StateSettings StateSettings;

        static Settings() 
        {
            StateSettings = JsonConvert.DeserializeObject<StateSettings>(
                File.ReadAllText(GetDataPath("stateSettings.json"))) ?? throw new FileNotFoundException("无法找到配置文件");
        }

        private static string GetDataPath(string fileName)
        {
            return $@".\Data\Configuration\{fileName}";
        }
    }
}
