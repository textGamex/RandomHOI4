using Newtonsoft.Json;
using Random_HOI4.logic.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Random_HOI4.logic
{
    public static class Settings
    {
        public readonly static StateSettings StateSettings;

        static Settings() 
        {
            string statePath = GetDataPath("stateSettings.json");
            using var stream = new FileStream(statePath, FileMode.Open);
            using var reader = new StreamReader(stream);
            StateSettings = JsonConvert.DeserializeObject<StateSettings>(
                reader.ReadToEnd()) ?? throw new FileNotFoundException("无法找到配置文件", statePath);
        }

        private static string GetDataPath(string fileName)
        {
            return $@".\Data\Configuration\{fileName}";
        }
    }
}
